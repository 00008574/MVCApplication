#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CW1_MVCApplication_8574/CW1_MVCApplication_8574.csproj", "CW1_MVCApplication_8574/"]
RUN dotnet restore "CW1_MVCApplication_8574/CW1_MVCApplication_8574.csproj"
COPY . .
WORKDIR "/src/CW1_MVCApplication_8574"
RUN dotnet build "CW1_MVCApplication_8574.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CW1_MVCApplication_8574.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CW1_MVCApplication_8574.dll"]