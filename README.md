# iChoosr Exercise

This repository contains a full-stack solution for viewing and searching security camera data. It includes:

- ✅ A **CLI tool** to search cameras by name.
- 🌐 A **.NET 9 Web API** that exposes camera data retrieved from a CSV file.
- 🗺️ An **Angular Web application** that displays cameras on a map and distributes them based on predefined rules.

## Search Functionality in Angular App
- The Angular frontend provides a user interface to perform searches
- Input field with debounce to limit request frequency
- Map updates dynamically to show search results
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

## ✅ Unit Tests

Unit tests are implemented using [xUnit](https://xunit.net/) and [Moq](https://github.com/moq/moq4). The tests cover core functionalities including CSV parsing, camera searching, and caching behavior.

### 🔍 Tested Components

#### `CsvDataServiceTests`
Tests parsing logic for extracting camera data from raw CSV lines:

- ✅ `ParseCsvLines_ValidLines_ReturnsCameras`  
  Ensures valid CSV lines are parsed into camera objects correctly.

- ✅ `ParseCsvLines_InvalidLines`  
  Verifies that malformed or non-numeric lines are skipped.

- ✅ `ParseCsvLines_InvalidLine_SkipsIt`  
  Confirms that partially valid input returns only valid camera entries.

#### `CameraSearchServiceTests`
Tests camera search logic, including integration with caching and data filtering:

- ✅ `SearchCamerasAsync_WithEmptySearchTerm_ReturnsAllCameras`  
  Returns the full camera list when the search term is empty.

- ✅ `SearchCamerasAsync_WithMatchingName_ReturnsFilteredCameras`  
  Filters results by camera name containing the search term.

- ✅ `SearchCamerasAsync_WithLatitudeMatch_ReturnsCamera`  
  Matches camera by latitude, supporting both `.` and `,` decimal formats.

- ✅ `GetAllCamerasAsync_CachesResult_AfterFirstCall`  
  Validates that results are cached after the first read from CSV.

### 🧪 How to Run Tests

From the root of the solution:

```bash
dotnet test
```
