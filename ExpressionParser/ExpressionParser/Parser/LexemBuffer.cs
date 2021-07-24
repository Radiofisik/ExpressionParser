using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser.Parser
{
   public class LexemeBuffer
   {
      private int _position;
      private readonly List<Lexeme> _lexemes;

      public LexemeBuffer() {
         _lexemes = new List<Lexeme>();
         _position = 0;
      }

      public void Add(Lexeme lexeme)
      {
         _lexemes.Add(lexeme);
      }

      public Lexeme Next() {
         return _lexemes[_position++];
      }

      public Lexeme Back() {
         return _lexemes[--_position];
      }

      public int Position => _position;
   }
}
