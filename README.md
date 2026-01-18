# Products API

Modern ASP.NET Core mimarisi kullanılarak geliştirilmiş, **Category** ve **Product** yönetimi sağlayan,  
katmanlı, ölçeklenebilir ve production yaklaşımıyla tasarlanmış bir RESTful Web API projesidir.

Veritabanı olarak **SQLite** kullanılmaktadır. Bu sayede proje:
- Hafif
- Kolay kurulabilir
- Local geliştirme ve demo ortamları için idealdir

---

## 🚀 Teknoloji Stack’i

- **.NET 8 / ASP.NET Core Web API**
- **Entity Framework Core**
- **SQLite**
- **Repository & Service Pattern**
- **DTO Pattern**
- **Global Exception Handling**
- **Pagination & Filtering**
- **Swagger / OpenAPI**
- **Serilog (Structured Logging)**
- **ExceptionMiddleware**
- **Custom Exceptions**

---

## 🧱 Mimari Yaklaşım

Proje **Clean Architecture** ve **SOLID prensipleri** baz alınarak tasarlanmıştır.

Products.Api
│
├── Controllers → HTTP endpoint’leri
├── Services → Business logic
├── Repositories → Data access (EF Core)
├── Dtos → Request / Response modelleri
├── Exceptions → Custom exception’lar
├── Responses → Standart API response modelleri
└── Middlewares → Global exception & logging


### Katman Sorumlulukları

| Katman | Sorumluluk |
|------|-----------|
| Controller | HTTP istek/cevap yönetimi |
| Service | İş kuralları |
| Repository | Veritabanı erişimi |
| DTO | API contract |
| Middleware | Logging, exception handling |

---

## 📦 Özellikler

### ✅ Category Management
- CRUD işlemleri
- REST standartlarına uygun response’lar

### ✅ Product Management
- CRUD işlemleri
- **Pagination**
- **Filtering**
  - Search
  - Min / Max Price

### ✅ Global Exception Handling
- Tüm exception’lar merkezi olarak yakalanır
- Client’a **tek tip hata response’u** döner

### ✅ Logging Strategy
- ASP.NET Core logging altyapısı **Serilog** ile yönetilir
- Request bazlı structured logging
- Production uyumlu yapı
