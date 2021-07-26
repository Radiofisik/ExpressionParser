using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionParser.Expression;
using ExpressionParser.Parser.Abstraction.PartialParsers;

namespace ExpressionParser.Parser.Implementations.PartialParsers
{
   class PlusMinusParser: IPlusMinusParser
   {
      public IMultDivParser MultDivParser { get; set; }
      public BaseExpression Parse(LexemeBuffer lexemes)
      {
         var result = MultDivParser.Parse(lexemes);
         while (true)
         {
            var lexeme = lexemes.Next();
            switch (lexeme.Type)
            {
               case LexemeType.Plus:
                  result = new PlusExpression(result, MultDivParser.Parse(lexemes));
                  break;
               case LexemeType.Minus:
                  result = new MinusExpression(result, MultDivParser.Parse(lexemes));
                  break;
               default:
                  lexemes.Back();
                  return result;
            }
         }
      }
   }
}
