﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var filesToBeArchived = Directory.GetFiles(_Settings.SourcePath);

            //process ignore list and remove match files in files to be archived
            ProcessFilesToBeArchived(filesToBeArchived);

            foreach (var file in filesToBeArchived)
            {
                File.Copy(file, Path.Combine(_Settings.TargetPath, GetFileNameOnly(file)));
            }
        }

        private void ProcessFilesToBeArchived(string[] filesToBeArchived)
        {
            if(_IgnoreList == null || _IgnoreList.Count == 0)
            {
                return;
            }


        }

        private string GetFileNameOnly(string FullPath)
        {
            return new FileInfo(FullPath).Name;
        }
    }
}
