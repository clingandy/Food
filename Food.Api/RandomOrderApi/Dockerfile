FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["RandomOrderApi/RandomOrderApi.csproj", "RandomOrderApi/"]
COPY ["RandomOrderInject/RandomOrderInject.csproj", "RandomOrderInject/"]
COPY ["RandomOrderRepositories/RandomOrderRepositories.csproj", "RandomOrderRepositories/"]
COPY ["RandomOrderCore/RandomOrderCore.csproj", "RandomOrderCore/"]
COPY ["RandomOrderServices/RandomOrderServices.csproj", "RandomOrderServices/"]
RUN dotnet restore "RandomOrderApi/RandomOrderApi.csproj"
COPY . .
WORKDIR "/src/RandomOrderApi"
RUN dotnet build "RandomOrderApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RandomOrderApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RandomOrderApi.dll"]