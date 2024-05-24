import pika
import json
from django.core.management.base import BaseCommand
import pika.channel
from datetime import datetime
from ...utils import get_search_collection as db
from decimal import Decimal
from bson.decimal128 import Decimal128

class Command(BaseCommand):
    help = 'Consume AuctionCreated events from RabbitMQ'

    def handle(self, *args, **kwargs):
        def callback(ch, method, properties, body):
            # call back function processes the message
            print("Message received")
            message = json.loads(body)
            auction = message['message']
            print(auction)

            document = {
                'auction_id': auction['id'],
                'reserve_price': auction['reservePrice'],
                'seller': auction['seller'],
                'winner': auction['winner'],
                'sold_amount': auction['soldAmount'],
                'current_high_bid': auction['currentHighBid'],
                'created_at': auction['createdAt'],
                'updated_at': auction['updatedAt'],
                'auction_end': auction['auctionEnd'],
                'status': auction['status'],
                'title': auction['title'],
                'artist': auction['artist'],
                'width': auction['width'],
                'height': auction['height'],
                'depth': auction['depth'],
                'style': auction['style'],
                'medium': auction['medium'],
                'current_location': auction['currentLocation'],
                'value': auction['value'],
                'image_url': auction['imageUrl'],
                'is_authenticated': auction['isAuthenticated']
            }

            for field in ['created_at', 'updated_at', 'auction_end']:
                if field in document and isinstance(document[field], str):
                    document[field] = datetime.fromisoformat(document[field].replace('Z','+00:00'))

            for field in ['sold_amount', 'current_high_bid', 'width', 'height', 'depth', 'value', 'reserve_price']:
                if field in document and isinstance(document[field], str):
                    document[field] = Decimal128(document[field])

            collection = db()

            try:
                collection.insert_one(document)
            except Exception as err:
                print(err)

        connection_params = pika.ConnectionParameters(
            host='localhost',
            port=5672,
            credentials=pika.PlainCredentials('guest', 'guest')
        )

        connection = pika.BlockingConnection(connection_params)
        channel = connection.channel()

        channel.exchange_declare(exchange='EventBus.Messages:AuctionCreated', exchange_type='fanout', durable=True)

        queue_name = 'search-auction-created'
        channel.queue_declare(queue=queue_name, durable=True)

        channel.queue_bind(exchange='EventBus.Messages:AuctionCreated', queue=queue_name)

        channel.basic_consume(queue=queue_name, on_message_callback=callback, auto_ack=True)

        print('Waiting for messages')

        try:
            channel.start_consuming()
        except KeyboardInterrupt:
            print('Interrupted')
            channel.stop_consuming()

        connection.close()

