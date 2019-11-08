using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Crawler.Engine.Services.Excels
{
    public class ExcelWorkbook
    {
        public ClosedXML.Excel.XLWorkbook Workbook { get; set; }

        public int SheetCount { get { return Workbook == null ? -1 : Workbook.Worksheets.Count; } }
        public ExcelWorkbook(ClosedXML.Excel.XLWorkbook workbook)
        {
            Workbook = workbook;
        }

        public List<ExcelWorksheet> GetSheets()
        {
            return Workbook.Worksheets.Select(x => new ExcelWorksheet(x)).ToList();
        }

        public ExcelWorksheet GetSheet(int index)
        {
            if (Workbook == null || Workbook.Worksheets.Count < index) return null;
            return new ExcelWorksheet(Workbook.Worksheet(index));
        }

        public Stream ToStream()
        {
            MemoryStream OutPut = new MemoryStream();
            Workbook.SaveAs(OutPut);
            if (OutPut != null)
            {
                // return the filestream
                // Rewind the memory stream to the beginning
                OutPut.Seek(0, SeekOrigin.Begin);
            }
            return OutPut;
        }

        public static ExcelWorkbook OpenWorkbook(Stream fileStream)
        {
            return new ExcelWorkbook(new ClosedXML.Excel.XLWorkbook(fileStream));
        }
    }
}