using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser.Exceptions
{
   public class WrongFormatException : Exception
   {
      public string SourceCode { get; }

      public WrongFormatException(string sourceCode)
      {
         SourceCode = sourceCode;
      }
   }
}
