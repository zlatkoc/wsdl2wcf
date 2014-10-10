using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wsdl2wcf.Model
{
    public class WsdlParameter
    {
        public string Name;
        public WsdlParameterType Type;
        public string BodyElement;
        public string BodyNamespace;
    }
}
