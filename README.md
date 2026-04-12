# 🏦 Bank Management System (Desktop Application)
> A C# Windows Forms banking system built with 3-tier architecture, SQL Server, ADO.NET, password hashing, and Event Viewer integration.
### Three-Layer Architecture (Presentation • Business Logic • Data Access)

---

## 📘 Overview

**Bank Management System (Desktop Application)** is a **C# Windows Forms** application developed to manage core banking operations such as **clients, accounts, users, and financial transactions**, following a **three-layer architecture** for better maintainability, scalability, and separation of concerns.

The system simulates a real banking workflow and provides a structured environment for handling account operations securely and efficiently using **SQL Server**, **ADO.NET**, **password hashing**, and **Event Viewer integration**.

---

## 🏗️ Architecture Overview

This project follows the **3-tier architecture**:

1. **Presentation Layer (PL / UI)**
   - Built with **Windows Forms**.
   - Provides user interfaces for login, client management, account management, transactions, and user operations.

2. **Business Logic Layer (BLL)**
   - Contains business rules, validation, and core system logic.
   - Acts as the connection between the UI and the data layer.

3. **Data Access Layer (DAL)**
   - Handles all communication with the **SQL Server** database using **ADO.NET**.
   - Responsible for CRUD operations and data retrieval.

---

## ✨ Features

### 👤 Client Management
- Add new clients
- Update client information
- View client details
- Search and manage client records

### 💳 Account Management
- Create and update bank accounts
- View account information
- Manage account-related operations

### 💸 Transactions
- **Deposit**
- **Withdraw**
- **Transfer**

### 👥 User Management
- Add and update users
- View user details
- Change user passwords
- Manage login credentials

### 🔐 Authentication & Security
- Secure login system
- Password storage using **hashing**
- Input validation to reduce invalid data entry
- Integrated **Event Viewer logging** for monitoring important system events

### 🧾 Personal Information Management
- Add and update personal information
- Reusable UI components for displaying filtered and detailed records

---

## 🧰 Technologies Used

- **C#**
- **.NET Framework**
- **Windows Forms**
- **SQL Server**
- **ADO.NET**
- **3-Tier Architecture**
- **Event Viewer**
- **Hashing**

---

## 🗂️ Project Structure

The application is organized into functional modules such as:

- **Accounts**
- **Clients**
- **Login**
- **Personal Information**
- **Transactions**
- **Users**
- **Global Classes / Utilities**

This modular structure improves readability, maintainability, and future scalability.

---

## 🔒 Security & Reliability

This project includes several practices to improve security and system reliability:

- **Password Hashing** for more secure credential handling
- **Input Validation** to prevent invalid data entry
- **Event Viewer Integration** for logging important application events
- **Separation of Concerns** through 3-layer architecture

---

## 🗃️ Database

- The system uses **SQL Server** as the backend database.
- Database access is implemented using **ADO.NET**.
- The database supports entities related to:
  - Clients
  - Accounts
  - Users
  - Transactions

---

## ▶️ Getting Started

### Requirements
- **Visual Studio**
- **SQL Server**
- **.NET Framework**
- Configured database

### Run Steps
1. Clone the repository.
2. Open the solution in **Visual Studio**.
3. Restore any required packages.
4. Configure the database connection.
5. Build and run the project.

---

## 📸 Screenshots

Add screenshots here to showcase the application, for example:

### Login Screen
<img width="787" height="488" alt="image" src="https://github.com/user-attachments/assets/9d164e82-0c17-458d-b17d-31dfec9bb4d3" />

### Manage Clients
<img width="1366" height="769" alt="image" src="https://github.com/user-attachments/assets/42bd7631-293e-43a1-bbfd-5efdcf912abf" />

### Manage Accounts
<img width="1361" height="769" alt="image" src="https://github.com/user-attachments/assets/1a618211-b442-45c1-826e-61bb9a4b3b2c" />

### Transactions Module
<img width="1367" height="769" alt="image" src="https://github.com/user-attachments/assets/97f98145-862a-4459-8138-eaf955e713c8" />

### User Management
<img width="1365" height="764" alt="image" src="https://github.com/user-attachments/assets/21892b3b-97b7-4ac6-b182-589942dcca88" />

---

## 🎯 Purpose

This project was built to strengthen practical skills in:

- **Desktop application development with C#**
- **3-Tier Architecture**
- **Database integration with SQL Server and ADO.NET**
- **Authentication and security**
- **Logging and monitoring with Event Viewer**
- **Business-oriented application design**

---

## 👨‍💻 Author

**Ahmed Najib**  
GitHub: [ahmednajib](https://github.com/ahmednajib)
