using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionParser.Expression;
using ExpressionParser.Parser.Abstraction;
using ExpressionParser.Parser.Abstraction.PartialParsers;

namespace ExpressionParser.Parser
{
   public class FormulaParser : IFormulaParser
   {
      private readonly ILexemeSplitter _lexemeSplitter;
      private readonly IExprParser _exprParser;

      public FormulaParser(ILexemeSplitter lexemeSplitter, IExprParser exprParser)
      {
         _lexemeSplitter = lexemeSplitter;
         _exprParser = exprParser;
      }

      public BaseExpression Parse(string input)
      {
         return Parse(input.AsSpan());
      }

      private BaseExpression Parse(ReadOnlySpan<char> input)
      {
         /*------------------------------------------------------------------
              * PARSER RULES
          * *------------------------------------------------------------------*/

         //expr : plusminus* EOF ;
         //plusminus: multdiv ( ( '+' | '-' ) multdiv )* ;
         //multdiv : factor ( ( '*' | '/' ) factor )* ; // multdiv это factor дальше * или / и другой factor, при этом оператор и фактор могут повторяться бесконечно
         //func : name(expr(; expr)*)
         //factor : NUMBER | '(' expr ')' | func | [variable]; // factor это NUMBER или expr в скобках

         var lexemes = _lexemeSplitter.ParseLexemes(input);
         return _exprParser.Parse(lexemes);
      }

   }
}