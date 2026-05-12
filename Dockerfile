# Utiliser l'image de base .NET 8.0 runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

# Utiliser l'image SDK pour la compilation
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier les fichiers de projet
COPY ["echec-poo.csproj", "./"]
RUN dotnet restore "echec-poo.csproj"

# Copier le code source
COPY . .

# Compiler l'application
RUN dotnet build "echec-poo.csproj" -c Release -o /app/build

# Publier l'application
FROM build AS publish
RUN dotnet publish "echec-poo.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Image finale
FROM base AS final
WORKDIR /app

# Copier les fichiers publiés
COPY --from=publish /app/publish .

# Créer un utilisateur non-root pour la sécurité
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

# Point d'entrée
ENTRYPOINT ["dotnet", "echec-poo.dll"]

