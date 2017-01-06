using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelLibrary;
using DatamodelBuild.Exceptions;

namespace DatamodelBuild.Main
{
    public class DatamodelBuilder : IDatamodelBuild
    {
        private List<String> sheets;
        private List<String> savePaths;
        public String backupDirectory { get; set; }

        public DatamodelBuilder()
        {
            sheets = new List<String>();
            savePaths = new List<String>();
        }

        public List<String> addSheet(String sn)
        {
            sheets.Add(sn);
            return sheets;
        }
        public List<String> getSheets()
        {
            return sheets;
        }

        public void Clear()
        {
            sheets.Clear();
            savePaths.Clear();
        }

        public void buildModel()
        {
            if (sheets.Count > 0 && savePaths.Count > 0)
            {
                ExcelObject _eo = new ExcelObject();
                _eo.Open();

                foreach (String sheet in sheets)
                {
                    _eo.addSheet(sheet);
                }

                foreach (String savePath in savePaths)
                {
                    try
                    {
                        _eo.Save(savePath);
                    }
                    catch (CustomCOMException e)
                    {
                        throw e;
                    }
                }

                String backupFile = backupDirectory + DateTime.Now.ToString("yyyyMMddHHmmss");
                _eo.Save(backupFile);

                _eo.Close();
            }
        }
        public List<String> addSavePath(String spName)
        {
            savePaths.Add(spName);
            return savePaths;
        }
        public List<String> getSavePaths()
        {
            return savePaths;
        }
    }
}
