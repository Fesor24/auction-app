from pymongo import MongoClient

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
