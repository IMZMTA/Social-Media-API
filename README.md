# 🚀 SocialMedia API - Scalable Backend with JWT & Refresh Tokens

A **production-ready .NET Web API** built with **Clean Architecture + CQRS**, supporting:

* 🔐 JWT Authentication (Access + Refresh Tokens)
* 🍪 HttpOnly Cookie-based Refresh Token
* 🔑 Traditional Login + OAuth (Google & GitHub - Planned)
* ⚡ High-performance modular backend design

---

# 📖 Context

This project demonstrates real-world backend concepts:

* Authentication with **Access Token (short-lived) + Refresh Token**
* Secure session handling using **HttpOnly Cookies**
* Clean Architecture with **CQRS pattern**
* Scalable API design for **social media systems**
* Extensible OAuth integration (Google & GitHub)

---

# 📑 Index

1. Features
2. Tech Stack
3. Project Structure
4. Setup Instructions
5. Configuration
6. Authentication System
7. Auth Flows (Login / Signup / Refresh)
8. OAuth Flow (Planned)
9. API Endpoints Overview
10. System Flow
11. Internal Processing Flow
12. Architecture Breakdown
13. Security Practices
14. Common Issues

---

# 📌 01. Features

✅ User Registration & Login
✅ JWT Access Token (Short-lived)
✅ Refresh Token (HttpOnly Cookie)
✅ Token Refresh Endpoint
✅ Logout with Token Invalidation
✅ Posts, Comments, Likes APIs
✅ Feed Aggregation API
✅ CQRS with MediatR
✅ FluentValidation
✅ Global Exception Middleware
✅ Standard API Response Wrapper
✅ Swagger Integration
✅ CORS Enabled

🚧 OAuth (Google & GitHub) – Planned / Extendable

---

# 🏗️ 02. Tech Stack

| Layer        | Technology                |
| ------------ | ------------------------- |
| Backend      | ASP.NET Core (.NET)       |
| Architecture | Clean Architecture + CQRS |
| ORM          | Entity Framework Core     |
| Database     | SQL / SQLite              |
| Auth         | JWT + Refresh Token       |
| Validation   | FluentValidation          |
| API Docs     | Swagger                   |
| Middleware   | Exception Handling        |

---

# 📂 03. Project Structure

```
SocialMedia.API            → Controllers, Middleware, Swagger
SocialMedia.Application    → CQRS Handlers, DTOs, Validators
SocialMedia.Infrastructure → DB, Auth, Token Service
SocialMedia.Domain         → Entities, Enums, Constants
```

---

# 🛠️ 04. Setup Instructions

## Clone Project

```bash
git clone <repo-url>
cd SocialMedia
```

---

## Restore Dependencies

```bash
dotnet restore
```

---

## Apply Migration

```bash
dotnet ef database update
```

---

## Run Application

```bash
dotnet run
```

---

## Access

* API → http://localhost:<port>
* Swagger → http://localhost:<port>/swagger

---

# ⚙️ 05. Configuration

## 🔐 JWT

```json
"Jwt": {
  "Key": "MINIMUM_32_CHAR_SECRET",
  "Issuer": "SocialMediaAPI",
  "Audience": "SocialMediaClient",
  "AccessTokenExpiryMinutes": 15,
  "RefreshTokenExpiryDays": 7
}
```

---

## 🍪 Cookie Settings

* HttpOnly → ✅
* Secure → ✅ (Production)
* SameSite → Strict / Lax

---

## 🗄️ Database

```json
"ConnectionStrings": {
  "DefaultConnection": "Your_DB_Connection"
}
```

---

# 🔐 06. Authentication System

This project uses **dual-token authentication**:

### 1️⃣ Access Token (JWT)

* Short-lived (e.g., 15 mins)
* Sent in Authorization header
* Used for API access

---

### 2️⃣ Refresh Token

* Long-lived (stored in DB)
* Stored in **HttpOnly Cookie**
* Used to generate new access tokens

---

# 🔄 07. Auth Flows

## 🧾 Signup Flow

