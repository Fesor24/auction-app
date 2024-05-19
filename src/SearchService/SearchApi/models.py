from django.db import models

# Create your models here.
class Auction(models.Model):
    auction_id = models.CharField(max_length=70)
    title = models.CharField(max_length=70)
    description = models.CharField(max_length=120)
    seller = models.CharField(max_length=50)
    buyer = models.CharField(max_length=50, default='')
    status = models.CharField(max_length=20, default='')
    reserve_price= models.DecimalField(decimal_places=2, max_digits=18)
    sold_amount = models.DecimalField(decimal_places=2, max_digits=18)
    current_high_bid = models.DecimalField(decimal_places=2, max_digits=18)
    created_at = models.DateTimeField(auto_now= False)
    updated_at = models.DateTimeField(auto_now= False)
    auction_end = models.DateTimeField(auto_now= False)
    width = models.DecimalField(decimal_places=2, max_digits=18)
    height = models.DecimalField(decimal_places=2, max_digits=18)
    depth = models.DecimalField(decimal_places=2, max_digits=18)
    style = models.CharField(max_length=20)
    medium = models.CharField(max_length=20)
    current_location = models.CharField(max_length=70)
    value = models.DecimalField(decimal_places=2, max_digits=18)
    image_url = models.CharField(max_length=120)
    is_authenticated = models.BooleanField()

    def __str__(self):
        return self.title
