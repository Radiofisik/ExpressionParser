using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionParser.Expression;
using ExpressionParser.Parser.Abstraction.PartialParsers;

namespace ExpressionParser.Parser.Implementations.PartialParsers
{
   class MultDivParser: IMultDivParser
   {
      public IFactorParser FactorParser { get; set; }

      public BaseExpression Parse(LexemeBuffer lexemes)
      {
         var result = FactorParser.Parse(lexemes);
         while (true)
         {
            var lexeme = lexemes.Next();
            switch (lexeme.Type)
            {
               case LexemeType.Multiply:
                  result = new MultiplyExpression(result, FactorParser.Parse(lexemes));
                  break;
               case LexemeType.Divide:
                  result = new DivideExpression(result, FactorParser.Parse(lexemes));
                  break;
               default:
                  lexemes.Back();
                  return result;
            }
         }
      }
   }
}
