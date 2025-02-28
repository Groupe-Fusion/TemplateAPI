FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /5MI.BookManager

COPY . .

RUN dodnet restore
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet","5MI.BookManager.Presentation.dll"]