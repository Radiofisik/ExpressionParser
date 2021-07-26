using System;
using System.Collections.Generic;
using Autofac;
using ExpressionParser.Expression;
using ExpressionParser.Parser;
using ExpressionParser.Parser.Abstraction;

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


         var cb = new ContainerBuilder();
         cb.RegisterModule<ParserModule>();
         var container = cb.Build();
         
         
         var parser = container.Resolve<IFormulaParser>();
         var exp = parser.Parse("2*min(sqrt(25);10+3)");
         var result = exp.Compute(new Dictionary<string, object>());


      }
   }
}
