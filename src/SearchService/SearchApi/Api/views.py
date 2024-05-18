from rest_framework.response import Response
from rest_framework.views import APIView
from . import utils

collection = utils.get_db_handler()

class MovieSearch(APIView):
    def get(self, request):
        auctions = collection.find({})
        for auction in auctions:
            print(auction['title'])
        return Response({"data":"Hello"})
    
    def post(self, request):
        auction = {
            "title": "Second Auction",
            "description": "My second auction"
        }
        collection.insert_one(auction)
        return Response({"data":"auction created"})

    