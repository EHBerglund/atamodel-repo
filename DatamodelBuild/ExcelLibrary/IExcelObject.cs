using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelLibrary
{
    public interface IExcelObject
    {
        void addSheet(String sheetPath);
        void Close();

        void Save(String path);

        void Open();
    }
}
