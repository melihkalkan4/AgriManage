# AgriManage 🌱

**AgriManage**, modern greenhouse operations management system designed to optimize agricultural productivity through digitalization. This project represents a transition from a monolithic architecture to a scalable **Microservices** architecture, incorporating data analysis and decision support mechanisms.

![AgriManage Status](https://img.shields.io/badge/Status-In%20Development-green)
![.NET](https://img.shields.io/badge/.NET-6.0%2F7.0-purple)
![Architecture](https://img.shields.io/badge/Architecture-Microservices-blue)

## 🚀 Project Overview

AgriManage aims to solve the complexity of tracking daily greenhouse operations. It allows facility managers to handle personnel shifts, equipment maintenance, crop planning, and inventory tracking from a single unified dashboard.

### Key Features

* **📈 Data Analysis & Reporting:** Visualizing crop yields and operational efficiency.
* **🛠 Equipment Maintenance (Bakım):** Scheduling and tracking maintenance logs for greenhouse machinery to prevent downtime.
* **busts_in_silhouette: Shift Management (Vardiya):** Organizing staff schedules and workforce allocation.
* **📅 Production Planning (PlanOluştur):** Strategic planning for planting and harvesting cycles.
* **📦 Inventory Service:** Microservice dedicated to tracking stock levels of seeds, fertilizers, and tools.
* **🏠 Greenhouse Service:** Microservice dedicated to managing specific greenhouse unit conditions.

## 🏗 Architecture

The project is built on the **.NET Ecosystem** and implements a **Microservices Architecture** to ensure scalability and maintainability.

* **AgriManage.WebApp:** The core MVC web application serving the user interface (Razor Views).
* **AgriManage.Service.Inventory:** Independent service handling stock logic.
* **AgriManage.Service.Greenhouse:** Independent service handling greenhouse environment logic.
* **Database:** MSSQL with Entity Framework Core (Code-First approach).

## 🛠 Tech Stack

* **Backend:** C#, .NET Core / .NET Framework, ASP.NET MVC
* **Frontend:** HTML5, CSS3, Bootstrap 5, JavaScript
* **Data Access:** Entity Framework Core
* **Tools:** Visual Studio, Git, SQL Server

## ⚙️ Getting Started

Follow these instructions to get a copy of the project up and running on your local machine.

### Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download) installed.
* SQL Server (LocalDB or full instance).
* Visual Studio 2022 (recommended).

### Installation

1.  **Clone the repository**
    ```bash
    git clone [https://github.com/melihkalkan4/AgriManage.git](https://github.com/melihkalkan4/AgriManage.git)
    ```

2.  **Navigate to the project directory**
    ```bash
    cd AgriManage
    ```

3.  **Restore Dependencies**
    ```bash
    dotnet restore
    ```

4.  **Update Database (Entity Framework)**
    Open the Package Manager Console in Visual Studio and run:
    ```powershell
    Update-Database
    ```

5.  **Run the Application**
    Set `AgriManage.WebApp` as the startup project and press `F5` or run:
    ```bash
    dotnet run --project AgriManage.WebApp
    ```

## 📸 Screenshots

*(You can add screenshots of your dashboard here)*

## 🤝 Contributing

Contributions are always welcome!
1.  Fork the project
2.  Create your feature branch (`git checkout -b feature/NewFeature`)
3.  Commit your changes (`git commit -m 'Add some NewFeature'`)
4.  Push to the branch (`git push origin feature/NewFeature`)
5.  Open a Pull Request

## 📝 License

This project is licensed under the MIT License.

---
**Developer:** [Melih Kalkan](https://github.com/melihkalkan4)
