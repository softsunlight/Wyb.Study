#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Wyb.Study.Http.Api/Wyb.Study.Http.Api.csproj", "src/Wyb.Study.Http.Api/"]
RUN dotnet restore "src/Wyb.Study.Http.Api/Wyb.Study.Http.Api.csproj"
COPY . .
WORKDIR "/src/src/Wyb.Study.Http.Api"
RUN dotnet build "Wyb.Study.Http.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wyb.Study.Http.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wyb.Study.Http.Api.dll"]