using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser.Expression
{
   public class ParenthesisExpression : BaseExpression
   {
      public BaseExpression Expression { get; }

      public ParenthesisExpression(BaseExpression expression)
      {
         Expression = expression;
      }

      public override double Compute(Dictionary<string, object> context)
      {
         return Expression?.Compute(context) ?? 0.0;
      }

      public override string ToString() => $"({Expression})";
   }
}
