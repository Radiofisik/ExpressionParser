using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionParser.Exceptions;

namespace ExpressionParser.Expression
{
   public class DivideExpression : BaseBinaryExpression
   {
      public DivideExpression(BaseExpression left, BaseExpression right) : base(left, right)
      {
      }

      public override double Compute(Dictionary<string, object> context)
      {
         return Left.Compute(context) / Right.Compute(context);
      }

      public override string ToString() => $"{Left} / {Right}";
   }
}
