using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionParser.Exceptions;
using ExpressionParser.Expression;
using ExpressionParser.Parser.Abstraction.PartialParsers;

namespace ExpressionParser.Parser.Implementations.PartialParsers
{
   class ConstantParser : IConstantParser
   {
      public BaseExpression Parse(LexemeBuffer lexemes)
      {
         var lexeme = lexemes.Next();
         if (lexeme.Type == LexemeType.Number)
         {
            var numberFormat = NumberFormatInfo.CurrentInfo.Clone() as NumberFormatInfo;
            numberFormat.NumberDecimalSeparator = lexeme.Value.Contains(',') ? "," : ".";

            if (!double.TryParse(lexeme.Value, NumberStyles.Float | NumberStyles.AllowThousands, numberFormat, out var result))
            {
               throw new WrongFormatException(lexeme.Value);
            }

            return new ConstExpression(result);
         }

         throw new WrongFormatException(lexeme.Value);
      }
   }
}