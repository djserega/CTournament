using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.SaveToExcel
{
    internal class ColumnMainStatistics
    {
        public ColumnMainStatistics(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        internal int Id { get; set; }
        internal string Name { get; set; }
        internal string Description { get; set; }
    }
}
