#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["swc.Function.FetchPage/swc.Function.FetchPage.csproj", "swc.Function.FetchPage/"]
COPY ["swc.DB.PageStorage/swc.DB.PageStorage.csproj", "swc.DB.PageStorage/"]
COPY ["swc.Function.FindLinks/swc.Function.FindLinks.csproj", "swc.Function.FindLinks/"]
COPY ["swc.Function.ProcessStaticContent/swc.Function.ProcessStaticContent.csproj", "swc.Function.ProcessStaticContent/"]
RUN dotnet restore "swc.Function.FetchPage/swc.Function.FetchPage.csproj"
COPY . .
WORKDIR "/src/swc.Function.FetchPage"
RUN dotnet build "swc.Function.FetchPage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "swc.Function.FetchPage.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "swc.Function.FetchPage.dll"]
