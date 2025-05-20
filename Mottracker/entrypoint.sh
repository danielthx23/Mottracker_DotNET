#!/bin/bash
set -e

if [ "$RUN_MIGRATIONS" = "true" ]; then
  echo ">> Rodando migrations..."
  dotnet ef database update --project Mottracker.csproj
fi

echo ">> Iniciando aplicação..."
exec dotnet /app/publish/Mottracker.dll
