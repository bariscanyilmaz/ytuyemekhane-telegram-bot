# NuGet restore
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /app


COPY *.csproj ./
RUN dotnet restore


COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .


# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ytuyemekhane-telegram-bot.dll