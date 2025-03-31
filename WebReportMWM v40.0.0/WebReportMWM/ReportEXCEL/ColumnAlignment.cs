using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPORTEXCEL
{
    public enum ColumnAlignment
    {
        CENTER= OfficeOpenXml.Style.ExcelHorizontalAlignment.Center,
        LEFT = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left,
        RIGHT = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right
    }
}
