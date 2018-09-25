using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityTableName : Attribute
    {
        public string TableName { get; private set; }

        public EntityTableName(string _tableName)
        {
            this.TableName = _tableName;
        }
    }
}
