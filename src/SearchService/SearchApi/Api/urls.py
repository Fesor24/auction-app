from django.urls import path
from . import views

urlpatterns = [
    path('', views.AuctionSearch.as_view(), name="movie-search"),
    path('<id>', views.AuctionSearchItem.as_view(), name="movie_search_item")
]