using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser.Expression
{
   public class VariableExpression : BaseExpression
   {
      public string Name { get; }

      public VariableExpression(string name)
      {
         Name = name;
      }

      public override double Compute(Dictionary<string, object> context)
      {
         return (double) context[Name];
      }

      public override string ToString() => $"Var({Name})";
   }
}
