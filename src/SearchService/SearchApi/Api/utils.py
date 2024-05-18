from pymongo import MongoClient

def get_db_handler():
    client = MongoClient(host='localhost', port=27017)
    db = client['search_auction']
    collection = db['search']
    return collection
