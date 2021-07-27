using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionParser.Exceptions;
using ExpressionParser.Expression;
using ExpressionParser.Parser.Abstraction.PartialParsers;

namespace ExpressionParser.Parser.Implementations.PartialParsers
{
   class MultDivParser : IMultDivParser
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
                  var right = FactorParser.Parse(lexemes);
                  if (right is ConstExpression {Value: 0})
                  {
                     throw new ParserDivideByZeroException();
                  }
                  result = new DivideExpression(result, right);
                  break;
               default:
                  lexemes.Back();
                  return result;
            }
         }
      }
   }
}