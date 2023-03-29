#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
RUN ls
COPY ["/BooksMgmt.API/BooksMgmt.API.csproj", "BooksMgmt.API/"]
COPY ["/BooksMgmt.Data/BooksMgmt.Data.csproj", "BooksMgmt.Data/"]
COPY ["/BooksMgmt.Domain/BooksMgmt.Domain.csproj", "BooksMgmt.Domain/"]
RUN dotnet restore "BooksMgmt.API/BooksMgmt.API.csproj"
COPY . .
WORKDIR "/src/BooksMgmt.API"
RUN dotnet build "BooksMgmt.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BooksMgmt.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BooksMgmt.API.dll"]