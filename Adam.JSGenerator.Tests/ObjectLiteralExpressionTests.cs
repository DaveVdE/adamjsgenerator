﻿using System;
using System.Collections.Generic;
using Adam.JSGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adam.JSGenerator.Tests
{
    [TestClass]
    public class ObjectLiteralExpressionTests
    {
        [TestMethod]
        public void ObjectLiteralExpression_Produces_Empty_Object()
        {
            var expression = new ObjectLiteralExpression();

            Assert.AreEqual("{};", expression.ToString());
        }

        [TestMethod]
        public void ObjectLiteralExpression_Produces_ObjectLiterals()
        {
            var expression = new ObjectLiteralExpression(new Dictionary<Expression, Expression>
            {
                {JS.Id("a"), JS.Number(12)},
                {JS.Id("b"), JS.String("Wrong!")},
                {JS.Id("c"), null}
            });

            Assert.AreEqual(3, expression.Properties.Count);
            Assert.AreEqual("{a:12,b:\"Wrong!\",c:null};", expression.ToString());
            
            expression.Properties = new Dictionary<Expression, Expression>
            {
                {JS.Id("a"), JS.Number(12)}                
            };

            Assert.AreEqual(1, expression.Properties.Count);
            Assert.AreEqual("{a:12};", expression.ToString());            
        }

        [TestMethod]
        public void ObjectLiteralExpression_Has_Helpers()
        {
            var expression = new ObjectLiteralExpression();

            Assert.AreEqual("{};", expression.ToString());

            expression = expression.WithProperty(JS.Id("name"), "value");

            Assert.AreEqual("{name:\"value\"};", expression.ToString());

            expression = expression.WithProperties(new Dictionary<Expression, Expression>
            {
                {JS.Id("key"), "value"},
                {JS.Id("price"), 1200}
            });

            Assert.AreEqual("{name:\"value\",key:\"value\",price:1200};", expression.ToString());

            expression = new ObjectLiteralExpression().WithProperties(new
            {
                Key = "Value"
            });

            Assert.AreEqual("{Key:\"Value\"};", expression.ToString());
        }

        [TestMethod]
        public void ObjectLiteralExpression_Helpers_Require_Expression()
        {
            ObjectLiteralExpression expression = null;
            Expect.Throw<ArgumentNullException>(() => expression.WithProperty("name", "value"));
            Expect.Throw<ArgumentNullException>(() => expression.WithProperties(new Dictionary<Expression, Expression>()));
            Expect.Throw<ArgumentNullException>(() => expression.WithProperties(new object()));
        }
    }
}
