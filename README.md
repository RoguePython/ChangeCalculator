🧮 Change Calculator API (C# / .NET)

A small Web API that calculates the minimum number of South African
banknotes and coins required for any given amount in Rands.

------------------------------------------------------------------------

📦 Getting Started

You can run this project either from a zipped download or directly by
cloning the GitHub repo.

------------------------------------------------------------------------

Option 1: Run from a .zip file

1.  Download the .zip file provided.
2.  Right-click → Extract All… into a folder (e.g. C:\ChangeCalculator).
3.  Open PowerShell in that folder.
    (Tip: Right-click the folder → Open in Terminal).

------------------------------------------------------------------------

Option 2: Clone from GitHub

1.  Open PowerShell in the folder where you keep projects.

2.  Run:

        git clone https://github.com/RoguePython/ChangeCalculator.git
        cd ChangeCalculator

------------------------------------------------------------------------

▶️ Running the API

Confirm you have .NET SDK 9.0 installed:

    dotnet --version

You should see something like 9.0.xxx.

Restore dependencies:

    dotnet restore

Build the solution:

    dotnet build

Start the API:

    dotnet run --project .\ChangeCalculator.Api\ChangeCalculator.Api.csproj

Watch the console — you’ll see something like:

    Now listening on: https://localhost:xxxx
    Now listening on: http://localhost:xxxx

Open a browser to:
👉 https://localhost:xxxx/swagger
(replace xxxx with your port number).

------------------------------------------------------------------------

📡 API Usage

Endpoint
POST /api/change/calculate

Request Example

    { "amount": 786.80 }

Response Example

    {
      "R200": 3,
      "R100": 1,
      "R50": 1,
      "R20": 1,
      "R10": 1,
      "R5": 1,
      "R2": 0,
      "R1": 1,
      "50c": 1,
      "20c": 1,
      "10c": 1
    }

------------------------------------------------------------------------

🧪 Running Tests

To verify the calculation logic:

    dotnet test

All tests should pass ✅.

------------------------------------------------------------------------

⚙️ Tech Notes

-   Built with C# .NET 9 Web API
-   Swagger/OpenAPI docs enabled by default
-   Uses a greedy algorithm with cents (integers) to avoid rounding
    issues
-   Smallest denomination supported: 10c

------------------------------------------------------------------------

🎯 Why this matters

This project shows:

-   Clean API design (POST endpoint, structured JSON)
-   Clear error handling for invalid inputs
-   Readable, testable service code
-   Developer-friendly docs (Swagger + README)
