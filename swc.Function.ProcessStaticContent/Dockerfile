#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["swc.Function.ProcessStaticContent/swc.Function.ProcessStaticContent.csproj", "swc.Function.ProcessStaticContent/"]
RUN dotnet restore "swc.Function.ProcessStaticContent/swc.Function.ProcessStaticContent.csproj"
COPY . .
WORKDIR "/src/swc.Function.ProcessStaticContent"
RUN dotnet build "swc.Function.ProcessStaticContent.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "swc.Function.ProcessStaticContent.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "swc.Function.ProcessStaticContent.dll"]