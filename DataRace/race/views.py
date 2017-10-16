"""
Definition of views.
"""

from datetime import datetime
from django.core import serializers
from django.http import HttpRequest, JsonResponse, HttpResponse
from django.middleware.csrf import get_token
from django.shortcuts import render
from django.template import RequestContext
from django.views import generic
import race.models as models
import json
from django.contrib.auth.decorators import login_required

quoteSerializer = models.quoteSerializer()

def home(request):
	"""Renders the home page."""
	assert isinstance(request, HttpRequest)
	return render(
		request,
		'index.html',
		{
			#csrf_token = get_token(request),
			#'quotes': quoteSerializer.serialize(models.quote.objects.all()),
		}
	)

def AJAXrace(request):
	if request.method == 'POST' and request.is_ajax():
		return JsonResponse({'status':'success','data':quoteSerializer.serialize([models.quote.objects.get(ticker=request.POST['ticker'])])})
	return JsonResponse({'status':'fail','ajax':request.is_ajax(),'data':quoteSerializer.serialize([models.quote.objects.get(ticker='USD/CAD')])})

