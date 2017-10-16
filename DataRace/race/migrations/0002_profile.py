# -*- coding: utf-8 -*-
# Generated by Django 1.11.4 on 2017-10-02 22:13
from __future__ import unicode_literals

from django.conf import settings
from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
        ('race', '0001_initial'),
    ]

    operations = [
        migrations.CreateModel(
            name='profile',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('points', models.DecimalField(decimal_places=2, default=1000, max_digits=16)),
                ('name', models.CharField(blank=True, default='', max_length=120)),
                ('dailytime', models.DecimalField(decimal_places=0, default=0, max_digits=16)),
                ('dailycount', models.DecimalField(decimal_places=0, default=0, max_digits=4)),
                ('user', models.OneToOneField(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL)),
            ],
        ),
    ]