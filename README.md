# Fetch Backend Takehome
This API is a simple points management system written in .NET Core 8. It supports managing points for different payers, 
spending points based on a First-In-First-Out (FIFO) methodology, and retrieving balances for payers. 
This document will walk you through everything you need to set up, compile, run, and test the API. The API is deployed at https://fetchbackendtakehome20241130181142.azurewebsites.net/swagger/index.html

## Features:
1. Add Points: Add points for a payer with a timestamp.
2. Spend Points: Spend points based on the oldest transactions (FIFO), ensuring no payer has a negative balance.
3. Get Balances: Retrieve the current balance of points for each payer.

## Prerequisites
To run the API, ensure you have the following installed:

1. DotNET SDK (Version 7 or later, compatible with .NET Core 8):
* Download .NET SDK
* Verify installation by running:
  ```console
  dotnet --version
  ```

2. Development Environment (Optional):
* Any IDE supporting .NET Core (e.g., Visual Studio, Visual Studio Code).

## Setup Instructions
1. Clone or Download the Repository:
* Clone the repository:
```console
  git clone <repository-url>
```
* Navigate to the project folder:
```console
  cd PointsManagementAPI
```
2. Restore Dependencies:
* Restore NuGet dependencies using:
```console
  dotnet restore
```
3. Build the Project:
* Compile the application using:
```console
dotnet build
```
4. Run the Application:
* Start the API using:
```console
dotnet run
```
5. Access the API:
* Once running, the API will be accessible at:
```
  http://localhost:8000
```
* Swagger UI is available at:
```console
http://localhost:8000/swagger
```
## Endpoints
### 1. Add Points
* Method: POST
* URL: /points/add
* Request Body:
```json
{
  "payer": "DANNON",
  "points": 300,
  "timestamp": "2022-10-31T10:00:00Z"
}
```
* Response: 200 OK if successful.

### 2. Spend Points
Description: Spend points using FIFO methodology.

* Method: POST
* URL: /points/spend
* Request Body:
```json
{
  "points": 500
}
```
* Response: 200 OK
```json
[
  { "payer": "DANNON", "points": -100 },
  { "payer": "UNILEVER", "points": -400 }
]
```

### 3. Get Balance
Description: Retrieve the current balance of points for each payer.

* Method: GET
* URL: /points/balance
* Response: 200 OK
```json
{
  "DANNON": 100,
  "UNILEVER": 0,
  "MILLER COORS": 5300
}
```
## Testing the API:
Using Swagger UI
1. Open http://localhost:5000/swagger in your browser.
2. Use the interface to test the Add, Spend, and Get Balance endpoints.

Using curl
Alternatively, you can use curl to test the API:

1. Add Points:

```console
curl -X POST http://localhost:5000/points/add \
-H "Content-Type: application/json" \
-d '{
      "payer": "DANNON",
      "points": 300,
      "timestamp": "2022-10-31T10:00:00Z"
    }'
```
2. Spend Points:

```console
curl -X POST http://localhost:5000/points/spend \
-H "Content-Type: application/json" \
-d '{
      "points": 500
    }'
```
Get Balances:

```console
curl -X GET http://localhost:5000/points/balance
```
## Error Handling:
* Invalid Input
  * For invalid inputs or insufficient points, the API returns a 400 Bad Request with an error message.
Example:
```json
{
  "error": "Insufficient points."
}
```
## FAQ:
FAQ
1. What happens if I try to spend more points than available?
  * The API returns a 400 Bad Request with an appropriate error message.

2. How does the API handle FIFO spending?
  * Transactions are sorted by their timestamp. Points are deducted from the oldest transaction first.

3. Can I test the API without an IDE?
  * Yes, you can use the terminal and tools like curl or Postman to test the API.
