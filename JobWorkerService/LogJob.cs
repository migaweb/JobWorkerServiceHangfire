namespace JobWorkerService;

public static class LogJob
{
  public static void Execute(CancellationToken cancellationToken)
  {
    if (!cancellationToken.IsCancellationRequested)
    {
      string docPath =
          Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

      // Write the string array to a new file named "WriteLines.txt".
      using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "JobLog.txt"), true))
      {
        outputFile.WriteLine($"{DateTime.UtcNow:s}");
      }

      Console.WriteLine("Executed job!");
    }
  }
}
