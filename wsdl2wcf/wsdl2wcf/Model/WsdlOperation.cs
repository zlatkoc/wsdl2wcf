using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wsdl2wcf.Model
{
    public class WsdlOperation
    {
        public string Name;
        public string Action;
        public WsdlParameter Input;
        public WsdlParameter Output;
        public WsdlParameter Fault;
    }
}
