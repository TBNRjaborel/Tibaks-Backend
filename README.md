# 🚀 Backend Setup (ASP.NET Core Web API)

This guide explains how to run the backend locally.

---

## ✅ Prerequisites

Make sure you have installed:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- PostgreSQL (local or cloud-hosted)
- Any IDE of your choice (e.g., **Visual Studio**, **Rider**, or **VS Code**)

---

## 🛠️ Setup Instructions

### 1️⃣ Restore Dependencies

```sh
dotnet restore
```

### 2️⃣ Apply Migrations (if using Entity Framework Core)

```sh
dotnet ef database update
```

---

## 🔐 Configure `secrets.json`

Instead of hardcoding sensitive data, use **User Secrets**:

Run this in the project directory:

```sh
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=YOUR_DB_NAME;Username=YOUR_USERNAME;Password=YOUR_PASSWORD;SSL Mode=Allow;Trust Server Certificate=true"
dotnet user-secrets set "Jwt:Key" "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
dotnet user-secrets set "Jwt:Issuer" "MyApp"
dotnet user-secrets set "Jwt:Audience" "MyAppClient"
dotnet user-secrets set "Jwt:AccessTokenExpirationMinutes" "15"
dotnet user-secrets set "Jwt:RefreshTokenExpirationDays" "7"
```

### ❌ If you prefer a manual `secrets.json`, it should look like this:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=xxxxx;Username=xxxxx;Password=xxxxx;SSL Mode=Allow;Trust Server Certificate=true"
  },
  "Jwt": {
    "Key": "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
    "Issuer": "MyApp",
    "Audience": "MyAppClient",
    "AccessTokenExpirationMinutes": 15,
    "RefreshTokenExpirationDays": 7
  }
}
```

> 🔁 Replace all `xxxxx` values with **your own credentials**.  
> 🚫 **Never commit real credentials to GitHub!**

---

## ▶️ Run the Project

```sh
dotnet run
```

or in **Visual Studio**: press **F5** / click **Run ▶️**

---

## ✅ API Ready!

Once running, the API should be available at:

```
https://localhost:7108
```
