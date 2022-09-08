// See https://aka.ms/new-console-template for more information

using ArchiveProject.App;

if(args == null || !args.Any())
{
    throw new InvalidProgramException("source path not provided. ArchiveProject.App.exe sourcePath");
}

var sourcePath = args[0];

ConfigurationHelper _Helper = new ConfigurationHelper(sourcePath);
