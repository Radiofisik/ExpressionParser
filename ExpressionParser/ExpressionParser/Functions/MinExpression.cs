using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionParser.Expression.Abstraction;

namespace ExpressionParser.Expression
{
   [FunctionName("min")]
   public class MinExpression: BaseFunctionExpression
   {
      public override double Compute(Dictionary<string, object> context)
      {
         return  Math.Min(Params[0].Compute(context), Params[1].Compute(context));
      }
   }
}
