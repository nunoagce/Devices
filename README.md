# Devices
# Devices API

This project supports multiple development and deployment workflows. Choose the option that best fits your current task.

## Prerequisites

.NET 10 SDK
Docker Desktop
	
## How to run

### Option 1: Hybrid (DB in Docker + API in Visual Studio)

Best for active coding and step-by-step debugging.

1. **Start the database in Docker**
```powershell 
docker-compose up -d db
```

2. **Run the API in Visual Studio**
- Press `F5` or click **Debug > Start Debugging** in Visual Studio
- The API will typically run on a local port (default: `http://localhost:32698`)

3. **Find the API Port**
- Check the Visual Studio **Debug** output window for the port information
- Or look at the **Output** window (__View > Output__) and search for the listening port
- The console will display: `Now listening on: http://localhost:<PORT>`

4. **Use the .http files with dev-visual-studio configuration**
- Open any `.http` file in the API folder
- Select the `dev-visual-studio` configuration from the dropdown
- Update the port in the `http-client.env.json` file if necessary to match what Visual Studio is using
- Execute your HTTP requests

### Option 2: Full Containerized Build (Docker-Only)

Docker handles the compilation using the SDK image.

1. **Start both API and Database**
```powershell 
docker-compose up
```

2. **Use the .http files with release-docker configuration**
   - Open any `.http` file in the API folder
   - Select the `release-docker` configuration from the dropdown
   - Execute your HTTP requests

This configuration uses a release mode build and runs in port 8080.

### Option 3: Pre-built Optimized Deployment

Best for mimicking a CI/CD pipeline. This uses the lightweight runtime image and assumes the app is already compiled.

1. **Compile the app locally**
Run this from the root folder to generate the binaries:
```powershell 
dotnet publish ./API/API.csproj -c Release -o ./publish /p:UseAppHost=false
```

2. **Run with Release configuration**
```powershell 
docker-compose -f docker-compose.Release.yml up --build
```

3. **Use the .http files with release-docker configuration**
   - Open any `.http` file in the API folder
   - Select the `release-docker` configuration from the dropdown
   - Execute your HTTP requests

This configuration uses a release mode build and runs in port 8080.