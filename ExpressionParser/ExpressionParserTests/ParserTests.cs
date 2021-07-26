using System;
using System.Collections.Generic;
using Autofac;
using ExpressionParser.Parser;
using ExpressionParser.Parser.Abstraction;
using FluentAssertions;
using Xunit;

namespace ExpressionParserTests
{
   public class ParserTests
   {
      [Fact]
      public void SqrtTest()
      {
         var parser = BuildContainer().Resolve<IFormulaParser>();

         var exp = parser.Parse("sqrt(25)");
         var result = exp.Compute(new Dictionary<string, object>());

         result.Should().Be(5);
      }

      [Fact]
      public void FunctionsTest()
      {
         var parser = BuildContainer().Resolve<IFormulaParser>();

         var exp = parser.Parse("2*min(sqrt(25);10+3)");
         var result = exp.Compute(new Dictionary<string, object>());

         result.Should().Be(10);
      }


      private IContainer BuildContainer()
      {
         var cb = new ContainerBuilder();
         cb.RegisterModule<ParserModule>();
         var container = cb.Build();
         return container;
      }
   }
}
