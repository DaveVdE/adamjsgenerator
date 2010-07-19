﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adam.JSGenerator.Tests
{
    [TestClass]
    public class PrecedenceTests
    {
        [TestMethod]
        public void PrecedenceAutomaticallyAppliesGrouping()
        {
            IdentifierExpression a = "a";

            Assert.AreEqual("(function(){})();", JS.Function().Call().ToString());
            Assert.AreEqual("(\"a\"+\"b\").length;", JS.String("a").AddWith("b").Dot("length").ToString());
            Assert.AreEqual("a[1]();", a.Index(1).Call().ToString());
            Assert.AreEqual("a==1?true:a==2?false:true;", a.IsEqualTo(1).Iif(true, a.IsEqualTo(2).Iif(false, true)).ToString());
            Assert.AreEqual("(a==1?1:2)+5;", a.IsEqualTo(1).Iif(1, 2).AddWith(5).ToString());
        }

        [TestMethod]
        public void PrecedenceSupportsEquals()
        {
            Precedence first = new Precedence { Association = Association.LeftToRight, Level = 1 };
            Precedence second = new Precedence { Association = Association.LeftToRight, Level = 1 };
            Precedence third = new Precedence { Association = Association.LeftToRight, Level = 2 };
            Precedence fourth = new Precedence { Association = Association.RightToLeft, Level = 2 };

            object fifth = null;
            object sixth = new Precedence {Association = Association.RightToLeft, Level = 2};

            Assert.IsTrue(first.Equals(second));
            Assert.IsFalse(second.Equals(third));
            Assert.IsFalse(third.Equals(fourth));
            Assert.IsFalse(fourth.Equals(fifth));
            Assert.IsTrue(fourth.Equals(sixth));

            Assert.IsTrue(first == second);
            Assert.IsFalse(first != second);
        }

        [TestMethod]
        public void PrecedenceSupportsHashCode()
        {
            Precedence first = new Precedence { Association = Association.LeftToRight, Level = 1 };
            Precedence second = new Precedence { Association = Association.LeftToRight, Level = 1 };

            Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
        }
    }
}
