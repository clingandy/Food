#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["RandomOrderApi/RandomOrderApi.csproj", "RandomOrderApi/"]
COPY ["RandomOrderInject/RandomOrderInject.csproj", "RandomOrderInject/"]
COPY ["RandomOrderRepositories/RandomOrderRepositories.csproj", "RandomOrderRepositories/"]
COPY ["RandomOrderCore/RandomOrderCore.csproj", "RandomOrderCore/"]
COPY ["RandomOrderServices/RandomOrderServices.csproj", "RandomOrderServices/"]
RUN dotnet restore "RandomOrderApi/RandomOrderApi.csproj"
COPY . .
WORKDIR "/src/RandomOrderApi"
RUN dotnet build "RandomOrderApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RandomOrderApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RandomOrderApi.dll"]