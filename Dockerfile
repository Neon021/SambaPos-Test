# Use the .NET SDK as a base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the solution files into the container
COPY . .

# Build the solution
RUN dotnet restore
RUN dotnet build --configuration Release

# Expose the port your application will listen on
EXPOSE 5000

# Start the application
CMD ["dotnet", "SambaPos.Api/bin/Release/net7.0/SambaPos.Api.dll"]
