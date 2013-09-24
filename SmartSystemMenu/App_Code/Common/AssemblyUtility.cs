using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace SmartSystemMenu.App_Code.Common
{
    static class AssemblyUtility
    {
        public static String AssemblyLocation
        {
            get
            {
                String location = Assembly.GetExecutingAssembly().Location;
                return location;
            }
        }

        public static String AssemblyDirectory
        {
            get
            {
                String directory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return directory;
            }
        }

        public static String AssemblyTitle
        {
            get
            {
                Object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length == 0)
                {
                    return String.Empty;
                }
                String title = ((AssemblyTitleAttribute)attributes[0]).Title;
                return title;
            }
        }

        public static String AssemblyProductName
        {
            get
            {
                Object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return String.Empty;
                }
                String productName = ((AssemblyProductAttribute)attributes[0]).Product;
                return productName;
            }
        }

        public static String AssemblyCopyright
        {
            get
            {
                Object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return String.Empty;
                }
                String copyright = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
                return copyright;
            }
        }

        public static String AssemblyCompany
        {
            get
            {
                Object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return String.Empty;
                }
                String company = ((AssemblyCompanyAttribute)attributes[0]).Company;
                return company;
            }
        }

        public static String AssemblyVersion
        {
            get
            {
                String version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return version;
            }
        }

        public static void ExtractFileFromAssembly(String resourceName, String path)
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
