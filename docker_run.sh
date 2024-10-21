#!/usr/bin/sh

docker kill "restfulEncoders" > /dev/null;
docker run --rm --name "restfulEncoders"  -d -p 5140:5140/tcp restfulencoders:latest
