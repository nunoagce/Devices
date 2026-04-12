# Devices

## Debugging

### Option 1: Database in Docker + API in Visual Studio (Development)

For debugging with the API running locally in Visual Studio:

1. **Start the database in Docker**
```powershell
docker-compose up -d db
```

Or run the specific database container command for your setup.

2. **Run the API in Visual Studio**
- Press `F5` or click **Debug > Start Debugging** in Visual Studio
- The API will typically run on a local port (default: `https://localhost:5001` or `http://localhost:5000`)

3. **Find the API Port**
- Check the Visual Studio **Debug** output window for the port information
- Or look at the **Output** window (__View > Output__) and search for the listening port
- The console will display: `Now listening on: http://localhost:<PORT>`

4. **Use the .http files with dev-visual-studio configuration**
- Open any `.http` file in the API folder
- Select the `dev-visual-studio` configuration from the dropdown
- Update the port if necessary to match what Visual Studio is using
- Execute your HTTP requests

### Option 2: Everything in Docker (Release Build)

For a complete containerized deployment with release build:

1. **Run all services with Docker**
```powershell
docker-compose up
```

3. **Use the .http files with release-docker configuration**
   - Open any `.http` file in the API folder
   - Select the `release-docker` configuration from the dropdown
   - Execute your HTTP requests

This configuration uses a release mode build and all services run in containers.
