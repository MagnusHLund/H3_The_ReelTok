#!/bin/bash

set -e

if [ ! -d "/app/H3_The_ReelTok" ]; then
  git clone https://github.com/MagnusHLund/H3_The_ReelTok.git /app/H3_The_ReelTok
else
  cd /app/H3_The_ReelTok && git pull
fi


build_and_run_api() {
  local api_dir=$1
  local csproj_file=$2
  local dll_file=$3
  local env_var=$4
  local ef_name=$5

  if [ "$env_var" = true ]; then
    echo "Building and running $dll_file..."
    cd /app/H3_The_ReelTok/"$api_dir" || exit 1
    echo "Restoring $csproj_file..."
    dotnet restore "$csproj_file"
    echo "Building $csproj_file..."
    dotnet build "$csproj_file" -c Release -o /app/build
    echo "Publishing $csproj_file..."
    dotnet publish "$csproj_file" -c Release -o /app/publish
    
    if [ "$api_dir" == "reeltok.api/reeltok.api.gateway" ]; then
      echo "Running $dll_file..."
      exec dotnet /app/publish/"$dll_file"
      exit 0 
    fi

    # If the Migrations folder does not exist, add an initial migration
    if [ ! -d "./Migrations" ]; then
      echo "No migrations found. Adding initial migration $ef_name..."
      dotnet ef migrations add "$ef_name"
    fi
    
    echo "Updating database..."
    dotnet ef database update -- --environment Production || echo "Database update encountered an error, continuing..."
    
    echo "Running $dll_file..."
    exec dotnet /app/publish/"$dll_file"
  else
    echo "Skipping $dll_file as $env_var is set to false."
  fi
}

build_and_run_api "reeltok.api/reeltok.api.auth" "AuthServiceApi.csproj" "AuthServiceApi.dll" "$RUN_API_AUTH" "InitialCreate"
build_and_run_api "reeltok.api/reeltok.api.comments" "CommentsServiceApi.csproj" "CommentsServiceApi.dll" "$RUN_API_COMMENTS" "InitialCreate"
build_and_run_api "reeltok.api/reeltok.api.gateway" "GatewayServiceApi.csproj" "GatewayServiceApi.dll" "$RUN_API_GATEWAY" "InitialCreate"
build_and_run_api "reeltok.api/reeltok.api.recommendations" "RecommendationsServiceApi.csproj" "RecommendationsServiceApi.dll" "$RUN_API_RECOMMENDATIONS" "InitialCreate"
build_and_run_api "reeltok.api/reeltok.api.users" "UsersServiceApi.csproj" "UsersServiceApi.dll" "$RUN_API_USERS" "InitialCreate"
build_and_run_api "reeltok.api/reeltok.api.videos" "VideosServiceApi.csproj" "VideosServiceApi.dll" "$RUN_API_VIDEOS" "InitialCreate"

# Sleep indefinitely if no API is to be run
sleep infinity
