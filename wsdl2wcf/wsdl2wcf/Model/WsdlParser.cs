using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace wsdl2wcf.Model
{
    public class WsdlParser
    {
        public static Wsdl Parse(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("wsdl", "http://schemas.xmlsoap.org/wsdl/");
            ns.AddNamespace("wsdlsoap", "http://schemas.xmlsoap.org/wsdl/soap/");

            Wsdl model = new Wsdl();

            model.TargetNamespace = GetSingleNodeInnerText(
                doc, ns, "/wsdl:definitions/@targetNamespace");

            ns.AddNamespace("tns", model.TargetNamespace);
            ns.AddNamespace("schema", model.TargetNamespace);

            model.PortTypeName = GetSingleNodeInnerText(
                doc, ns, "/wsdl:definitions/wsdl:portType/@name");

            model.PortTypeNameVariable =
                model.PortTypeName.Replace(".", "");

            XmlNodeList operationList = doc.SelectNodes(
                "/wsdl:definitions/wsdl:binding/wsdl:operation", ns);

            foreach (XmlNode operation in operationList)
            {
                WsdlOperation op = new WsdlOperation();

                op.Name = GetSingleNodeInnerText(operation, ns, "@name");
                op.Action = GetSingleNodeInnerText(operation, ns, 
                    "./wsdlsoap:operation/@soapAction");

                op.Input = GetWsdlParameter(operation, ns, 
                    WsdlParameterType.input);

                op.Output = GetWsdlParameter(operation, ns, 
                    WsdlParameterType.output);

                model.Operations.Add(op);
            }

            return model;
        }

        private static string GetSingleNodeInnerText(
            XmlNode node,
            XmlNamespaceManager ns,
            string xpath)
        {
            return node.SelectSingleNode(xpath, ns).InnerText;
        }

        private static WsdlParameter GetWsdlParameter(
            XmlNode node,
            XmlNamespaceManager ns,
            WsdlParameterType type)
        {
            WsdlParameter p = new WsdlParameter();

            p.Name = GetSingleNodeInnerText(node, ns,
                "./wsdl:"+type+"/@name");
            p.Type = type;

            string selectMessage = string.Format(
                "/wsdl:definitions/wsdl:portType/wsdl:operation/wsdl:{0}[@name='{1}']/@message",
                type, p.Name);
            string message = GetSingleNodeInnerText(node.OwnerDocument, ns, selectMessage);

            string selectElement = string.Format(
                "/wsdl:definitions/wsdl:message[@name='{0}']/wsdl:part/@element",
                message.Replace("tns:", ""));

            string element = GetSingleNodeInnerText(node.OwnerDocument, ns, selectElement);

            p.BodyElement = element.Replace("schema:", "");
            p.BodyNamespace = ns.LookupNamespace("schema");

            return p;
        }
    }
}
