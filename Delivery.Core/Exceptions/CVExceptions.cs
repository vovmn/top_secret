using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Exceptions
{
    public class OCRProcessingException : Exception
    {
        public OCRProcessingException(string message, Exception inner)
            : base(message, inner) { }
    }

    public class ImageProcessingException : Exception
    {
        public ImageProcessingException(string message, Exception inner)
            : base(message, inner) { }
    }
}
