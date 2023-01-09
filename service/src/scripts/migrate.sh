#!/bin/bash

# moving to persistence layer
cd ../OnlineClothes.Persistence

dotnet ef migrations add $1 -s ../OnlineClothes.Api/
