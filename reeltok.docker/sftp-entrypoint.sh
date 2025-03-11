#!/bin/sh

chown root:root /home/VideosService
chmod 755 /home/VideosService

chown root:root /home/UsersService
chmod 755 /home/UsersService

chown 1001:101 /home/VideosService/videos
chmod 770 /home/VideosService/videos

chown 1002:101 /home/UsersService/profiles
chmod 770 /home/UsersService/profiles

exec /entrypoint "$@"
