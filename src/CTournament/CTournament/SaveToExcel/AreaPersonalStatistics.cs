using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.SaveToExcel
{
    internal class AreaPersonalStatistics
    {
        public AreaPersonalStatistics(int rowId, int columnId, string name, string description)
        {
            RowId = rowId;
            ColumnId = columnId;
            Name = name;
            Description = description;
        }

        internal int RowId { get; set; }
        internal int ColumnId { get; set; }
        internal string Name { get; set; }
        internal string Description { get; set; }
    }
}
