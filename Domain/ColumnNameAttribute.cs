using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : Attribute
    {
        public string Column { get; private set; }

        public ColumnNameAttribute(string _columnName)
        {
            this.Column = _columnName;
        }
    }
}
