using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionParser.Expression;
using ExpressionParser.Parser.Abstraction.PartialParsers;

namespace ExpressionParser.Parser.Implementations.PartialParsers
{
   class ExprParser: IExprParser
   {
      public IPlusMinusParser PlusMinusParser { get; set; }
      public BaseExpression Parse(LexemeBuffer lexemes)
      {
         var lexeme = lexemes.Next();
         if (lexeme.Type == LexemeType.End)
         {
            throw new Exception("expression is empty");
         }

         lexemes.Back();
         return PlusMinusParser.Parse(lexemes);
      }
   }
}
