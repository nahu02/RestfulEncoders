# Generated using Docker for Visual Studio Code

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5140

ENV ASPNETCORE_URLS=http://+:5140

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release

# See https://github.com/nahu02/EncoderHubCore/blob/main/Doc/UsingTheNugetPackageFromGithub.md
ARG GITHUB_USERNAME
ARG GITHUB_TOKEN

WORKDIR /src
COPY ["RestfulEncoders.csproj", "./"]

# Add GitHub Packages as a NuGet source for EncoderHub package
RUN dotnet nuget add source --username ${GITHUB_USERNAME} --password ${GITHUB_TOKEN} --store-password-in-clear-text --name github "https://nuget.pkg.github.com/nahu02/index.json"

RUN dotnet restore "RestfulEncoders.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RestfulEncoders.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "RestfulEncoders.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Check if .env file exists before copying it, so the image won't fail if no .env file is present
RUN [ -f ".env" ] && COPY .env .

ENTRYPOINT ["dotnet", "RestfulEncoders.dll"]
