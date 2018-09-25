using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SqlGenerator<T>
    {
        private string insertFields = null;
        private string updateFields = null;

        public SqlGenerator()
        {
            this.insertFields = GetInsertParametersNames();
            this.updateFields = GetUpdateParametersNames();
        }

        private string GetInsertParametersNames()
        {
            StringBuilder parameters = new StringBuilder();
            Type type = typeof(T);
            foreach (PropertyInfo property in type.GetProperties())
            {
                string display = property.Name;

                ColumnNameAttribute attribute = property.GetCustomAttribute<ColumnNameAttribute>();
                if(attribute != null)
                {
                    display = attribute.Column;
                }

                if (property.Name != "ID")
                {
                    parameters.Append("@" + display + ",");
                }
            }
            return parameters.ToString().Substring(0, parameters.Length - 1);
        }

        private string GetUpdateParametersNames()
        {
            StringBuilder parameters = new StringBuilder();
            Type type = typeof(T);
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.GetCustomAttribute<NonEditable>() == null)
                {
                    string display = property.Name;
                    ColumnNameAttribute attribute = property.GetCustomAttribute<ColumnNameAttribute>();
                    if (attribute != null)
                    {
                        display = attribute.Column;
                    }
                    parameters.Append(string.Format("{0} = @{0},", display));
                }
            }
            return parameters.ToString().Substring(0, parameters.Length - 1);
        }

        private string GetTableName()
        {
            Type type = typeof(T);
            EntityTableName etn =
            type.GetCustomAttribute<EntityTableName>();
            return etn == null ? type.Name : etn.TableName;
        }

        private object GetPropertyValue(T item, string parameter)
        {
            return typeof(T).GetProperty(parameter.Replace("@", "")).GetValue(item);
        }

        public SqlCommand CreateInsertCommand(T item)
        {
            string tableName = GetTableName();
            string query =
                string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                               tableName,
                               insertFields.Replace("@", ""),
                               insertFields);
            SqlCommand command = new SqlCommand();
            command.CommandText = query;
            string[] parameters = insertFields.Split(',');
            foreach (string parameter in parameters)
            {
                string realParameter = ReverseEngineerDataAnnotationDiscover(parameter);

                command.Parameters.AddWithValue(parameter, GetPropertyValue(item, realParameter));
            }
            return command;
        }

        private string ReverseEngineerDataAnnotationDiscover(string parameter)
        {
            foreach(PropertyInfo property in typeof(T).GetProperties())
            {
                ColumnNameAttribute att = property.GetCustomAttribute<ColumnNameAttribute>();
                if(att != null)
                {
                     if(parameter.Replace("@","").ToLower() == att.Column.ToLower())
                     {
                         return property.Name;
                     }
                }
            }
            return parameter;
        }

        public SqlCommand CreateUpdateCommand(T item)
        {
            string tableName = GetTableName();
            string query =
                string.Format("UPDATE {0} SET {1} WHERE ID = @ID",
                               tableName,
                               updateFields);
            SqlCommand command = new SqlCommand();
            command.CommandText = query;

            string[] parameters = updateFields.Split(',');
            foreach (string parametro in parameters)
            {
                int qtdCaracteres = parametro.Length;
                int indiceArroba = parametro.LastIndexOf('@');
                string nomeParametro = parametro.Substring(indiceArroba, qtdCaracteres - indiceArroba);
                command.Parameters.AddWithValue(nomeParametro, GetPropertyValue(item, nomeParametro));
            }
            command.Parameters.AddWithValue("@ID", GetPropertyValue(item, "@ID"));
            return command;
        }

        public SqlCommand CreateDeleteCommand(int id)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText =
                string.Format("DELETE FROM {0} WHERE ID = @ID", GetTableName());
            command.Parameters.AddWithValue("@ID", id);
            return command;
        }
    }
}
