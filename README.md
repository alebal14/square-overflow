# square-overflow
 If Minecraft and Tetris had a chaotic love child.

# Square Overflow

A full-stack application for creating and managing colorful squares with a clean architecture. This project consists of a Next.js frontend and a .NET backend API.



## Features

- Add colored squares to a dynamic grid
- Clear all squares with a single click
- Full-stack application with React frontend and .NET backend
- Swagger API documentation

## Getting Started

### Prerequisites

- Node.js 20.x or later (required for React 19 and Next.js 15)
- npm or yarn
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code with C# extensions

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/square-overflow.git
   cd square-overflow
   ```

2. Frontend Setup:
   ```bash
   cd frontend
   npm install
   # or
   yarn install
   ```

3. Backend Setup:
   ```bash
   cd SquareOverFlowApi
   dotnet restore
   ```

### Running the Application

1. Start the backend API:
   ```bash
   cd SquareOverFlowApi
   dotnet run
   ```
   The API will be available at https://localhost:7194

2. In a separate terminal, start the frontend:
   ```bash
   cd frontend
   npm run dev
   # or
   yarn dev
   ```

3. Open [http://localhost:3000](http://localhost:3000) in your browser

4. You can access the Swagger API documentation at https://localhost:7194/swagger

## Project Structure

The repository is organized into two main directories:

### Frontend (React/Next.js)

```
frontend/
├── app/                  # Main application code
│   ├── components/       # Reusable UI components
│   │   ├── errorbanner.tsx
│   │   ├── loadingspinner.tsx
│   │   ├── servererror.tsx
│   │   └── squaregrid.tsx
│   ├── styles/           # CSS modules
│   │   ├── errorbanner.module.css
│   │   ├── loadingspinner.module.css
│   │   ├── servererror.module.css
│   │   └── squarepage.module.css
│   ├── useHooks/         # Custom React hooks
│   │   └── useSquares.ts # Hook for square data management
│   ├── interfaces.tsx    # TypeScript interfaces
│   ├── layout.tsx        # Layout component
│   └── page.tsx          # Main page component
└── public/               # Static assets
```

### Backend (.NET API)

```
backend/
├──SquareOverFlowApi/
│    ├── Controllers/          # API controllers
│    │   └── SquareController.cs
│    ├── Middleware/           # Custom middleware
│    ├── Program.cs            # Application entry point
│    └── appsettings.json      # Configuration
│
├──SquareOverFlowCore/                 # Core business logic
│    ├── Models/           # Domain models
│    ├── Interfaces/       # Service interfaces
│    ├── SquareService.cs  # Square service implementation
│    └── StorageService.cs # Storage service
```

## API Architecture

### Custom Hooks

The application uses custom hooks to manage state and API interactions:

#### useSquares

This hook manages all square-related state and operations:

- `fetchSquares`: Loads squares from the API
- `addSquare`: Creates a new square
- `clearSquares`: Removes all squares

## API Endpoints

The application communicates with a .NET backend API with the following endpoints:

| Method | Endpoint     | Description                           |
|--------|-------------|---------------------------------------|
| GET    | /api/Square | Retrieves all squares from storage    |
| POST   | /api/Square | Creates a new square with unique color|
| DELETE | /api/Square | Deletes all squares from storage      |

The API is documented using Swagger UI, which can be accessed at https://localhost:7194/swagger when the backend is running.

## Technologies Used

### Frontend
- [Next.js](https://nextjs.org/) - React framework
- [TypeScript](https://www.typescriptlang.org/) - Type-safe JavaScript

### Backend
- [ASP.NET Core 8.0](https://dotnet.microsoft.com/apps/aspnet) - Web API framework
- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) - Backend language
- [Swashbuckle.AspNetCore 6.6.2](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - Swagger implementation for API

## Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/amazing-feature`
3. Commit your changes: `git commit -m 'Add some amazing feature'`
4. Push to the branch: `git push origin feature/amazing-feature`
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- WizardWorks for the original design inspiration
- The React/Next.js team for creating an amazing library
- Microsoft for the .NET framework
