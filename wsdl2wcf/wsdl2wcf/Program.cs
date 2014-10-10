using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace wsdl2wcf
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            if (args.Length != 2)
            {
                PrintUsage();
                return;
            }
             */

            string inWsdl = @"OE2.Service.ProductOffering.wsdl";// args[0];
            string outCSharp = @"test.cs";// args[1];

            Model.Wsdl model = Model.WsdlParser.Parse(inWsdl);

            Console.WriteLine(Template.Generator.Generate(model, "TestNs.Data", "TestNs.Msgs"));


        }

        private static void PrintUsage()
        {
            Console.WriteLine("usage: xxx");
        }
    }
}
