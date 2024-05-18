from rest_framework import serializers
from .. import models

class AuctionSerializer(serializers.ModelSerializer):
    class Meta:
        model = models.Auction
        fields="__all__"