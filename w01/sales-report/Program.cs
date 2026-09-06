using Newtonsoft.Json;
using System.Text;

var currentDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName
?? Directory.GetCurrentDirectory();

var storesDirectory = Path.Combine(currentDirectory, "store");

var salesTotalDirectory = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDirectory);

var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);

var salesSummary = GenerateSalesSummary(salesFiles, salesTotal);

var reportFile = Path.Combine(salesTotalDirectory, "totals.txt");

File.WriteAllText(reportFile, salesSummary);

Console.WriteLine($"Sales summary created: {reportFile}");

IEnumerable<string> FindFiles(string folderName)
{
List<string> salesFiles = new List<string>();

var foundFiles = Directory.EnumerateFiles(
    folderName,
    "*.json",
    SearchOption.AllDirectories);

foreach (var file in foundFiles)
{
    salesFiles.Add(file);
}

return salesFiles;


}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
double salesTotal = 0;

foreach (var file in salesFiles)
{
    string salesJson = File.ReadAllText(file);

    SalesData? data =
        JsonConvert.DeserializeObject<SalesData?>(salesJson);

    salesTotal += data?.Total ?? 0;
}

return salesTotal;


}

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

record SalesData(double Total);