using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using DatamodelBuild.Exceptions;

namespace ExcelLibrary
{
    public class ExcelObject : IExcelObject
    {

        private Excel.Application _excelApp;
        private Excel.Workbook _workBook;
        private Excel.Workbook _tmpWB;
        private Excel.Worksheet _tmpWS;
        private Excel.Sheets _workSheets;

        public ExcelObject()
        {
            //
        }

        public void addSheet(String sheetPath)
        {
            try
            {
                _tmpWB = _excelApp.Workbooks.Open(sheetPath);
                _tmpWS = _tmpWB.Sheets[1];
                _tmpWS.Copy(_workSheets[1]);
                _tmpWB.Close();
            } catch (Exception e)
            {
                if (_tmpWB != null)
                {
                    _tmpWB.Close();
                }
                // TODO
            }
        }
        public void Close()
        {
            _workBook.Close();
            _excelApp.Quit();
        }

        public void Save(String path)
        {
            try
            {
                _workBook.SaveAs(path);
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                this.Close();
                throw new CustomCOMException();
            }
        }
        public void Open()
        {
            _excelApp = new Excel.Application();
            _excelApp.DisplayAlerts = false;
            _workBook = _excelApp.Workbooks.Add();
            _workSheets = _workBook.Sheets;
        }

        
    }
}
