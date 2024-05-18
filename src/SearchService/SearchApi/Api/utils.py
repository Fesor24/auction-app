from pymongo import MongoClient

port = 27017

def get_search_collection():
    client = MongoClient(host='localhost', port=port)
    db = client['search_auction']
    collection = db['search']
    indexes = collection.index_information()
    if not 'auction_id_1' in indexes:
        collection.create_index([('auction_id', 1)], unique=True)
    collection.create_index([('title', 1)], unique=False)
    
    return collection
