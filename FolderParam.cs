using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class FolderParam
    {
        public FolderParam(string localPath)
        {
            LocalPath = localPath;

        }
        public string LocalPath { get; set; }
    }
}
