using System.Collections.Generic;
using AzureDevOps.Extensions.Logging;

namespace AzureDevops.Extensions.Logging.Tests;

public class FakeConsoleOutput : IConsoleOutput
{
    private readonly List<string> lines = new();

    public void WriteLine(string line)
    {
        lines.Add(line);
    }

    public List<string> GetOutputLines()
    {
        return lines;
    }
}