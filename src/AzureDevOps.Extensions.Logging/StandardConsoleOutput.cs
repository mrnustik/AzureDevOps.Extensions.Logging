namespace AzureDevOps.Extensions.Logging;

class StandardConsoleOutput : IConsoleOutput
{
    public void WriteLine(string line)
    {
        Console.WriteLine(line);
    }
}