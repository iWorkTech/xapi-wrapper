#region License and Warranty Information

// ==========================================================
//  <copyright file="StatementTest.cs" company="iWork Technologies">
//  Copyright (c) 2015 All Right Reserved, http://www.iworktech.com/
//
//  This source is subject to the iWork Technologies Permissive License.
//  Please see the License.txt file for more information.
//  All other rights reserved.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//
//  </copyright>
//  <author>iWorkTech Dev</author>
//  <email>info@iworktech.com</email>
//  <date>2017-01-05</date>
// ===========================================================

#endregion

#region

using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

#endregion

namespace TinCan.Standard.Tests
{ /// <summary>
  /// Class StatementTest.
  /// </summary>
    [TestFixture]
    public class StatementTest
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [SetUp]
        public void Init()
        {
            Console.WriteLine("Running " + TestContext.CurrentContext.Test.FullName);
        }

        /// <summary>
        /// Tests the empty CTR.
        /// </summary>
        [Test]
        public void TestEmptyCtr()
        {
            var obj = new Statement();
            Assert.IsInstanceOf<Statement>(obj);
            Assert.IsNull(obj.ID);
            Assert.IsNull(obj.Actor);
            Assert.IsNull(obj.Verb);
            Assert.IsNull(obj.Target);
            Assert.IsNull(obj.Result);
            Assert.IsNull(obj.Context);
            Assert.IsNull(obj.Version);
            Assert.IsNull(obj.Timestamp);
            Assert.IsNull(obj.Stored);

            StringAssert.AreEqualIgnoringCase("{\"version\":\"1.0.1\"}", obj.ToJSON());
        }

        /// <summary>
        /// Tests the j object CTR sub statement.
        /// </summary>
        [Test]
        public void TestJObjectCtrSubStatement()
        {
            var cfg = new JObject
            {
                {"actor", Support.Agent.ToJObject()},
                {"verb", Support.Verb.ToJObject()},
                {"object", Support.SubStatement.ToJObject()}
            };

            var obj = new Statement(cfg);
            Assert.IsInstanceOf<Statement>(obj);
            Assert.IsInstanceOf<SubStatement>(obj.Target);
        }
    }
}