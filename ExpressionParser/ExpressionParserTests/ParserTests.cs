using System;
using System.Collections.Generic;
using Autofac;
using ExpressionParser.Expression;
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

      [Fact]
      public void ParenthesisAfter()
      {
         // Arrange
         var parser = BuildContainer().Resolve<IFormulaParser>();
         var str = "[c] + ([a] + [b])*2 + 5";
         var context = new Dictionary<string, object>
         {
            ["a"] = 1.0,
            ["b"] = 2.0,
            ["c"] = 3.0
         };

         // Act
         var exp = parser.Parse(str);
         var result = exp.Compute(context);

         // Assert
         result.Should()
            .Be(14.0);
      }

      [Fact]
      public void VariablesHardcore()
      {
         // Arrange
         var parser = BuildContainer().Resolve<IFormulaParser>();
         var str = "[a1]/[b1] + ([a2]/[b2]*[a3]*[b3] + [a4]/[b4]) * 2 - [a5]/[b5]";
         var context = new Dictionary<string, object>
         {
            ["a1"] = 35.0,
            ["b1"] = 7.0,

            ["a2"] = 5.0,
            ["b2"] = 1.0,

            ["a3"] = 5.0,
            ["b3"] = 2.0,

            ["a4"] = 28.0,
            ["b4"] = 7.0,

            ["a5"] = 5.0,
            ["b5"] = 5.0,
         };

         // Act
         var exp = parser.Parse(str);
         var result = exp.Compute(context);

         // Assert
         result.Should()
            .Be(35.0/7.0 + (5.0*5.0*2.0 + 28.0/7.0)*2 - 5.0/5.0);
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
