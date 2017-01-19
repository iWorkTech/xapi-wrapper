#region License and Warranty Information

// ==========================================================
//  <copyright file="AgentTest.cs" company="iWork Technologies">
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

using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TinCan;
using TinCan.Json;

#endregion

namespace TinCanTests
{
    /// <summary>
    /// Class AgentTest.
    /// </summary>
    [TestFixture]
    public class AgentTest
    {
        /// <summary>
        /// Tests the empty CTR.
        /// </summary>
        [Test]
        public void TestEmptyCtr()
        {
            var obj = new Agent();
            Assert.IsInstanceOf<Agent>(obj);
            Assert.IsNull(obj.Mbox);

            StringAssert.AreEqualIgnoringCase("{\"objectType\":\"Agent\"}", obj.ToJSON());
        }

        /// <summary>
        /// Tests the j object CTR.
        /// </summary>
        [Test]
        public void TestJObjectCtr()
        {
            var mbox = "mailto:tincancsharp@tincanapi.com";

            var cfg = new JObject();
            cfg.Add("mbox", mbox);

            var obj = new Agent(cfg);
            Assert.IsInstanceOf<Agent>(obj);
            Assert.That(obj.Mbox, Is.EqualTo(mbox));
        }

        /// <summary>
        /// Tests the string of json CTR.
        /// </summary>
        [Test]
        public void TestStringOfJSONCtr()
        {
            var mbox = "mailto:tincancsharp@tincanapi.com";

            var json = "{\"mbox\":\"" + mbox + "\"}";
            var strOfJson = new StringOfJSON(json);

            var obj = new Agent(strOfJson);
            Assert.IsInstanceOf<Agent>(obj);
            Assert.That(obj.Mbox, Is.EqualTo(mbox));
        }
    }
}