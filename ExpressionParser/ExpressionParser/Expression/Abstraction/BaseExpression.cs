using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser.Expression
{
   public abstract class BaseExpression
   {
      public abstract double Compute(Dictionary<string, object> context);
   }
}
