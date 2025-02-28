# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /5MI.BookManager

# Copy the entire solution directory into the container (including all the projects)
COPY . .

RUN dotnet restore
RUN dotnet publish -c release -o /app2 --no-restore

# run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app2
COPY --from=build /app2 ./
ENTRYPOINT ["dotnet", "5MI.BookManager.Presentation.dll"]