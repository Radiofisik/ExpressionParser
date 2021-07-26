using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionParser.Parser.Abstraction;

namespace ExpressionParser.Parser.Implementations
{
   internal class LexemeSplitter : ILexemeSplitter
   {
      public LexemeBuffer ParseLexemes(ReadOnlySpan<char> input)
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
                  else if (Char.IsLetter(input[current]))
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