# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore ./ProfileService --disable-parallel
RUN dotnet publish ./ProfileService -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /app
Copy --from=build /app ./

EXPOSE 5009-5010

ENTRYPOINT ["dotnet", "ProfileService.Api.dll"]