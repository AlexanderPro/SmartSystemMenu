using System;
using System.Reflection;
using System.IO;

namespace SmartSystemMenu
{
    static class AssemblyUtils
    {
        public static string AssemblyLocation
        {
            get
            {
                string location = Assembly.GetExecutingAssembly().Location;
                return location;
            }
        }

        public static string AssemblyDirectory
        {
            get
            {
                string directory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return directory;
            }
        }

        public static string AssemblyTitle
        {
            get
            {
                Object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                string title = ((AssemblyTitleAttribute)attributes[0]).Title;
                return title;
            }
        }

        public static string AssemblyProductName
        {
            get
            {
                Object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                string productName = ((AssemblyProductAttribute)attributes[0]).Product;
                return productName;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                Object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                string copyright = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
                return copyright;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                Object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                string company = ((AssemblyCompanyAttribute)attributes[0]).Company;
                return company;
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return version;
            }
        }

        public static void ExtractFileFromAssembly(string resourceName, string path)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            FileStream outputFileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Stream resouceStream = currentAssembly.GetManifestResourceStream(resourceName);
            resouceStream.CopyTo(outputFileStream);
            resouceStream.Close();
            outputFileStream.Close();
        }
    }
}
