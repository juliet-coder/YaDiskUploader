using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class UploadParam
    {
        public UploadParam(string userDir, string yaDiskDir)
        {
            UserDir = userDir;
            YaDiskDir = yaDiskDir;
        }
        public string UserDir { get; set; }
        public string YaDiskDir { get; set; }

    }
}
