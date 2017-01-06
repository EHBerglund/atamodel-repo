using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DatamodelBuild
{
    public interface IDatamodelBuild
    {
        List<String> addSheet(String sname);
        List<String> getSheets();
        void buildModel();
        List<String> addSavePath(String spName);
        List<String> getSavePaths();
    }
}
