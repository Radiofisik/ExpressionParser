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
               case ' ':
                  current++;
                  break;
               case '[':
                  var varStart = current;
                  while (current < input.Length && input[current]!=']')
                  {
                     current++;
                  }
                  lexemes.Add(new Lexeme(LexemeType.Variable, input.Slice(varStart + 1, current - varStart - 1).ToString()));
                  current++;
                  break;
               case '"':
                  var varQStart = current;
                  while (current < input.Length && (current == varQStart || input[current] != '"'))
                  {
                     current++;
                  }
                  lexemes.Add(new Lexeme(LexemeType.Variable, input.Slice(varQStart + 1, current - varQStart - 1).ToString()));
                  current++;
                  break;
               default:
                  if(current >= input.Length) break;
                  if (Char.IsDigit(input[current]))
                  {
                     var start = current;
                     while (current < input.Length && (char.IsDigit(input[current]) || input[current] == '.' || input[current] == ','))
                     {
                        current++;
                     }

                     lexemes.Add(new Lexeme(LexemeType.Number, input.Slice(start, current - start).ToString()));
                  }
                  else if (Char.IsLetter(input[current]))
                  {
                     var start = current;
                     while (current < input.Length && (char.IsLetter(input[current]) || input[current] == '_'))
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