using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFrameworkCommonLib
{
    public class AppSettings
    {
        public string AppName { get; set; }
        public string MainHeading { get; set; }

        public string MainDesc { get; set; }

        public string FolioName { get; set; }

        public string FolioAmount { get; set; }

        public string Year { get; set; }
        


        public AppSettings()
        {
            AppName = "";
            MainHeading = "";
            MainDesc = "";
            FolioName = "";
            FolioAmount = "";
            Year = "";

        }
    }
}
