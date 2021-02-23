using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace FileapiCli
{
    public static class Helper
    {
        public static bool IsBase64Sting(string str)
        {
            try 
            {
                Convert.FromBase64String(str);
                return true;
            } 
            catch 
            { 
                return false;
            
            }
        }

        public static object GetSafeValue(PropertyInfo prop, string defaultVal)
        {
            Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            object safeValue = (defaultVal == null) ? null : Convert.ChangeType(defaultVal, t);
            return safeValue;
        }

        /// <summary>
        ///  Update the values in appsettings, Mainly used to set the password encrypted value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void UpdateSettingsValue(string key, dynamic value)
        {
            try
            {

                string filePath = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\appsettings.json ";
                string json = File.ReadAllText(filePath);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                var splittedKey = key.Split(":");
                if (splittedKey.Length > 1)
                {
                    var sectionPath = key.Split(":")[0];
                    var keyPath = key.Split(":")[1];
                    jsonObj[sectionPath][keyPath] = value;
                }
                else
                {
                    jsonObj[key] = value; // if no sectionpath just set the value
                }
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error writing app settings");
                throw;
            }
        }

    }
}
