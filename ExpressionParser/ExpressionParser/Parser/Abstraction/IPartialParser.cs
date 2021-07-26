using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionParser.Expression;

namespace ExpressionParser.Parser.Abstraction
{
   public interface IPartialParser
   {
      BaseExpression Parse(LexemeBuffer lexemes);
   }
}