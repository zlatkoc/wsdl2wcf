using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wsdl2wcf.Model
{
    public class Wsdl
    {
        public string TargetNamespace;
        public string PortTypeName;
        public string PortTypeNameVariable;

        public List<WsdlOperation> Operations = new List<WsdlOperation>();
    }
}
