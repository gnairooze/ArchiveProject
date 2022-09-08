using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArchiveProject.App
{
    internal class Archive
    {
        private Models.ArchiveSettings _Settings;
        private List<string> _IgnoreList;
        public Archive(Models.ArchiveSettings settings, List<string> ignoreList)
        {
            if(settings == null)
            {
                throw new ArgumentNullException("settings is null");
            }

            if (ignoreList == null)
            {
                throw new ArgumentNullException("ignoreList is null");
            }

            _Settings = settings;
            _IgnoreList = ignoreList;
        }

        public void Run()
        {
            var filesToBeArchived = Directory.GetFiles(_Settings.SourcePath).ToList();

            //process ignore list and remove match files in files to be archived
            ProcessFilesToBeArchived(filesToBeArchived);

            foreach (var file in filesToBeArchived)
            {
                File.Copy(file, Path.Combine(_Settings.TargetPath, new FileInfo(file).Name));
            }
        }

        private void ProcessFilesToBeArchived(List<string> filesToBeArchived)
        {
            if(_IgnoreList == null || _IgnoreList.Count == 0)
            {
                return;
            }

            foreach (var pattern in _IgnoreList)
            {
                var matchedFiles = (from file in filesToBeArchived
                            where Regex.IsMatch(file, pattern)
                            select file).ToList();

                filesToBeArchived = filesToBeArchived.Except(matchedFiles).ToList();
            }
        }
    }
}
