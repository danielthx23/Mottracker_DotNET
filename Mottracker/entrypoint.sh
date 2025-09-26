#!/bin/bash
set -e

host="$ORACLE_HOST"
port="$ORACLE_PORT"

echo "Esperando Oracle em $host:$port..."
while ! (echo > /dev/tcp/$host/$port) >/dev/null 2>&1; do
  sleep 5
done

echo "Oracle disponível, aguardando mais 10s..."
sleep 10

echo ">> Verificando dotnet..."
which dotnet
dotnet --version

echo ">> Verificando dotnet-ef..."
which dotnet-ef
dotnet-ef --version

if [ "$RUN_MIGRATIONS" = "true" ]; then
  echo ">> Rodando migrations..."
  dotnet ef database update --project ./src/Mottracker.csproj --verbose
fi

echo ">> Iniciando aplicação..."
exec dotnet ./Mottracker.dll