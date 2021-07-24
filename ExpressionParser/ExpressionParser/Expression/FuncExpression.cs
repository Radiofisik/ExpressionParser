using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser.Expression
{
   public class FuncExpression: BaseExpression
   {
      private readonly List<BaseExpression> _params;
      private string _name;

      public FuncExpression(string name, List<BaseExpression> @params)
      {
         _params = @params;
         _name = name;
      }

      public override double Compute(Dictionary<string, object> context)
      {
         if (_name == "sqrt")
         {
            return  Math.Sqrt(_params.First().Compute(context));
         }
         if (_name == "min")
         {
            return  Math.Min(_params[0].Compute(context), _params[1].Compute(context));
         }
         else
         {
            throw new Exception($"function with name {_name} was not found");
         }
        
      }
   }
}
