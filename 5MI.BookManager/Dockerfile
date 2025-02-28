# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the entire solution (you can adjust the path if your solution is in a subfolder)
COPY . ./src

RUN dotnet build -o /app
RUN dotnet publish -o /publish

# Use the official .NET runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the build output and set it as the entry point
COPY --from=build /publish .

ENTRYPOINT ["dotnet", "5MI.BookManager.dll"]
