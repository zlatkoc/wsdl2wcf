using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wsdl2wcf.Template
{
    class Generator
    {
        public static string Generate(
            Model.Wsdl wsdl,
            string namespace_data,
            string namespace_messages)
        {
            StringBuilder operations = new StringBuilder();
            StringBuilder parameters = new StringBuilder();

            foreach (Model.WsdlOperation op in wsdl.Operations)
            {
                string opTempl = Helper.Read("Operation.cs_template");
                string opResult = opTempl
                    .Replace("${Name}", op.Name)
                    .Replace("${Action}", op.Action)
                    .Replace("${Input}", op.Input.Name)
                    .Replace("${Output}", op.Output.Name);
                operations.Append(opResult);

                parameters.Append(GenerateParameter(op.Input, namespace_data));
                parameters.Append(GenerateParameter(op.Output, namespace_data));
            }

            string itfTempl = Helper.Read("Interface.cs_template");
            string itfResult = itfTempl
                .Replace("${Namespace.Message}", namespace_messages)
                .Replace("${PortTypeName}", wsdl.PortTypeName)
                .Replace("${PortTypeNameVariable}", wsdl.PortTypeNameVariable)
                .Replace("${TargetNamespace}", wsdl.TargetNamespace)
                .Replace("${Operations}", operations.ToString())
                .Replace("${Parameters}", parameters.ToString());

            return itfResult;
        }

        private static string GenerateParameter(
            Model.WsdlParameter p,
            string namespace_data)
        {
            string parTempl = Helper.Read("Parameter.cs_template");
            string parResult = parTempl
                .Replace("${Name}", p.Name)
                .Replace("${BodyElement}", p.BodyElement)
                .Replace("${BodyNamespace}", p.BodyNamespace)
                .Replace("${Namespace.Data}", namespace_data);
            return parResult;
        }
    }
}
