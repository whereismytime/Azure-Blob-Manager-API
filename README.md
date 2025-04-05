# Azure Blob Manager – Backend (ASP.NET Core)

## 🧠 Architecture

- ASP.NET Core Web API
- Azure Blob Storage SDK via `Azure.Storage.Blobs`
- Repository pattern with interface abstraction
- Swagger integration for API testing
- CORS support for client requests
- Environment-based configuration via `appsettings.json`

---

## 📌 API Endpoints

| Method | Endpoint                       | Description                        |
|--------|--------------------------------|------------------------------------|
| GET    | /api/GetContainers             | Returns list of blob containers   |
| GET    | /api/GetBlobFiles?container=X  | Returns blob file names           |
| GET    | /api/DownloadBlobFile          | Downloads file by container+name  |

All endpoints are defined in `BlobStorageController.cs`.

---

## ⚙️ Deployment Instructions (Azure App Service)

1. Configure **Azure App Service**
2. Set connection string `AzureBlobConnection` in App Settings
3. Deploy using:
```bash
dotnet publish -c Release
az webapp deployment source config-zip --src ./publish.zip ...
```
4. Or use GitHub Actions workflow.

---

## 🔐 Environment Variables

Set in Azure App Service → Configuration:

```
AzureBlobConnection = DefaultEndpointsProtocol=...;AccountKey=...
```

---

## 🔄 Client Integration

- Accepts cross-origin requests from Azure Static Web App.
- Axios requests from frontend routed to this API with absolute URL.

---

## 🚀 Extensibility Ideas

- Add POST /api/UploadBlob
- Add DELETE /api/DeleteBlob
- Add authentication (JWT / OAuth)
- Add logging and monitoring (e.g., AppInsights)
