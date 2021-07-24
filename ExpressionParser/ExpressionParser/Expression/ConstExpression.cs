using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser.Expression
{
   public class ConstExpression : BaseExpression
   {
      public double Value { get; }

      public ConstExpression(double value)
      {
         Value = value;
      }

      public override double Compute(Dictionary<string, object> context)
      {
         return Value;
      }

      public override string ToString() => $"Const({Value})";
   }
}
