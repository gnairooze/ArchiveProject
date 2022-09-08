using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ArchiveProject.App
{
    internal class ConfigurationHelper
    {
        private const string SETTING_FILE = ".archiveproject.xml";

        private readonly XmlDocument _Config;

        public Models.ArchiveSettings Settings { get; private set; }

        public ConfigurationHelper(string sourcePath)
        {
            _Config = new XmlDocument();

            Settings = new Models.ArchiveSettings() { 
                SourcePath = sourcePath 
            };

            ReadSettings();
        }

        private void ReadSettings()
        {
            var settingsFilePath = Path.Combine(Settings.SourcePath, SETTING_FILE);

            if (!File.Exists(settingsFilePath))
            {
                throw new FileNotFoundException($"{settingsFilePath} not found");
            }

            _Config.Load(settingsFilePath);

            var targetPath = _Config.SelectSingleNode("//targetPath");
            if (targetPath == null)
            {
                throw new InvalidOperationException($"targetPath not defined in {SETTING_FILE}");
            }
            Settings.TargetPath = targetPath.InnerText;

            var attendedRun = _Config.SelectSingleNode("//attendedRun");
            if (attendedRun == null)
            {
                throw new InvalidOperationException($"attendedRun not defined in {SETTING_FILE}");
            }

            var attendedRunBoolean = false;
            
            if (bool.TryParse(attendedRun.InnerText, out attendedRunBoolean))
            {
                Settings.AttendedRun = attendedRunBoolean;
            }
            else
            {
                throw new InvalidOperationException($"invalid attendedRun value {attendedRun.InnerText} in {SETTING_FILE}");
            }

            var ignoreListXml = _Config.SelectNodes("//ignoreList/ignore");

            if(ignoreListXml == null)
            {
                return;
            }

            foreach (XmlNode ignore in ignoreListXml)
            {
                Settings.IgnoreList.Add(ignore.InnerText);
            }
        }
    }
}
