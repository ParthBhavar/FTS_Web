using Dapper;
using FTS.Model;
using FTS.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;

namespace FTS.Data.Extension
{
    public static class PropertyExtension
    {
        /// <summary>
        /// TO get column names
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseModel"></param>
        /// <returns></returns>
        public static IList<string> GetTableValueParams<T>(this T baseModel)
        {
            PropertyInfo[] properties = typeof(T).GetProperties
                    (BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] readableProperties = properties.Where
                (w => w.CanRead).ToArray();

            var columnNames =
                readableProperties.Select(s => s.Name).ToList();
            var paramValues = new List<TableValueParam>();
            columnNames.ForEach(x => {
                AttributeCollection attributes = TypeDescriptor.GetProperties(baseModel)[x].Attributes;
                TableValueParam myAttribute = (TableValueParam)attributes[typeof(TableValueParam)];
                if (myAttribute != null)
                    paramValues.Add(myAttribute);
            });
            return paramValues.OrderBy(t => t.Order).Select(x => x.Name).ToList();
        }

        /// <summary>
        /// This extension converts an enumerable set to a Dapper TVP
        /// </summary>
        /// <typeparam name="T">type of enumerbale</typeparam>
        /// <param name="enumerable">list of values</param>
        /// <param name="typeName">database type name</param>
        /// <param name="orderedColumnNames">if more than one column in a TVP, 
        /// columns order must mtach order of columns in TVP</param>
        /// <returns>a custom query parameter</returns>
        public static SqlMapper.ICustomQueryParameter AsTableValuedParameter<T>
            (this IEnumerable<T> enumerable,
            string typeName, IEnumerable<string> orderedColumnNames = null)
        {
            var dataTable = new DataTable();
            if (typeof(T).IsValueType || typeof(T).FullName.Equals("System.String"))
            {
                dataTable.Columns.Add(orderedColumnNames == null ?
                    "NONAME" : orderedColumnNames.First(), typeof(T));
                foreach (T obj in enumerable)
                {
                    dataTable.Rows.Add(obj);
                }
            }
            else
            {
                PropertyInfo[] properties = typeof(T).GetProperties
                    (BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo[] readableProperties = properties.Where
                    (w => w.CanRead).ToArray();
                if (readableProperties.Length > 1 && orderedColumnNames == null)
                    throw new ArgumentException("Ordered list of column names must be provided when TVP contains more than one column");

                var columnNames = (orderedColumnNames ??
                    readableProperties.Select(s => s.Name)).ToArray();
                foreach (string name in columnNames)
                {
                    dataTable.Columns.Add(name, readableProperties.FirstOrDefault
                        (s => s.Name.ToLower().Equals(name.ToLower())).PropertyType);
                }

                foreach (T obj in enumerable)
                {
                    dataTable.Rows.Add(
                        columnNames.Select(s => readableProperties.FirstOrDefault
                            (s2 => s2.Name.ToLower().Equals(s.ToLower())).GetValue(obj))
                            .ToArray());
                }
            }
            return dataTable.AsTableValuedParameter(typeName);
        }
    }
}
