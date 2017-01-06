using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatamodelBuild.Main;

namespace DatamodelBuildConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = "c:\\tmp\\";
            string sp = dir + "new.xlsx";
            string sh = dir + "Sheet.Alle importer.xlsx";

            DatamodelBuilder db = new DatamodelBuilder();

            db.addSheet(sh);
            db.addSavePath(sp);
            db.buildModel();

        }
    }
}
