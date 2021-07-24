using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser.Expression
{
   public abstract class BaseBinaryExpression : BaseExpression
   {
      public BaseExpression Left { get; }

      public BaseExpression Right { get; }

      protected BaseBinaryExpression(BaseExpression left, BaseExpression right)
      {
         Left = left;
         Right = right;
      }
   }
}
