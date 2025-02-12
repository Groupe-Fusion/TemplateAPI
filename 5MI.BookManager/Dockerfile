# Étape 1 : Construire l'application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copier les fichiers du projet et restaurer les dépendances
COPY *.csproj .
RUN dotnet restore

# Copier le reste des fichiers de l'application
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Étape 2 : Créer l'image d'exécution
FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app

# Copier les fichiers publiés depuis l'étape de build
COPY --from=build /app/publish .

# Définir le point d'entrée de l'application
ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]
