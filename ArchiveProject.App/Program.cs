// See https://aka.ms/new-console-template for more information

using ArchiveProject.App;

if(args == null || !args.Any())
{
    throw new InvalidProgramException("source path not provided. ArchiveProject.App.exe sourcePath");
}

var sourcePath = args[0];

ConfigurationHelper _Helper = new ConfigurationHelper(sourcePath);

if (_Helper.Settings.AttendedRun)
{
    Console.WriteLine($"All the files and directories in {_Helper.Settings.TargetPath} will be deleted. Please select Y to proceed (Y,N)?");
    ConsoleKeyInfo key = Console.ReadKey(true);

    if (key.Key != ConsoleKey.Y)
    {
        Console.WriteLine("operation aborted");
        return;
    }
}

Console.WriteLine("operation started");
Archive manager = new Archive(_Helper.Settings);
manager.Run();

Console.WriteLine("operation completed");