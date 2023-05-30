using System.Text;
using System.IO;
using System.Xml.Linq;

namespace SmartSystemMenu.Utils
{
    internal static class FileUtils
    {
        public static void Save(string fileName, XDocument document)
        {
            using var writer = new Utf8StringWriter();
            document.Save(writer, SaveOptions.None);
            File.WriteAllText(fileName, writer.ToString());
        }

        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }
    }
}
