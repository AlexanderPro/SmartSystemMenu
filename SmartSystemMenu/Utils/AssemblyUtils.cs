using System.Reflection;
using System.IO;
using System.Linq;

namespace SmartSystemMenu
{
    static class AssemblyUtils
    {
        public static string AssemblyLocation => Assembly.GetExecutingAssembly().Location;

        public static string AssemblyDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public static string AssemblyTitle => Assembly
            .GetExecutingAssembly()
            .GetCustomAttributes(typeof(AssemblyTitleAttribute), false)
            .OfType<AssemblyTitleAttribute>()
            .FirstOrDefault()?.Title ?? string.Empty;

        public static string AssemblyProductName => Assembly
            .GetExecutingAssembly()
            .GetCustomAttributes(typeof(AssemblyProductAttribute), false)
            .OfType<AssemblyProductAttribute>()
            .FirstOrDefault()?.Product ?? string.Empty;

        public static string AssemblyCopyright => Assembly
            .GetExecutingAssembly()
            .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)
            .OfType<AssemblyCopyrightAttribute>()
            .FirstOrDefault()?.Copyright ?? string.Empty;

        public static string AssemblyCompany => Assembly
            .GetExecutingAssembly()
            .GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)
            .OfType<AssemblyCompanyAttribute>()
            .FirstOrDefault()?.Company ?? string.Empty;

        public static string AssemblyProductVersion
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }

        public static void ExtractFileFromAssembly(string resourceName, string path)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var outputFileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            var resouceStream = currentAssembly.GetManifestResourceStream(resourceName);
            resouceStream.CopyTo(outputFileStream);
            resouceStream.Close();
            outputFileStream.Close();
        }
    }
}
