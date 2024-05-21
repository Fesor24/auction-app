from rest_framework import serializers
from .. import models
from datetime import datetime
from decimal import Decimal

class AuctionSerializer(serializers.ModelSerializer):
    class Meta:
        model = models.Auction
        fields="__all__"

    # invoked when data is created
    def to_internal_value(self, data):
        data = super().to_internal_value(data)
        for field in ['created_at', 'updated_at', 'auction_end']:
            if field in data and isinstance(data[field], str):
                data[field] = datetime.fromisoformat(data[field].replace("Z", "+00:00"))
        for field in ['reserve_price', 'sold_amount', 'current_high_bid', 'width', 'height', 'depth', 'value']:
            if field in data and isinstance(data[field], Decimal):
                data[field] = float(data[field])
        return data
    
    # invoked when data is being read from db
    # def to_representation(self, instance):
    #     data = super().to_representation(instance)
    #     if 'auction_id' in data:
    #         if instance(data['auction_id'], Binary):
    #             data['auction_id'] = Binary.as_uuid(data['auction_id'])
    #     return data