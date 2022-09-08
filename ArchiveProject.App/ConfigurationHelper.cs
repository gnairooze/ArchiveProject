using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveProject.App
{
    internal class ConfigurationHelper
    {
        private const string SETTING_FILE = ".archiveproject";

        public Models.ArchiveSettings Settings { get; private set; }

        public ConfigurationHelper(string sourcePath)
        {
            Settings = new Models.ArchiveSettings() { 
                SourcePath = sourcePath 
            };

            GetTargetPath();
        }

        private void GetTargetPath()
        {
            var settingsFilePath = Path.Combine(Settings.SourcePath, SETTING_FILE);

            if (!File.Exists(settingsFilePath)){
                throw new FileNotFoundException($"{settingsFilePath} not found");
            }

            var lines = File.ReadLines(settingsFilePath);
            
            if(!lines.Any() || !string.IsNullOrWhiteSpace(lines.First()))
            {
                throw new FileLoadException($"{settingsFilePath} is empty");
            }

            Settings.TargetPath = lines.First();
        }
    }
}
