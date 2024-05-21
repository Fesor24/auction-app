from pymongo import MongoClient
import requests
from datetime import datetime

uri = 'mongodb://root:bidbay_pw@localhost:7722/'


def get_search_collection():
    client = MongoClient(uri)
    db = client['search_auction']
    collection = db['search']
    indexes = collection.index_information()
    if not 'auction_id_1' in indexes:
        collection.create_index([('auction_id', 1)], unique=True)
    collection.create_index([('title', 1)], unique=False)

    return collection


def load_db_data():
    collection = get_search_collection()
    if collection.count_documents({}) == 0:
        try:
            response = requests.get('http://localhost:7000/api/auction/list')
            response.raise_for_status()
        except requests.exceptions.HTTPError as err:
            print(f'An error occurred while sending request. Details: {err}')
        if response.status_code == 200:
            data = response.json()
            documents = []
            for item in data:
                document = {
                    'auction_id': item['id'],
                    'reserve_price': item['reservePrice'],
                    'seller' : item['seller'],
                    'winner' : item['winner'],
                    'sold_amount' : item['soldAmount'],
                    'current_high_bid' : item['currentHighBid'],
                    'created_at' : item['createdAt'],
                    'updated_at' : item['updatedAt'],
                    'auction_end' : item['auctionEnd'],
                    'status' : item['status'],
                    'title' : item['item']['title'],
                    'artist' : item['item']['artist'],
                    'width' : item['item']['width'],
                    'height' : item['item']['height'],
                    'depth': item['item']['depth'],
                    'style' : item['item']['style'],
                    'medium' : item['item']['medium'],
                    'current_location' : item['item']['currentLocation'],
                    'value' : item['item']['value'],
                    'image_url' : item['item']['imageUrl'],
                    'is_authenticated' : item['item']['isAuthenticated']
                }

                for field in ['created_at', 'updated_at', 'auction_end']:
                    if field in document and isinstance(document[field], str):
                        document[field] = datetime.fromisoformat(document[field].replace("Z", "+00:00"))
             
                # collection.insert_one(auction)
                documents.append(document)
                # print(document)

            collection.insert_many(documents)
        else:
            print(response)
    else:
        print("Documents exists")
