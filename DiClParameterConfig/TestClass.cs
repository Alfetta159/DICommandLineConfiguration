using System.Text;
using Microsoft.Extensions.Configuration;

namespace DiClParameterConfig;

public class TestClass
{
    private readonly string First;
    private readonly string Second;
    private readonly string Third;
    private readonly string Fourth;
    private readonly string Fifth;

    public TestClass(IConfiguration configuration)
    {
        First = configuration["First"];
        Second = configuration["Second"];
        Third = configuration["Third"];
        Fourth = configuration["Fourth"];
        Fifth = configuration["Fifth"];
    }

    public void Run()
    {
        var output = new StringBuilder();

        output.AppendLine("Welcome to command line parametric configuratino with dependency injection in .NET.");
        output.AppendLine();

        output.AppendLine($"The first value is {First}");
        output.AppendLine($"The second value is {Second}");
        output.AppendLine($"The third value is {Third}");
        output.AppendLine($"The fourth value is {Fourth}");
        output.AppendLine($"The fifth value is {Fifth}");

        output.AppendLine();
        output.AppendLine("Goodbye...");

        Console.Write(output.ToString());
    }
}