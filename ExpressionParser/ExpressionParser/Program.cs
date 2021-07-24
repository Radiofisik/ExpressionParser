using System;
using System.Collections.Generic;
using ExpressionParser.Expression;
using ExpressionParser.Parser;

namespace ExpressionParser
{
   class Program
   {
      static void Main(string[] args)
      {
        
         //2*(2+3)
         //var exp = new MultiplyExpression( new ConstExpression(2),
         //   new ParenthesisExpression(new PlusExpression(new ConstExpression(2), new ConstExpression(3))));
         //var result = exp.Compute(new Dictionary<string, object>());



         var parser = new FormulaParser();
         var exp = parser.Parse("2*min(25;10)");
         var result = exp.Compute(new Dictionary<string, object>());


      }
   }
}
