# Étape 1 : Construire l'application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /5MI.BookManager

# Copier les fichiers du projet et restaurer les dépendances
COPY . .
RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

# Étape 2 : Créer l'image d'exécution
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

# Copier les fichiers publiés depuis l'étape de build
COPY --from=build /app ./

# Définir le point d'entrée de l'application
ENTRYPOINT ["dotnet", "5MI.BookManager.Presentation.dll"]