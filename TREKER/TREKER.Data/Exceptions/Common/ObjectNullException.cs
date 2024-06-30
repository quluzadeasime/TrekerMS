using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREKER.DAL.Exceptions.Common
{
    public class ObjectNullException : Exception
    {
        public string PropertyName { get; set; }
        public ObjectNullException(string? message, string propName) : base(message)
        {
            PropertyName = propName ?? string.Empty;
        }
    }
}
