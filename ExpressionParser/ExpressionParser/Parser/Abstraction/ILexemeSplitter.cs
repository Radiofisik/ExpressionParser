using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser.Parser.Abstraction
{
   public interface ILexemeSplitter
   {
      LexemeBuffer ParseLexemes(ReadOnlySpan<char> input);
   }
}
