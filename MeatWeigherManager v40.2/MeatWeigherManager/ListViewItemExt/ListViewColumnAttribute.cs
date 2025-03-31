using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListViewItemExt
{
    public class ListViewColumnAttribute : Attribute
    {
        private string _columnName;

        public ListViewColumnAttribute(string columnName)
        {
            _columnName = columnName;
        }

        public override string ToString()
        {
            return _columnName;
        }
    }
}
