CSE 325 Week 01 Assignment
Part 1: ASP.NET Core Pizza Web API

I completed the Microsoft Learn module Create a web API with ASP.NET Core controllers using .NET 8.

Additional Pizza Record

I added the following additional pizza to the Pizza List:

ID: 3
Name: Beans
Gluten Free: Yes
API Testing

The Pizza API was tested using Swagger, and all four CRUD operations were successfully verified.

GET

Request:

GET /Pizza


Result:

The API successfully returned the pizza list, including the additional Beans pizza.

Status Code: 200 OK

POST

Request:

POST /Pizza


Example request body:

{
  "name": "Cheese Lovers",
  "isGlutenFree": false
}


Result:

The API successfully created a new pizza and assigned it a unique ID.

Status Code: 201 Created

PUT

Request:

PUT /Pizza/{id}


Example request body:

{
  "name": "Cheese Lovers Deluxe",
  "isGlutenFree": false
}


Result:

The API successfully updated the pizza.

Status Code: 204 No Content

DELETE

Request:

DELETE /Pizza/{id}


Result:

The API successfully deleted the pizza.

Status Code: 204 No Content

Part 2

Part 2: Sales Summary Report

I completed the file and directory processing portion of the assignment. The program reads the sales JSON files, calculates the total sales, and generates a detailed sales summary report showing the total for each file.

Working Sales Summary Function
string GenerateSalesSummary(
    IEnumerable<string> salesFiles,
    double salesTotal)
{
    StringBuilder report = new StringBuilder();

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($"Total Sales: {salesTotal:C}");
    report.AppendLine();
    report.AppendLine("Details:");

    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);

        SalesData? data =
            JsonConvert.DeserializeObject<SalesData?>(salesJson);

        double fileTotal = data?.Total ?? 0;

        string fileName = Path.GetFileName(file);

        report.AppendLine($" {fileName}: {fileTotal:C}");
    }

    return report.ToString();
}

Sales Summary Output

The program successfully generated the following report:

Sales Summary
----------------------------
Total Sales: $5,471.50

Details:
 store1.json: $1,250.50
 store2.json: $2,345.75
 store3.json: $1,875.25


The total sales were calculated from the three sales JSON files:

store1.json: $1,250.50
store2.json: $2,345.75
store3.json: $1,875.25
Total Sales: $5,471.50