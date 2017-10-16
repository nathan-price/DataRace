"""
Definition of models.
"""

from django.conf import settings
from django.db import models
from django.contrib.auth.models import User
from allauth.account.signals import user_signed_up, user_logged_in
from django.core import serializers
from django.core.serializers.json import Serializer
import time

# Create your models here.
class quote(models.Model):
    ticker = models.CharField(default = "", max_length = 8)
    ask = models.DecimalField(decimal_places = 8, max_digits = 16, default = 0.0)
    bid = models.DecimalField(decimal_places = 8, max_digits = 16, default = 0.0)
    last = models.DecimalField(decimal_places = 8, max_digits = 16, default = 0.0)
    timestamp = models.DateTimeField()

    def __str__(self):
        return self.ticker

class quoteSerializer(Serializer):
    def get_dump_object(self, obj):
        dump_object = self._current or {}
        dump_object.update({'pk': obj._get_pk_val(), 'avg': (self._current['ask'] + self._current['bid'])/2})
        return dump_object

class profile(models.Model):
    user = models.OneToOneField(settings.AUTH_USER_MODEL, blank=True, null=True)
    points = models.DecimalField(default = 1000, decimal_places = 2, max_digits = 16)
    name = models.CharField(max_length=120, default = "", blank = True)
    dailytime = models.DecimalField(default = 0, decimal_places = 0, max_digits = 16)
    dailycount = models.DecimalField(default = 0, decimal_places = 0, max_digits = 4)

    def __str__(self):
        return self.name

def profileCallback(sender, request, user, **kwargs):
    newprofile, is_created = profile.objects.get_or_create(user=user)
    newprofile.points = 2000
    newprofile.name=user.username
    newprofile.save()
    #if created:
        #profile.objects.create(user=instance)

user_logged_in.connect(profileCallback)