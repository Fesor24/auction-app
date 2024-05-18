from django.db import models

# Create your models here.
class Auction(models.Model):
    auction_id = models.CharField(max_length=50)
    title = models.CharField(max_length=70)
    description = models.CharField(max_length=120)
    seller = models.CharField(max_length=50)
    buyer = models.CharField(max_length=50, default='')
    status = models.CharField(max_length=10, default='')

    def __str__(self):
        return self.title
