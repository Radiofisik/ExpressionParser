using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionParser.Expression;
using ExpressionParser.Parser.Abstraction;

namespace ExpressionParser.Parser
{
   public class FormulaParser : IFormulaParser
   {
      public BaseExpression Parse(string input)
      {
         return Parse(input.AsSpan());
      }

      private BaseExpression Parse(ReadOnlySpan<char> input)
      {
         /*------------------------------------------------------------------
              * PARSER RULES
          * *------------------------------------------------------------------*/

         //    expr : plusminus* EOF ;
         //
         //    plusminus: multdiv ( ( '+' | '-' ) multdiv )* ;
         //
         //    multdiv : factor ( ( '*' | '/' ) factor )* ;
         //
         //    factor : NUMBER | '(' expr ')' ;

         var lexemes = ParseLexemes(input);
         return Expr(lexemes);
      }

      private BaseExpression Expr(LexemeBuffer lexemes)
      {
         var lexeme = lexemes.Next();
         if (lexeme.Type == LexemeType.End)
         {
            throw new Exception("expression is empy");
         }

         lexemes.Back();
         return PlusMinus(lexemes);
      }

      private BaseExpression PlusMinus(LexemeBuffer lexemes)
      {
         var result = Multiply(lexemes);
         while (true)
         {
            var lexeme = lexemes.Next();
            switch (lexeme.Type)
            {
               case LexemeType.Plus:
                  result = new PlusExpression(result, Multiply(lexemes));
                  break;
               case LexemeType.Minus:
                  result = new MinusExpression(result, Multiply(lexemes));
                  break;
               default:
                  lexemes.Back();
                  return result;
            }
         }
      }

      private BaseExpression Multiply(LexemeBuffer lexemes)
      {
         var result = Factor(lexemes);
         while (true)
         {
            var lexeme = lexemes.Next();
            switch (lexeme.Type)
            {
               case LexemeType.Multiply:
                  result = new MultiplyExpression(result, Factor(lexemes));
                  break;
               case LexemeType.Divide:
                  result = new DivideExpression(result, Factor(lexemes));
                  break;
               default:
                  lexemes.Back();
                  return result;
            }
         }
      }

      private BaseExpression Func(LexemeBuffer lexemes)
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
            funcParams.Add(Expr(lexemes));
            lexeme = lexemes.Next();
            if (lexeme.Type == LexemeType.ParamSeparator)
            {
               continue;
            }
            if (lexeme.Type != LexemeType.RightBracket)
            {
               throw new Exception($") expected at position {lexemes.Position}");
            }
            return  new FuncExpression(funcName, funcParams);
         }

      }

      private BaseExpression Factor(LexemeBuffer lexemes)
      {
         var lexeme = lexemes.Next();
         switch (lexeme.Type)
         {
            case LexemeType.Number:
               return new ConstExpression(double.Parse(lexeme.Value));
            case LexemeType.LeftBracket:
               var result = new ParenthesisExpression(Expr(lexemes));
               lexeme = lexemes.Next();
               if (lexeme.Type != LexemeType.RightBracket)
               {
                  throw new Exception($") expected at position {lexemes.Position}");
               }
               return result;
            
            case LexemeType.Func:
               lexemes.Back();
               return Func(lexemes);

            default:
               throw new Exception($"syntax error at {lexemes.Position}");
         }
      }

      private LexemeBuffer ParseLexemes(ReadOnlySpan<char> input)
      {
         var lexemes = new LexemeBuffer();
         var current = 0;
         while (current < input.Length)
         {
            switch (input[current])
            {
               case '(':
                  lexemes.Add(new Lexeme(LexemeType.LeftBracket, input[current]));
                  current++;
                  break;
               case ')':
                  lexemes.Add(new Lexeme(LexemeType.RightBracket, input[current]));
                  current++;
                  break;
               case '+':
                  lexemes.Add(new Lexeme(LexemeType.Plus, input[current]));
                  current++;
                  break;
               case '-':
                  lexemes.Add(new Lexeme(LexemeType.Minus, input[current]));
                  current++;
                  break;
               case '*':
                  lexemes.Add(new Lexeme(LexemeType.Multiply, input[current]));
                  current++;
                  break;
               case '/':
                  lexemes.Add(new Lexeme(LexemeType.Divide, input[current]));
                  current++;
                  break;
               case ';':
                  lexemes.Add(new Lexeme(LexemeType.ParamSeparator, input[current]));
                  current++;
                  break;
               default:
                  if (Char.IsDigit(input[current]))
                  {
                     var start = current;
                     while (current < input.Length && char.IsDigit(input[current]) || input[current] == '.' || input[current] == ',')
                     {
                        current++;
                     }

                     lexemes.Add(new Lexeme(LexemeType.Number, input.Slice(start, current - start).ToString()));
                  }
                  else if(Char.IsLetter(input[current]))
                  {
                     var start = current;
                     while (current < input.Length && char.IsLetter(input[current]))
                     {
                        current++;
                     }

                     lexemes.Add(new Lexeme(LexemeType.Func, input.Slice(start, current - start).ToString()));
                  }

                  break;
            }
         }

         lexemes.Add(new Lexeme(LexemeType.End, string.Empty));

         return lexemes;
      }
   }
}