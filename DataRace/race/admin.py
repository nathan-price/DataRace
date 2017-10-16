from django.contrib import admin
from .models import *

# Register your models here.
class quoteAdmin(admin.ModelAdmin):
    class Meta:
        model = quote
admin.site.register(quote, quoteAdmin)

class profileAdmin(admin.ModelAdmin):
    class Meta:
        model = profile

admin.site.register(profile, profileAdmin)