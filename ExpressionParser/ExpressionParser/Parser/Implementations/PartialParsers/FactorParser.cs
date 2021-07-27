using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionParser.Expression;
using ExpressionParser.Parser.Abstraction.PartialParsers;

namespace ExpressionParser.Parser.Implementations.PartialParsers
{
   class FactorParser: IFactorParser
   {
      public IExprParser ExprParser { get; set; }
      public IFunctionParser FunctionParser { get; set; }

      public IConstantParser ConstantParser { get; set; }

      public BaseExpression Parse(LexemeBuffer lexemes)
      {
         var lexeme = lexemes.Next();
         switch (lexeme.Type)
         {
            case LexemeType.Number:
               lexemes.Back();
               return ConstantParser.Parse(lexemes);
            case LexemeType.Variable:
               return new VariableExpression(lexeme.Value);
            case LexemeType.LeftBracket:
               var result = new ParenthesisExpression(ExprParser.Parse(lexemes));
               lexeme = lexemes.Next();
               if (lexeme.Type != LexemeType.RightBracket)
               {
                  throw new Exception($") expected at position {lexemes.Position}");
               }
               return result;
            
            case LexemeType.Func:
               lexemes.Back();
               return FunctionParser.Parse(lexemes);

            default:
               throw new Exception($"syntax error at {lexemes.Position}");
         }
      }
   }
}
