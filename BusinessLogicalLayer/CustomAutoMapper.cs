using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    //nuget Console - Install-Package AutoMapper.dll
    public class CustomAutoMapper<T, W>
    {
        public static T ConvertTo(W item)
        {
            T obj = Activator.CreateInstance<T>();
            Type type = typeof(W);
            foreach (PropertyInfo property in type.GetProperties())
            {
                try
                {
                    PropertyInfo p = typeof(T).GetProperty(property.Name);
                    if (p.Name == property.Name && p.PropertyType == property.PropertyType)
                    {
                        p.SetValue(obj, property.GetValue(item));
                    }
                }
                catch
                {

                }
            }
            return obj;
        }

    }
}
