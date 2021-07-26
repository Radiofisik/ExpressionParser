using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using ExpressionParser.Expression;
using ExpressionParser.Expression.Abstraction;
using ExpressionParser.Parser.Abstraction.PartialParsers;

namespace ExpressionParser.Parser.Implementations.PartialParsers
{
   class FunctionParser: IFunctionParser
   {
      public IExprParser ExprParser { get; set; }

      private IIndex<string, BaseFunctionExpression> _functions;

      public FunctionParser(IIndex<string, BaseFunctionExpression> functions)
      {
         _functions = functions;
      }

      public BaseExpression Parse(LexemeBuffer lexemes)
      {
         var lexeme = lexemes.Next();
         var funcName = lexeme.Value;

         lexeme = lexemes.Next();
         if (lexeme.Type != LexemeType.LeftBracket)
         {
            throw new Exception($"( expected at position {lexemes.Position}");
         }
         var funcParams = new List<BaseExpression>();
         while (true)
         {
            funcParams.Add(ExprParser.Parse(lexemes));
            lexeme = lexemes.Next();
            if (lexeme.Type == LexemeType.ParamSeparator)
            {
               continue;
            }
            if (lexeme.Type != LexemeType.RightBracket)
            {
               throw new Exception($") expected at position {lexemes.Position}");
            }
            var funcExp = _functions[funcName];
            funcExp.Params = funcParams;
            return funcExp;
         }
      }

   }
}
