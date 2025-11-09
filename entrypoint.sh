#!/bin/sh
set -e

if [ "$RUN_MIGRATIONS" = "true" ]; then
 echo "Running EF migrations..."
 dotnet ef database update --project ./src/Mottracker.Infrastructure/Mottracker.Infrastructure.csproj --startup-project ./src/Mottracker.Presentation/Mottracker.Presentation.csproj
fi

exec dotnet Mottracker.Presentation.dll --urls "$ASPNETCORE_URLS" -e "$ASPNETCORE_ENVIRONMENT"