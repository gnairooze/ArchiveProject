using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public Archive(Models.ArchiveSettings settings)
        {
            if(settings == null)
            {
                throw new ArgumentNullException("settings is null");
            }

            _Settings = settings;
        }

        public void Run()
        {
            var filesToBeArchived = Directory.GetFiles(_Settings.SourcePath, "*.*", SearchOption.AllDirectories).ToList();
            var directoriesToBeArchived = Directory.GetDirectories(_Settings.SourcePath, "*.*", SearchOption.AllDirectories).ToList();

            if (Directory.Exists(_Settings.TargetPath))
            {
                Directory.Delete(_Settings.TargetPath, true);
                Directory.CreateDirectory(_Settings.TargetPath);
            }
            

            RemoveIgnoredFiles(ref filesToBeArchived);
            RemoveIgnoredDirectories(ref directoriesToBeArchived);

            Parallel.ForEach(directoriesToBeArchived, dirPath =>
            {
                var dirname = dirPath.Replace(_Settings.SourcePath, _Settings.TargetPath);

                if (!Directory.Exists(dirname))
                {
                    Directory.CreateDirectory(dirPath.Replace(_Settings.SourcePath, _Settings.TargetPath));
                }
            });

            Parallel.ForEach(filesToBeArchived, newPath =>
            {
                File.Copy(newPath, newPath.Replace(_Settings.SourcePath, _Settings.TargetPath), true);
            });


        }

        private void RemoveIgnoredFiles(ref List<string> toBeArchived)
        {
            if(_Settings.IgnoreList.Count == 0)
            {
                return;
            }

            foreach (var pattern in _Settings.IgnoreList)
            {
                var matchedFiles = (from file in toBeArchived
                            where file.IndexOf($"\\{pattern}\\") >= 0
                            select file).ToList();

                toBeArchived = toBeArchived.Except(matchedFiles).ToList();
            }
        }

        private void RemoveIgnoredDirectories(ref List<string> toBeArchived)
        {
            if (_Settings.IgnoreList.Count == 0)
            {
                return;
            }

            foreach (var pattern in _Settings.IgnoreList)
            {
                var matchedFiles = (from file in toBeArchived
                                    where file.IndexOf($"\\{pattern}") >= 0
                                    select file).ToList();

                toBeArchived = toBeArchived.Except(matchedFiles).ToList();
            }
        }

    }
}
