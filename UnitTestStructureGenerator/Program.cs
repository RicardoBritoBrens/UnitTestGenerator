// See https://aka.ms/new-console-template for more information
using System.Text;

/* whishes features

    * Create empty folders structure of proejct
    * Explore namespaces and create file classes with apend Tests at the end
    * On DoubleClick List files in the current folder, create new folder and create the same files with and apend at the end
 */

// Default namespace
string defaultNamespace = "AV.Web.ViewModels.Api";

// Usings
string[] usings = new string[]
{
    "Xunit",
    "System",
    defaultNamespace,
};

// Class names
//string[] classNames = new string[]
//{
//    "AccionistaRepresentanteLegalAddOrUpdateVM"
//};

string[] classNames = GetClassNamesFromPath("C:\\Workspace\\Remesas\\BHDL-PegasusLBTR_Services\\PegasusLbtr.Core\\Models");

string[] GetClassNamesFromPath(string path)
{
    var fileNameWithPaths = Directory.GetFiles(path);

    List<string> output = new List<string>();

    foreach (var fileName in fileNameWithPaths)
    {
        var currentFileName = Path.GetFileNameWithoutExtension(fileName);

        output.Add(currentFileName);
    }
    return output.ToArray();
}

// Start Multiple generation just for test
GenerateMultipleFiles(defaultNamespace, classNames, usings);

static void GenerateMultipleFiles(string namesSpace, string[] classNames, string[] usings)
{
    foreach (var className in classNames)
    {
        Directory.CreateDirectory("GeneratedFiles");

        var fileName = $"{Path.Combine(Directory.GetCurrentDirectory(), "GeneratedFiles")}/{className}Tests.cs";

        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        string templateFormatTest = @$"
            {GenerateUsings(usings)}

            namespace {namesSpace}
            {{
            	public class {className}Tests
                {{
                    [Fact]
                    public void CanCreate_Instance_Correctly()
                    {{
                        // Arrange & Act
                        var currentInstance = new {className}();

                        // Assert
                        Assert.NotNull(currentInstance);
                    }}
            	}}
            }}
        ";

        // Create a new file
        using (StreamWriter sw = File.CreateText(fileName))
        {
            sw.WriteLine(templateFormatTest);
        }

        Console.WriteLine($"File {fileName} generated");
    }
}

static string GenerateUsings(string[] vals)
{
    StringBuilder result = new StringBuilder();
    foreach (var className in vals)
    {
        result.Append($"using {className}; \n ");
    }
    return result.ToString();
}