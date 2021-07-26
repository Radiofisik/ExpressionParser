using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser.Expression.Abstraction
{
   public abstract class BaseFunctionExpression: BaseExpression
   {
      public List<BaseExpression> Params { get; set; }
   }
}
