from pymongo import MongoClient

def get_db_handler():
    client = MongoClient(host='localhost', port=27017)
    db = client['search_auction']
    collection = db['search']
    indexes = collection.index_information()
    if not 'auction_id_1' in indexes:
        collection.create_index([('auction_id', 1)], unique=True)
    collection.create_index([('title', 1)], unique=False)
    
    return collection
