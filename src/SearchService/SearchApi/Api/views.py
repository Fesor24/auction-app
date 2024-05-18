from rest_framework.response import Response
from rest_framework.views import APIView
from pymongo import MongoClient

client = MongoClient(host='localhost', port=27017)
db = client['search_auction']
collection = db['search']

class MovieSearch(APIView):
    def get(self, request):
        auctions = collection.find({})
        for auction in auctions:
            print(auction['title'])
        return Response({"data":"Hello"})
    
    def post(self, request):
        auction = {
            "title": "First Auction",
            "description": "My first auction"
        }
        collection.insert_one(auction)
        return Response({"data":"auction created"})

    