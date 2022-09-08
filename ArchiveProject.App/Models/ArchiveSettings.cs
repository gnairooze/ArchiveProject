using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveProject.App.Models
{
    internal class ArchiveSettings
    {
        public string SourcePath { get; set; } = string.Empty;
        public string TargetPath { get; set; } = string.Empty;
        public bool AttendedRun { get; set; } = false;

        public List<string> IgnoreList { get; }

        public ArchiveSettings()
        {
            IgnoreList = new List<string>();
        }
    }
}
