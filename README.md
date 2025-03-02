
---

# 🏨 Resort Booking System – Clean Architecture  

Welcome to the **Resort Booking System**! 🏝️ This project is a web-based application built using **.NET 8**, following the **Clean Architecture** principles. It ensures separation of concerns, scalability, and maintainability. It also features **Stripe Payment Integration** 💳 for secure transactions.  

## 🚀 Features  
- 🏠 **Villa Management** – Add, update, and remove villas.  
- 📅 **Booking System** – Manage reservations efficiently.  
- 💰 **Stripe Integration** – Secure payment processing.  
- 🔐 **Authentication & Authorization** – Role-based access control.  
- 📊 **DataTables Integration** – Better data visualization.  
- 🏗 **Clean Architecture** – Layered structure for maintainability.  

## 🏛 Clean Architecture Layers  
The project follows **Clean Architecture** with the following structure:  

```
📂 ResortBookingSystem
 ┣ 📂 Application   # Business logic (Use Cases, DTOs, Interfaces) 🏗
 ┣ 📂 Domain        # Core entities & business rules 📜
 ┣ 📂 Infrastructure # Data access, external APIs (Stripe, EF Core) ⚙️
 ┣ 📂 WebUI         # ASP.NET Core MVC frontend 🎨
```

## 🛠 Tech Stack  
- ⚙️ **.NET 8**  
- 🎨 **ASP.NET Core MVC** (WebUI)  
- 🏗 **Entity Framework Core** (Infrastructure)  
- 🔥 **Stripe API** (Payment Integration)  
- 🛡 **Identity Authentication**  
- 🗄 **SQL Server** (Database)  

## 📦 Installation  

1. Clone the repository 📂  
   ```bash
   git clone https://github.com/MahmoodElbadri/Resort.git
   cd Resort
   ```

2. Install dependencies 🏗  
   ```bash
   dotnet restore
   ```

3. Apply database migrations 🗄  
   ```bash
   dotnet ef database update
   ```

4. Set up **Stripe API Keys** 🔑  
   Update `appsettings.json` with your Stripe **Publishable Key** and **Secret Key**.

5. Run the project 🚀  
   ```bash
   dotnet run
   ```

## 💳 Stripe Integration  
The checkout process is fully integrated with **Stripe** for handling secure payments. Ensure you have the correct API keys set up in `appsettings.json`:  

```json
"Stripe": {
  "PublishableKey": "your_publishable_key_here",
  "SecretKey": "your_secret_key_here"
}
```

## 📷 Screenshots  
🚧 *Coming soon...* 🚧  

## 🎯 Future Enhancements  
- 📩 **Email Notifications** for bookings.  
- 📍 **Google Maps API** for villa locations.  
- 📈 **Admin Dashboard** with analytics.  

## 🤝 Contributing  
Pull requests are welcome! Feel free to fork the repo and submit improvements. 🚀  

## 📜 License  
This project is licensed under the MIT License. 📝  

---
