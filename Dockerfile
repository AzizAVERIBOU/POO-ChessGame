# Image runtime .NET 8
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

ARG TTYD_VERSION=1.7.7
RUN apt-get update && apt-get install -y --no-install-recommends ca-certificates curl \
    && ARCH=$(dpkg --print-architecture) \
    && case "${ARCH}" in \
         amd64) TTYD_ARCH=x86_64 ;; \
         arm64) TTYD_ARCH=aarch64 ;; \
         *) echo "Architecture non supportee: ${ARCH}" >&2; exit 1 ;; \
       esac \
    && curl -fsSL "https://github.com/tsl0922/ttyd/releases/download/${TTYD_VERSION}/ttyd.${TTYD_ARCH}" \
         -o /usr/local/bin/ttyd \
    && chmod +x /usr/local/bin/ttyd \
    && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["echec-poo.csproj", "./"]
RUN dotnet restore "echec-poo.csproj"

COPY . .
RUN dotnet build "echec-poo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "echec-poo.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENV TERM=xterm-256color

RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

EXPOSE 7681

ENTRYPOINT ["ttyd", "-W", "-p", "7681", "dotnet", "echec-poo.dll"]
