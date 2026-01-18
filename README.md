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

yaml
Kodu kopyala

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

---

## 📄 API Response Standartları

### Başarılı Response
```json
{
  "id": 1,
  "name": "Electronics"
}
Hatalı Response (Global Exception)
json
Kodu kopyala
{
  "statusCode": 404,
  "title": "Not Found",
  "message": "Category not found",
  "traceId": "00-4bf92f3577b34da6a3ce929d0e0e4736",
  "timestamp": "2026-01-18T12:30:00Z"
}
🔎 Pagination & Filtering Örneği
http
Kodu kopyala
GET /api/products?page=1&pageSize=10&search=iphone&minPrice=1000&maxPrice=50000
Response:

json
Kodu kopyala
{
  "items": [],
  "page": 1,
  "pageSize": 10,
  "totalCount": 125
}
🗄️ Veritabanı (SQLite)
EF Core Code First yaklaşımı kullanılır

SQLite dosya tabanlıdır

Ek kurulum gerektirmez

Varsayılan bağlantı:

powershell
Kodu kopyala
Data Source=products.db
⚙️ Çalıştırma
1️⃣ Bağımlılıkları Yükle
bash
Kodu kopyala
dotnet restore
2️⃣ Veritabanını Oluştur / Güncelle
bash
Kodu kopyala
dotnet ef database update
3️⃣ Uygulamayı Başlat
bash
Kodu kopyala
dotnet run
Swagger UI:

bash
Kodu kopyala
https://localhost:5001/swagger
🔐 Configuration
Environment bazlı yapılandırma kullanılır:

appsettings.json

appsettings.Development.json

Hassas bilgiler ve log dosyaları repository’ye dahil edilmez.

🧪 Best Practices
Controller’da business logic yok

Repository’de validation yok

Exception’lar controller’da try/catch ile yakalanmaz

Global Exception Middleware kullanılır

Log klasörü .gitignore ile hariç tutulur

API contract DTO ile korunur

📌 Notlar
Bu proje:

Gerçek dünya senaryoları düşünülerek yazılmıştır

Portfolio ve teknik mülakatlar için uygundur

SQL Server / PostgreSQL’e kolayca taşınabilir yapıdadır

👤 Author
G
Software Engineer
ASP.NET Core
