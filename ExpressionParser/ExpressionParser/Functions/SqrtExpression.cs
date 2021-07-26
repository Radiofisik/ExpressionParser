using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionParser.Expression.Abstraction;

namespace ExpressionParser.Expression
{
   [FunctionName("sqrt")]
   public class SqrtExpression: BaseFunctionExpression
   {

      public override double Compute(Dictionary<string, object> context)
      {
         return  Math.Sqrt(Params.First().Compute(context));
      }
   }
}
