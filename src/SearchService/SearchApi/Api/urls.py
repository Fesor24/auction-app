from django.urls import path
from . import views

urlpatterns = [
    path('', views.MovieSearch.as_view(), name="movie-search"),
    path('<id>', views.MovieSearchItem.as_view(), name="movie_search_item")
]