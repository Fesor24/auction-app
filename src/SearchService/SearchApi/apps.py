from django.apps import AppConfig
from . import utils

class SearchapiConfig(AppConfig):
    default_auto_field = 'django.db.models.BigAutoField'
    name = 'SearchApi'

    # we override this method to perform initializiation task
    def ready(self):
        import os

        # on first build, this will return None, for subsequent auto reload, it will return true
        # we only want the code to run once
        # hence this check
        if os.environ.get('RUN_MAIN', None) != 'true':
            try:
                utils.load_db_data()
            except Exception as e:
                print(f'An error occurred. Details: {e}')
