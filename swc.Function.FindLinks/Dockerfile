#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["swc.Function.FindLinks/swc.Function.FindLinks.csproj", "swc.Function.FindLinks/"]
COPY ["swc.DB.PageStorage/swc.DB.PageStorage.csproj", "swc.DB.PageStorage/"]
RUN dotnet restore "swc.Function.FindLinks/swc.Function.FindLinks.csproj"
COPY . .
WORKDIR "/src/swc.Function.FindLinks"
RUN dotnet build "swc.Function.FindLinks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "swc.Function.FindLinks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "swc.Function.FindLinks.dll"]
