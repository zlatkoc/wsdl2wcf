using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace wsdl2wcf.Template
{
    class Helper
    {
        public static string Read(
            string template)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            TextReader textReader = new StreamReader(
                assembly.GetManifestResourceStream(
                "wsdl2wcf.Template." + template));

            string result = textReader.ReadToEnd();
            textReader.Close();

            return result;
        }
    }
}
