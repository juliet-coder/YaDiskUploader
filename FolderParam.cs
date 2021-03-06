using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class FolderParam
    {
        public FolderParam(string nameWithExt, string localPath)
        {
            NameWithExt = nameWithExt;
            LocalPath = localPath;

        }
        public string NameWithExt { get; set; }
        public string LocalPath { get; set; }
        
    }
}
