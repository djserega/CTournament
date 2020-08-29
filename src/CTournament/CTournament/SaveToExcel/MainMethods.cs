using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.SaveToExcel
{
    internal static class MainMethods
    {
        internal static void SetStyleHeader(IXLRange range)
        {
            range.Style.Fill.BackgroundColor = XLColor.ForestGreen;
            range.Style.Font.Bold = true;
        }

        internal static void SetStyleHeader(IXLCell cell)
        {
            cell.Style.Font.Bold = true;
        }
    }
}
