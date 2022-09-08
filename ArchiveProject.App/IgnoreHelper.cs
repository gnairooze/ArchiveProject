using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveProject.App
{
    internal class IgnoreHelper
    {
        private const string IGNORE_FILE = ".gitignore";
        public string SourcePath { get; private set; }

        public List<string> IgnoreList { get; private set; }

        public IgnoreHelper(string sourcePath)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new ArgumentNullException(nameof(sourcePath));
            }

            IgnoreList = new List<string>();
            SourcePath = sourcePath;
            FillIgnoreList();
        }

        private void FillIgnoreList()
        {
            var ignorefilepath = Path.Combine(SourcePath, IGNORE_FILE);

            if(!File.Exists(ignorefilepath))
            {
                return;
            }

            IgnoreList.AddRange(File.ReadLines(ignorefilepath));
        }
    }
}
