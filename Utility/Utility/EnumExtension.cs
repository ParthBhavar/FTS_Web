namespace FTS.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    public static class EnumExtension
    {
        /// <summary>
        /// Function to get descriotion of enums
        /// </summary>
        /// <param name="enumValue">Its a value of enum member</param>
        /// <returns>Sting value</returns>
        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }

        /// <summary>
        /// Gets the values as a 'bindable' list datasource.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns>
        /// IList for data binding
        /// </returns>
        public static IList GetListValues(this Type enumType)
        {            
            Type underlyingType = Enum.GetUnderlyingType(enumType);
            ArrayList values = new ArrayList();

            //Look for our string value associated with fields in this enum
            foreach (FieldInfo fi in enumType.GetFields())
            {
                //Check for our custom attribute
                if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attrs && attrs.Length > 0)
                {
                    values.Add(
                        new DictionaryEntry(
                            Convert.ChangeType(Enum.Parse(enumType, fi.Name), underlyingType),
                            attrs[0].Description));
                }
            }

            return values;
        }
        public static List<KeyValuePair<int, string>> GetEnumValuesAndDescriptions<T>()
        {
            Type enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T is not System.Enum");

            List<KeyValuePair<int, string>> enumValList = new List<KeyValuePair<int, string>>();

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                var fi = e.GetType().GetField(e.ToString());
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                enumValList.Add(new KeyValuePair<int, string>((int)e, (attributes.Length > 0) ? attributes[0].Description : e.ToString()));
            }

            return enumValList;
        }
        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
        }
    }   
}
