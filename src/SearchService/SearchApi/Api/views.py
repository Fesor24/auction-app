from rest_framework.response import Response
from rest_framework.views import APIView
from rest_framework import status
from . import utils
from . import serializers
from .. import models

collection = utils.get_search_collection()

class MovieSearch(APIView):
    def get(self, request):
        page_number = request.GET.get('pageNumber', 1)
        page_size = request.GET.get('pageSize', 10)

        try:
            page_number = int(page_number)
            page_size = int(page_size)
        except ValueError:
            return Response({"error": "Invalid page number or page size"}, 
                            status=status.HTTP_400_BAD_REQUEST)
        
        filter = {}

        skip = (page_number - 1) * page_size

        auctions = collection.find(filter).skip(skip).limit(page_size)
        record_count = collection.count_documents(filter)
        serializer = serializers.AuctionSerializer(auctions, many=True)
        return Response({"data": serializer.data,
                         "total_count": record_count,
                         "page_number": page_number,
                         "page_size": page_size}, 
                         status=status.HTTP_200_OK)
    
    def post(self, request):
        serializer = serializers.AuctionSerializer(data=request.data)

        if serializer.is_valid():
            collection.insert_one(serializer.data)
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        else:
            return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
        
class MovieSearchItem(APIView):
    def get(self, request, id):
        try:
            auction = collection.find_one({"auction_id": id})

        except models.Auction.DoesNotExist:
            return Response("Auction not found", status=status.HTTP_404_NOT_FOUND)
        
        serializer = serializers.AuctionSerializer(auction)
        return Response(serializer.data, status=status.HTTP_200_OK)
    
    def put(self, request, id):
        serializer = serializers.AuctionSerializer(data=request.data)
        if not serializer.is_valid():
            return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
        data = request.data
        seller = data['seller'] # update these two for now
        auction_status = data['status']
        result = collection.update_one({'auction_id': id}, {'$set':{'seller': seller, 
                                                                    'status': auction_status}})

        if result.modified_count > 0:
            return Response(status=status.HTTP_204_NO_CONTENT)
        else:
            return Response({"error":"An error occurred during update"}, 
                            status=status.HTTP_400_BAD_REQUEST) 
    
    def delete(self, request, id):
        result = collection.delete_one({"auction_id":id})
        if result.deleted_count > 0:
            return Response(status=status.HTTP_204_NO_CONTENT)
        else:
            return Response({"error":"An error occurred during delete"}, 
                            status=status.HTTP_400_BAD_REQUEST)

        


    