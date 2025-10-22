# Istiqbal - Hotel Management System 🏨

A complete Hotel Management System built with .NET 9 for the backend and React for the frontend, designed to manage hotel operations efficiently.

## Project Idea

This system simplifies room, guest, and reservation management, with room statuses like Booked, Under Cleaning, and Under Maintenance. It demonstrates the ability to build secure, maintainable, and scalable backend systems.
## Key Features & Benefits

Based on the file structure, the project seems to encompass the following functionalities:

*   **Room Management:** Functionality to manage room details, statuses, and types.
*   **Reservation Management:** Features for handling reservations.
*   **Guest Management:** Management of guest information.
*   **Authentication:** Secure authentication mechanisms (likely user login/registration).
*   **Amenity Management:** Management of amenities offered.
*   **Frontend Interface:** A React-based frontend for user interaction.

## Prerequisites & Dependencies

Before running Istiqbal, ensure you have the following installed:

*   **Node.js:** (Version specified in `FrontEnd/package.json` or later is recommended)
*   **npm** or **yarn:** Package managers for JavaScript dependencies.
*   **C# SDK:** For building and running the backend API.
*   **.NET Runtime:** Required runtime version for the C# backend.
*   **Vite:**  A build tool for React (installed via npm/yarn)
*   **TypeScript:** Ensure you have TypeScript installed globally or as a dev dependency.
*   **Tailwind CSS:** CSS framework used for styling the frontend.

## Installation & Setup Instructions

Follow these steps to install and set up Istiqbal:

### Backend (C# API)

1.  **Navigate to the Backend Directory:**
    ```bash
    cd BackEnd/Istiqbal.Api
    ```

2.  **Restore Dependencies:**
    ```bash
    dotnet restore
    ```

3.  **Build the Project:**
    ```bash
    dotnet build
    ```

4.  **Run the API:**
    ```bash
    dotnet run
    ```
    *Note:* You might need to configure connection strings and other environment variables.

### Frontend (React)

1.  **Navigate to the Frontend Directory:**
    ```bash
    cd FrontEnd
    ```

2.  **Install Dependencies:**
    ```bash
    npm install  # or yarn install
    ```

3.  **Start the Development Server:**
    ```bash
    npm run dev # or yarn dev
    ```
    This will typically start the application at `http://localhost:5173` (or a similar port).

## Usage Examples & API Documentation

*   **Frontend:**  The React frontend provides the user interface to interact with the application. Refer to the components and pages within the `FrontEnd/src` directory for usage patterns.
*   **Backend API:** The C# API exposes endpoints for various functionalities.  Endpoints are likely located in the Controllers such as `AmenityController.cs`, `AuthenticationController.cs`, etc.  Example API endpoints might include:
    *   `GET /api/amenities`: Retrieves a list of amenities.
    *   `POST /api/reservations`: Creates a new reservation.
    *   `POST /api/authentication/login`: User login.

    Detailed API documentation (e.g., using Swagger/OpenAPI) is not provided, but the controller files should indicate endpoint functionality.

## Configuration Options

### Backend

Configuration (e.g., database connection strings) is typically managed using `appsettings.json` or environment variables in the `BackEnd` project.

### Frontend

Environment variables for the React application are configured via `.env` files.  Vite handles these variables.  Example:

```
VITE_API_BASE_URL=http://localhost:5000/api
```

Access these variables in your React code using `import.meta.env`.

