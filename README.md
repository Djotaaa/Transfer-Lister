# ⚽ Transfer Lister

**Transfer Lister** is a simple ASP.NET Core project that demonstrates how to build and consume your own Web API within the same solution.  
The goal of this project is to show understanding of REST architecture, CRUD operations and data handling between an API and a client-side web application.

---

##  Overview

The application simulates a small football "transfer list" system.  
Users can view, add, update and delete player data through a simple web interface.  
All data operations (Create, Read, Update, Delete) are performed through a custom-built API made with ASP.NET Core Web API.

The project consists of two parts:

- **TransferListerAPI** – RESTful API that exposes endpoints for managing player data  
- **TransferListerWebApp** – Web client that consumes the API and provides simple UI for user interaction

---

## 🛠️ Technologies Used

- **ASP.NET Core 9.0**
- **Entity Framework Core**
- **AutoMapper**
- **Swagger / Swashbuckle**
- **Razor Pages / MVC**
- **C# 12**
- **Dependency Injection**
- **HTTP Client for API communication**

---

## 🚀 How to Run

1. **Clone the repository**
2. **Open the solution**
3. **Set startup projects**
   - Right-click the solution → *Set Startup Projects...*
   - Choose *Multiple* startup projects
   - Set both TransferListerAPI and TransferListerWebApp to Start.
4. **Run the application**

---

## 🖥️ Preview

The **Transfer Lister** web app allows users to:
- View a list of players on the “transfer list”
- Create new player
- Edit existing players
- Delete player
- Communicate with the backend through a custom-built ASP.NET Web API

> The UI is very simple, focusing on the functional demonstration of API integration and CRUD operations.
