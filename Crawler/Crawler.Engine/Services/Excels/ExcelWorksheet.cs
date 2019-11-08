using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crawler.Engine.Services.Excels
{
    public class ExcelWorksheet
    {
        public ClosedXML.Excel.IXLWorksheet Worksheet { get; set; }
        public ExcelWorksheet(ClosedXML.Excel.IXLWorksheet worksheet)
        {
            Worksheet = worksheet;
        }
        public ExcelWorksheet()
        {
        }

        public ExcelRange GetRange(string firstCellAddress, string lastCellAddress)
        {
            return new ExcelRange(Worksheet.Range(firstCellAddress, lastCellAddress));
        }

        public ExcelRange GetRange(int firstRow, int firstCoumn, int lastRow, int lastColumn)
        {
            return new ExcelRange(Worksheet.Range(firstRow, firstCoumn, lastRow, lastColumn));
        }

        public ExcelRange GetRange(string CellAddress)
        {
            return new ExcelRange(Worksheet.Range(CellAddress));
        }

        public object GetValueOfCell(string Address)
        {
            return Worksheet.Cell(Address).Value;
        }
        public object GetValueOfCell(int Row, int Col)
        {
            return Worksheet.Cell(Row, Col).Value;
        }
    }
}