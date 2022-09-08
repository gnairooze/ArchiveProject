using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
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
        private const string TEMP_FOLDER = "Source";
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

            var targetTempFolder = Path.Combine(_Settings.TargetPath, TEMP_FOLDER);

            if (Directory.Exists(targetTempFolder))
            {
                Directory.Delete(targetTempFolder, true);
            }

            Directory.CreateDirectory(targetTempFolder);

            RemoveIgnoredFiles(ref filesToBeArchived);
            RemoveIgnoredDirectories(ref directoriesToBeArchived);

            Parallel.ForEach(directoriesToBeArchived, dirPath =>
            {
                var dirname = dirPath.Replace(_Settings.SourcePath, targetTempFolder);

                if (!Directory.Exists(dirname))
                {
                    Directory.CreateDirectory(dirPath.Replace(_Settings.SourcePath, targetTempFolder));
                }
            });

            Parallel.ForEach(filesToBeArchived, newPath =>
            {
                File.Copy(newPath, newPath.Replace(_Settings.SourcePath, targetTempFolder), true);
            });

            var zipFileName = Path.Combine(_Settings.TargetPath,$"{new DirectoryInfo(_Settings.SourcePath).Name}-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff")}.zip");

            ZipFile.CreateFromDirectory(targetTempFolder, zipFileName);

            Directory.Delete(targetTempFolder, true);
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
                                    where file.IndexOf($"\\{pattern}") >= 0 || file.IndexOf($"\\{pattern}\\") >= 0
                                    select file).ToList();

                toBeArchived = toBeArchived.Except(matchedFiles).ToList();
            }


        }

    }
}