1. User registers via `/api/v1/users`
2. User stored in DB
3. No token issued (or optional auto-login)

---

## 🔐 Login Flow

1. Call:

```
POST /api/v1/auth/login
```

2. Backend:

* Validate credentials
* Generate:

  * Access Token (JWT)
  * Refresh Token
* Store refresh token in DB
* Send refresh token via **HttpOnly Cookie**

---

## 🔁 Refresh Token Flow

```
POST /api/v1/auth/refresh
```

Flow:

1. Read refresh token from cookie
2. Validate against DB
3. Generate new access token
4. Return new JWT

---

## 🚪 Logout Flow

```
POST /api/v1/auth/logout
```

* Invalidate refresh token
* Clear cookie

---

# 🔐 08. OAuth Flow (Planned)

🚧 Not fully implemented yet — but architecture supports it.

Future support:

## Providers:

* Google OAuth
* GitHub OAuth

---

## Expected Flow:

1. User clicks login with provider
2. Redirect to provider
3. Callback returns user info
4. Match user via:

```
Email + Provider + ProviderId
```

5. If user exists → login
6. Else → auto register
7. Generate JWT + Refresh Token

---

## 🧠 User Identity Strategy

| Field      | Purpose                 |
| ---------- | ----------------------- |
| Email      | Unique user identity    |
| Provider   | Google / GitHub / Local |
| ProviderId | External auth ID        |

---

# 🔌 09. API Endpoints Overview

## 🔐 Auth

* POST `/api/v1/auth/login`
* POST `/api/v1/auth/refresh`
* POST `/api/v1/auth/logout`

---

## 👤 Users

* POST `/api/v1/users` → Register
* GET `/api/v1/users`
* GET `/api/v1/users/{id}`
* PUT `/api/v1/users/{id}`
* DELETE `/api/v1/users/{id}`

---

## 📝 Posts

* POST `/api/v1/posts`
* GET `/api/v1/posts`
* GET `/api/v1/posts/{id}`
* PUT `/api/v1/posts/{id}`
* DELETE `/api/v1/posts/{id}`

---

## 💬 Comments

* POST `/api/v1/comments`
* PUT `/api/v1/comments`
* DELETE `/api/v1/comments/{id}`
* GET `/api/v1/comments/{id}`

---

## ❤️ Likes

* POST `/api/v1/likes/{postId}`
* GET `/api/v1/likes/{postId}`
* DELETE `/api/v1/likes/{likeId}`

---

## 📰 Feed

* GET `/api/v1/feed`

---

# 🔄 10. System Flow

```
Client → Controller → CQRS → Service → DB → Response
```

---

# 🔁 11. Internal Processing Flow

1. Request hits Controller
2. Routed to CQRS Handler
3. Validation via FluentValidation
4. JWT extracted
5. User resolved
6. Business logic executed
7. Response mapped
8. Standard API response returned

---

# 🧱 12. Architecture Breakdown

### 🟦 API Layer

* Controllers
* Middleware
* Swagger

---

### 🟩 Application Layer

* CQRS Handlers
* DTOs
* Validators

---

### 🟨 Domain Layer

* Entities
* Core Rules

---

### 🟥 Infrastructure Layer

* Database
* Token Service
* Auth Logic

---

# 🛡️ 13. Security Practices

✅ JWT Access Token (short expiry)
✅ Refresh Token rotation
✅ HttpOnly Cookie storage
✅ No sensitive data exposure
✅ Input validation
✅ Global exception handling
✅ CORS protection

---

# ❗ 14. Common Issues

### ❌ Unauthorized (401)

* Access token expired → use refresh endpoint

---

### ❌ Refresh not working

* Cookie not sent (check browser / CORS)
* Token expired or invalid

---

### ❌ Login fails

* Invalid credentials
* User not registered

---

# 📎 Conclusion

A **modern social media backend API** showcasing:

* 🔐 Secure authentication (JWT + Refresh Tokens)
* 🧱 Clean Architecture with CQRS
* ⚡ Scalable and maintainable design

Ready for real-world applications and extensible for OAuth 🚀
