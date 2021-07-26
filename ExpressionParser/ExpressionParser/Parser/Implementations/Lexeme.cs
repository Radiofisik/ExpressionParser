using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser.Parser
{
   public class Lexeme
   {
      public Lexeme(LexemeType type, char value)
      {
         Type = type;
         Value = value.ToString();
      }

      public Lexeme(LexemeType type, string value)
      {
         Type = type;
         Value = value;
      }

      public LexemeType Type { get; }

      public string Value { get; }
   }
}
