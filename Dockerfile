FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# RUN dotnet tool install --global dotnet-ef
# ENV PATH="$PATH:/root/.dotnet/tools"
WORKDIR /src
COPY ["Product.Microservice.csproj", "./"]
RUN dotnet restore "./Product.Microservice.csproj"
COPY . .
RUN dotnet build "Product.Microservice.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Product.Microservice.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
# RUN dotnet ef migrations add Product
# RUN dotnet ef database update
ENTRYPOINT ["dotnet", "Product.Microservice.dll"]


