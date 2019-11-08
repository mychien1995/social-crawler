using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Crawler.Engine.Services.Excels
{
    public class ExcelRange
    {
        public ClosedXML.Excel.IXLRange Range { get; set; }
        public ExcelRange(ClosedXML.Excel.IXLRange range)
        {
            Range = range;
        }

        /// <summary>
        /// Set value of range
        /// WriteOnly
        /// </summary>
        public object Value
        {
            set
            {
                Range.Value = value;
            }
        }

        /// <summary>
        /// Set formula
        /// WriteOnly
        /// </summary>
        public string Formula
        {
            set
            {
                Range.FormulaA1 = value;
            }
        }

        /// <summary>
        /// Get Count of rows in range
        /// ReadOnly
        /// </summary>
        public int RowsCount
        {
            get
            {
                return Range.RowCount();
            }
        }

        /// <summary>
        /// Get Count of columns in range
        /// ReadOnly
        /// </summary>
        public int ColumnsCount
        {
            get
            {
                return Range.ColumnCount();
            }
        }


        public bool HasData(int rowNum)
        {
            for (int i = 1; i <= this.ColumnsCount; i++)
            {
                if (this.Range.Cell(rowNum, i).Value != null) return true;
            }
            return false;
        }

        public string SheetName { get; set; }
        public int IndexInSheet { get; set; }
    }
}