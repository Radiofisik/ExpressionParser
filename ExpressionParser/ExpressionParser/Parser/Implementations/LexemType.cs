using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser.Parser
{
   public enum LexemeType
   {
      LeftBracket, RightBracket,
      Plus, Minus, Multiply, Divide,
      Number,
      Func, ParamSeparator,
      Variable,
      End
   }
}
