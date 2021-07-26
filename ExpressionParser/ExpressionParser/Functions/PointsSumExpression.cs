using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionParser.Expression;
using ExpressionParser.Expression.Abstraction;

namespace ExpressionParser.Functions
{
   [FunctionName("sum_b")]
   public class PointsSumExpression : BaseFunctionExpression
   {
      public override double Compute(Dictionary<string, object> context)
      {
         var variable = Params[0] as VariableExpression;
         var points = (List<double>) context[variable.Name];
         var result = 0.0;

         for (int i = 0; i < points.Count; i++)
         {
            result += points[i];
         }

         return result;
      }
   }
}
