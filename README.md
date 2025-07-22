# iChoosr Exercise

This repository contains a full-stack solution for viewing and searching security camera data. It includes:

- ✅ A **CLI tool** to search cameras by name.
- 🌐 A **.NET 9 Web API** that exposes camera data retrieved from a CSV file.
- 🗺️ An **Angular Web application** that displays cameras on a map and distributes them based on predefined rules.

---

## 📁 Project Structure

```
├── README.md
├── data/                            # Camera data in CSV format
└── src/
    ├── SecurityCamera.Common/       # Shared models and services
    ├── SecurityCamera.CLI/          # Command-line search tool
    ├── SecurityCamera.API/          # .NET 9 Web API
    └── SecurityCamera.Web/          # Angular web front-end
```

---

## ▶️ How to Run the CLI Tool

1. Clone the repository.
2. Open your terminal and navigate to the `SecurityCamera.CLI` folder.
3. Run the CLI:
   ```
   dotnet run search --name Neude
   ```
   🔍 You should see a list of cameras matching the name "Neude".

---

## 🌍 How to Run the API and Web Application

1. **Run the API:**

   Open a terminal, navigate to the `SecurityCamera.API` folder, and run:
   ```
   dotnet run --launch-profile https
   ```
   The Swagger UI will be available at:  
   👉 `https://localhost:7056/swagger/index.html`

2. **Run the Web Application:**

   Open a new terminal, navigate to the Angular project and run:
   ```
   ng serve
   ```
   Then open your browser at:  
   👉 `http://localhost:4200/`

---

## 🛠️ Development Process

The solution was built with modularity and clarity in mind, following these steps:

1. **CSV Data Parsing**  
   - Created a shared service in `SecurityCamera.Common` to read and parse camera data from a CSV file.

2. **Web API Development**  
   - Built a .NET 9 Web API in `SecurityCamera.API` using dependency injection and controller routing.
   - Integrated OpenAPI/Swagger for API testing and documentation.

3. **CLI Tool Implementation**  
   - Developed a .NET console app in `SecurityCamera.CLI` to consume the shared CSV parsing logic and enable filtering by name.

4. **Frontend with Angular**  
   - Scaffolded a standalone Angular app inside `SecurityCamera.Web`.
   - Integrated Leaflet for map visualization.
   - Fetched data from the API and displayed it on the map, distributing the cameras into columns based on rules:
     - Divisible by 3 → First column  
     - Divisible by 5 → Second column  
     - Divisible by both → Third column  
     - None of the above → Last column
