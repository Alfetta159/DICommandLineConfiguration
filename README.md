Dependency Injection makes managing resources in applications hosted in .NET much more straightforward, at least once you get your head around configuring your host.

> To see the example, scroll down to 'The Example'

## Why Configure with Command Line Parameters?

A long long time ago before GUI-based operating systems there were CLI-based operating systems, and so many programs relied on a command with many parameters also known as switches, options, arguments or flags. One of my favorites was PKZIP. The amount of command line options was overwhelming just like Git. However, we didn't have the web back with resources like Stack Overflow to for instant help.

For .NET applications we typically use some kind of JSON file with settings in them, but we might want to make our application flexible where it can do many different things based on the command-line parameters which is what makes Git so powerful and adaptable.

### Why Would I Need This?

In my case it was to write a stop-gap solution that didn't require a complex algorithm. I wanted to run it like a schedule task so it would run as a service, but I didn't want to publish it as a service on the hosting server. Most of all, I wanted certain values to not be locked into code, procedural or configuration. Some of these values were API keys. So I could place those values in a DOS batch file or just put them in the Scheduled Task.

## The Example

Now our example is a .NET console application so it is called in the terminal and our parameters are positional and not optional. We call it this way:

`dotnet DiClParameterConfig.dll first second third fourth fifth`

> If you're just going to run this in the debugger in VSCode, the repo included at the bottom of this post has parameters in the launch.json file.

What makes this work is our configuration configuration. What I mean, is we configure the .NET host in our program.cs file and first creating the default builder with command line arguments and then configure our application configuration as an in-memory collection that is a based on key-value pairs, or a dictionary.

```csharp
using IHost host = Host
    .CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((_, configuration) =>
        configuration.AddInMemoryCollection(
            new Dictionary<string, string>
            {
                ["First"] = args[0],
                ["Second"] = args[1],
                ["Third"] = args[2],
                ["Fourth"] = args[3],
                ["Fifth"] = args[4]
            }))
        .ConfigureServices((_, services) =>
            services
                .AddScoped<TestClass>()
    )
    .Build();
```

Once we've done this, we can pass around this configuration as a parameter to any constructor:

```csharp
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
      ...
    }
}
```

Now this example doesn't validate the parameter list or offer any flexibility, but that's just C# and outside of scope of this demonstration.

What's important to note is that in our host configuration we are mapping our parameters to members of a dictionary, but in our class, we're extracting our configured values not unlike any other way we would using the `IConfiguration` interface.
