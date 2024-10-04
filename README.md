# Flight Booking Software (Working Title: Choose Your Name!)

This repository contains the source code for a flight booking software application built with Angular, .NET Core, and SQL Server. This software aims to provide a seamless and efficient platform for users to search, book, and manage flights.

## Features

*   **Central Reservation System (CRS):**
    *   Manages flight schedules, fares, and passenger information.
    *   Provides a user-friendly interface for searching and filtering flights.
*   **Inventory Control System (ICS):**
    *   Controls seat availability and manages booking requests in real-time.
    *   Prevents overbooking and ensures accurate inventory management.
*   **Departure Control System (DCS):** (Future Implementation)
    *   Handles passenger check-in, boarding, and other departure processes.
*   **User Authentication:**
    *   Securely authenticates users (customers and administrators).
    *   Implements role-based access control for different features.
*   **Real-time Updates:** (Future Implementation)
    *   Provides real-time updates on flight availability and booking status using SignalR or WebSockets.

## Technologies Used

*   **Frontend:** Angular
*   **Backend:** .NET Core 6+ (C#)
*   **Database:** SQL Server
*   **UI Library:** (e.g., Angular Material, PrimeNG, Clarity - Choose one)
*   **Authentication:** ASP.NET Core Identity or JWT
*   **Real-time Communication:** (Future) SignalR or WebSockets

## Project Structure

```
FlightOS /
├── Frontend /                       # Angular Frontend Application
|   └── Dockerfile                     # Dockerfile Angular Client
├── Backend /                        # .NET Core Web API
|   └── FlightOS.Api /                 # Api Layer
|   └── FLightOS.Domain /              # Domain Layer
|   └── FlightOS.Application /         # Applicaiton Layer
|   └── FlightOS.Infrastructure /      # Infrastructure Layer
|   └── Dockerfile                     # Dockerfile .NET Server
└── docker-compose.yml               # Docker Compose file
```

## Getting Started

### Prerequisites

*   .NET Core 6+ SDK
*   Node.js and npm
*   Angular CLI
*   SQL Server
*   (Optional) Visual Studio or VS Code

### Installation

1.  **Clone the repository:** `git clone https://github.com/your-username/flight-booking-app.git`
2.  **Backend:**
    *   Navigate to the `backend` directory.
    *   Restore NuGet packages: `dotnet restore`
    *   Update the connection string in `appsettings.json` to connect to your SQL Server database.
    *   Run database migrations to create the database schema.
    *   Start the API: `dotnet run`
3.  **Frontend:**
    *   Navigate to the `frontend` directory.
    *   Install npm packages: `npm install`
    *   Start the development server: `ng serve`

## Contributing

Contributions are welcome! Please feel free to submit pull requests for bug fixes, new features, or improvements.

## License

This project is licensed under the [MIT License](LICENSE).