
## 💡 Features (Server Side)

- 🔐 Secure and modular ASP.NET Core API
- 🧾 Swagger documentation out-of-the-box
- 🔁 Upload / Download / Delete / List operations for blob containers
- 🌐 Fully integrated CORS and HTTPS redirection
- 🧰 Interface-based architecture with dependency injection
- 📂 Supports any type of file: image, video, audio, documents

## 📚 Overview

This backend provides a solid and scalable API for managing files in Azure Blob Storage.
It is built with **ASP.NET Core** using clean architecture practices and runs easily on Azure App Services.
You can plug any frontend to this API and start uploading files immediately.
Swagger is included for testing and documentation.


# 🔧 Azure Blob Manager — Backend (ASP.NET Core)

This is the **backend API** of the Azure Blob Manager — provides endpoints to upload, list, delete, and download files from Azure Blob Storage.

---

## 🛠️ Features

- 🔐 CORS configured for SPA frontend
- 📦 Azure Blob Storage Client via `BlobServiceClient`
- 🔗 REST API endpoints for file operations
- 🧾 Swagger UI available on root for easy testing

---

## 🔌 API Endpoints

```
GET    /api/BlobStorage/GetContainers
GET    /api/BlobStorage/GetBlobFiles?containerName=
POST   /api/BlobStorage/UploadBlobFile?containerName=
DELETE /api/BlobStorage/DeleteBlobFile?containerName=&fileName=
GET    /api/BlobStorage/DownloadBlobFile?containerName=&fileName=
```

---

## ⚠️ Deployment Tips (Real Life Lessons)

### ❌ Bug: Frontend can’t fetch containers
✅ FIX: Make sure CORS is enabled before middleware
```csharp
builder.Services.AddCors(...)
app.UseCors("AllowAll")
```

### 🧪 Swagger not showing?
✅ FIX: Add `app.UseSwagger()` and `app.UseSwaggerUI()` **outside of if (env.IsDevelopment)**

### ⚠️ Connection string is null?
✅ FIX:
- Check `AzureBlobConnection` in `appsettings.json`
- Or override in Azure via Application Settings

---

## ⚙️ Hosting on Azure Web App

- Deployed via GitHub Actions using OIDC + publish profile
- Artifacts zipped and deployed directly with:
```yaml
uses: azure/webapps-deploy@v3
```

---

## 📂 Project Structure

```
BlobStorageAPI/
├── Controllers/
│   └── BlobStorageController.cs
├── Interfaces/
├── Repositories/
├── Program.cs
├── appsettings.json
```

---

## 🧠 Final Thought

> **Deploying to Azure is simple — once you fight the battles**.  
> CORS, dist paths, secrets, and broken GitHub Actions are just rites of passage 😤  
> But once it's up — you’ve got a sleek full-stack cloud file manager at your command.
