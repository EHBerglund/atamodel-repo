using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatamodelBuild.Exceptions
{
    public class CustomCOMException : System.Runtime.InteropServices.COMException
    {
        public CustomCOMException() : base() { }
        public CustomCOMException(string message) : base(message) { }
        public CustomCOMException(string message, System.Exception inner) : base(message, inner) { }

    }
}
