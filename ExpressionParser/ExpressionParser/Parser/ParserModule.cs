using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ExpressionParser.Expression;
using ExpressionParser.Expression.Abstraction;
using ExpressionParser.Parser.Abstraction;
using ExpressionParser.Parser.Implementations;
using Module = Autofac.Module;

namespace ExpressionParser.Parser
{
   public class ParserModule: Module
   {
      protected override void Load(ContainerBuilder builder)
      {
         builder.RegisterType<FormulaParser>().AsImplementedInterfaces();
         builder.RegisterType<LexemeSplitter>().AsImplementedInterfaces();

         var parsers = ThisAssembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && typeof(IPartialParser).IsAssignableFrom(type))
            .ToArray();

         builder.RegisterTypes(parsers)
            .AsImplementedInterfaces()
            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
            .InstancePerLifetimeScope();

         var functions = ThisAssembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && typeof(BaseFunctionExpression).IsAssignableFrom(type))
            .ToArray();

         foreach (var function in functions)
         {
            var functionName = (CustomAttributeExtensions.GetCustomAttribute<FunctionNameAttribute>((MemberInfo)function, true)).Name;
            builder.RegisterType(function)
               .As<BaseFunctionExpression>()
               .Keyed<BaseFunctionExpression>(functionName)
               .InstancePerLifetimeScope();
         }
        

      }
   }
}
