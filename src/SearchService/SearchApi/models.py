from django.db import models

# Create your models here.
class Auction(models.Model):
    auction_id = models.UUIDField(max_length=50, auto_created=False)
    title = models.CharField(max_length=70)
    description = models.CharField(max_length=120)
    seller = models.CharField(max_length=50)
    buyer = models.CharField(max_length=50, default='')
    status = models.CharField(max_length=20, default='')

    def __str__(self):
        return self.title
