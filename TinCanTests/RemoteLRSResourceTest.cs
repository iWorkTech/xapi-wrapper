#region License and Warranty Information

// ==========================================================
//  <copyright file="RemoteLRSResourceTest.cs" company="iWork Technologies">
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
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TinCan;
using TinCan.Documents;

#endregion

namespace TinCanTests
{
    /// <summary>
    /// Class RemoteLRSResourceTest.
    /// </summary>
    [TestFixture]
    public class RemoteLRSResourceTest
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [SetUp]
        public void Init()
        {
            Console.WriteLine("Running " + TestContext.CurrentContext.Test.FullName);

            //
            // these are credentials used by the other OSS libs when building via Travis-CI
            // so are okay to include in the repository, if you wish to have access to the
            // results of the test suite then supply your own endpoint, username, and password
            //
            _lrs = new RemoteLRS(
                "https://cloud.scorm.com/tc/U2S4SI5FY0/sandbox/",
                "Nja986GYE1_XrWMmFUE",
                "Bd9lDr1kjaWWY6RID_4"
                );

            //_lrs = new RemoteLRS(
            //    "https://lrs.adlnet.gov/xAPI/",
            //    "Nja986GYE1_XrWMmFUE",
            //    "Bd9lDr1kjaWWY6RID_4"
            //);
        }

        /// <summary>
        /// The LRS
        /// </summary>
        RemoteLRS _lrs;

        /// <summary>
        /// Tests the about.
        /// </summary>
        [Test]
        public void TestAbout()
        {
            var lrsRes = _lrs.About();
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the about failure.
        /// </summary>
        [Test]
        public void TestAboutFailure()
        {
            _lrs.Endpoint = new Uri("http://cloud.scorm.com/tc/3TQLAI9/sandbox/");

            var lrsRes = _lrs.About();
            Assert.IsFalse(lrsRes.Success);
            Console.WriteLine("TestAboutFailure - errMsg: " + lrsRes.ErrMsg);
        }

        /// <summary>
        /// Tests the state of the clear.
        /// </summary>
        [Test]
        public void TestClearState()
        {
            var lrsRes = _lrs.ClearState(Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the state of the clear.
        /// </summary>
        [Test]
        public async Task TestClearStateAsync()
        {
            var lrsRes = await _lrs.ClearStateAsync(Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the delete activity profile.
        /// </summary>
        [Test]
        public void TestDeleteActivityProfile()
        {
            var doc = new ActivityProfileDocument
            {
                Activity = Support.Activity,
                ID = "test"
            };

            var lrsRes = _lrs.DeleteActivityProfile(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the delete activity profile.
        /// </summary>
        [Test]
        public async Task TestDeleteActivityProfileAsync()
        {
            var doc = new ActivityProfileDocument
            {
                Activity = Support.Activity,
                ID = "test"
            };

            var lrsRes = await _lrs.DeleteActivityProfileAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the delete agent profile.
        /// </summary>
        [Test]
        public void TestDeleteAgentProfile()
        {
            var doc = new AgentProfileDocument
            {
                Agent = Support.Agent,
                ID = "test"
            };

            var lrsRes = _lrs.DeleteAgentProfile(doc);
            Assert.IsTrue(lrsRes.Success);
        }
        
        /// <summary>
        /// Tests the delete agent profile.
        /// </summary>
        [Test]
        public async Task TestDeleteAgentProfileAsync()
        {
            var doc = new AgentProfileDocument
            {
                Agent = Support.Agent,
                ID = "test"
            };

            var lrsRes = await _lrs.DeleteAgentProfileAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the state of the delete.
        /// </summary>
        [Test]
        public void TestDeleteState()
        {
            var doc = new StateDocument
            {
                Activity = Support.Activity,
                Agent = Support.Agent,
                ID = "test"
            };

            var lrsRes = _lrs.DeleteState(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the state of the delete.
        /// </summary>
        [Test]
        public async Task TestDeleteStateAsync()
        {
            var doc = new StateDocument
            {
                Activity = Support.Activity,
                Agent = Support.Agent,
                ID = "test"
            };

            var lrsRes = await _lrs.DeleteStateAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the more statements.
        /// </summary>
        [Test]
        public void TestMoreStatements()
        {
            var query = new StatementsQuery
            {
                Format = StatementsQueryResultFormat.IDS,
                Limit = 2
            };

            var queryRes = _lrs.QueryStatements(query);
            if (queryRes.Success && queryRes.Content.More != null)
            {
                var moreRes = _lrs.MoreStatements(queryRes.Content);
                Assert.IsTrue(moreRes.Success);
                Console.WriteLine("TestMoreStatements - statement count: " + moreRes.Content.Statements.Count);
            }
        }

        /// <summary>
        /// Tests the more statements.
        /// </summary>
        [Test]
        public async Task TestMoreStatementsAsync()
        {
            var query = new StatementsQuery
            {
                Format = StatementsQueryResultFormat.IDS,
                Limit = 2
            };

            var queryRes = await _lrs.QueryStatementsAsync(query);
            if (queryRes.Success && queryRes.Content.More != null)
            {
                var moreRes = _lrs.MoreStatements(queryRes.Content);
                Assert.IsTrue(moreRes.Success);
                Console.WriteLine("TestMoreStatements - statement count: " + moreRes.Content.Statements.Count);
            }
        }

        /// <summary>
        /// Tests the query statements.
        /// </summary>
        [Test]
        public void TestQueryStatements()
        {
            var query = new StatementsQuery
            {
                Agent = Support.Agent,
                VerbId = Support.Verb.ID,
                ActivityId = Support.Parent.ID,
                RelatedActivities = true,
                RelatedAgents = true,
                Format = StatementsQueryResultFormat.IDS,
                Limit = 10
            };

            var lrsRes = _lrs.QueryStatements(query);
            Assert.IsTrue(lrsRes.Success);
            Console.WriteLine("TestQueryStatements - statement count: " + lrsRes.Content.Statements.Count);
        }

        /// <summary>
        /// Tests the query statements.
        /// </summary>
        [Test]
        public async Task TestQueryStatementsAsync()
        {
            var query = new StatementsQuery
            {
                Agent = Support.Agent,
                VerbId = Support.Verb.ID,
                ActivityId = Support.Parent.ID,
                RelatedActivities = true,
                RelatedAgents = true,
                Format = StatementsQueryResultFormat.IDS,
                Limit = 10
            };

            var lrsRes = await _lrs.QueryStatementsAsync(query);
            Assert.IsTrue(lrsRes.Success);
            Console.WriteLine("TestQueryStatements - statement count: " + lrsRes.Content.Statements.Count);
        }

        /// <summary>
        /// Tests the retrieve activity profile.
        /// </summary>
        [Test]
        public void TestRetrieveActivityProfile()
        {
            var lrsRes = _lrs.RetrieveActivityProfile("test", Support.Activity);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<ActivityProfileDocument>(lrsRes.Content);
        }
        
        /// <summary>
        /// Tests the retrieve activity profile.
        /// </summary>
        [Test]
        public async Task TestRetrieveActivityProfileAsync()
        {
            var lrsRes = await _lrs.RetrieveActivityProfileAsync("test", Support.Activity);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<ActivityProfileDocument>(lrsRes.Content);
        }

        /// <summary>
        /// Tests the retrieve activity profile ids.
        /// </summary>
        [Test]
        public void TestRetrieveActivityProfileIds()
        {
            var lrsRes = _lrs.RetrieveActivityProfileIds(Support.Activity);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the retrieve activity profile ids.
        /// </summary>
        [Test]
        public async Task TestRetrieveActivityProfileIdsAsync()
        {
            var lrsRes = await _lrs.RetrieveActivityProfileIdsAsync(Support.Activity);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the retrieve agent profile.
        /// </summary>
        [Test]
        public void TestRetrieveAgentProfile()
        {
            var lrsRes = _lrs.RetrieveAgentProfile("test", Support.Agent);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<AgentProfileDocument>(lrsRes.Content);
        }

        /// <summary>
        /// Tests the retrieve agent profile.
        /// </summary>
        [Test]
        public async Task TestRetrieveAgentProfileAsync()
        {
            var lrsRes = await _lrs.RetrieveAgentProfileAsync("test", Support.Agent);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<AgentProfileDocument>(lrsRes.Content);
        }

        /// <summary>
        /// Tests the retrieve agent profile ids.
        /// </summary>
        [Test]
        public void TestRetrieveAgentProfileIds()
        {
            var lrsRes = _lrs.RetrieveAgentProfileIds(Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the retrieve agent profile ids.
        /// </summary>
        [Test]
        public async Task TestRetrieveAgentProfileIdsAsync()
        {
            var lrsRes = await _lrs.RetrieveAgentProfileIdsAsync(Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the state of the retrieve.
        /// </summary>
        [Test]
        public void TestRetrieveState()
        {
            var lrsRes = _lrs.RetrieveState("test", Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<StateDocument>(lrsRes.Content);
        }

        /// <summary>
        /// Tests the state of the retrieve.
        /// </summary>
        [Test]
        public async Task TestRetrieveStateAsync()
        {
            var lrsRes = await _lrs.RetrieveStateAsync("test", Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<StateDocument>(lrsRes.Content);
        }

        /// <summary>
        /// Tests the retrieve state ids.
        /// </summary>
        [Test]
        public void TestRetrieveStateIds()
        {
            var lrsRes = _lrs.RetrieveStateIds(Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the retrieve state ids.
        /// </summary>
        [Test]
        public async Task TestRetrieveStateIdsAsync()
        {
            var lrsRes = await _lrs.RetrieveStateIdsAsync(Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the retrieve statement.
        /// </summary>
        [Test]
        public void TestRetrieveStatement()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.Activity;
            statement.Context = Support.Context;
            statement.Result = Support.Result;

            var saveRes = _lrs.SaveStatement(statement);
            if (!saveRes.Success) return;
            if (saveRes.Content.ID == null) return;
            var retRes = _lrs.RetrieveStatement(saveRes.Content.ID.Value);
            Assert.IsTrue(retRes.Success);
            Console.WriteLine("TestRetrieveStatement - statement: " + retRes.Content.ToJSON(true));
        }
        
        /// <summary>
        /// Tests the retrieve statement.
        /// </summary>
        [Test]
        public async Task TestRetrieveStatementAsync()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.Activity;
            statement.Context = Support.Context;
            statement.Result = Support.Result;

            var saveRes = _lrs.SaveStatement(statement);
            if (!saveRes.Success) return;
            if (saveRes.Content.ID == null) return;
            var retRes = await _lrs.RetrieveStatementAsync(saveRes.Content.ID.Value);
            Assert.IsTrue(retRes.Success);
            Console.WriteLine("TestRetrieveStatement - statement: " + retRes.Content.ToJSON(true));
        }

        /// <summary>
        /// Tests the save activity profile.
        /// </summary>
        [Test]
        public void TestSaveActivityProfile()
        {
            var doc = new ActivityProfileDocument
            {
                Activity = Support.Activity,
                ID = "test",
                Content = Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = _lrs.SaveActivityProfile(doc);
            Assert.IsTrue(lrsRes.Success);
        }
        
        /// <summary>
        /// Tests the save activity profile.
        /// </summary>
        [Test]
        public async Task TestSaveActivityProfileAsync()
        {
            var doc = new ActivityProfileDocument
            {
                Activity = Support.Activity,
                ID = "test",
                Content = Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = await _lrs.SaveActivityProfileAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the save agent profile.
        /// </summary>
        [Test]
        public void TestSaveAgentProfile()
        {
            var doc = new AgentProfileDocument
            {
                Agent = Support.Agent,
                ID = "test",
                Content = Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = _lrs.SaveAgentProfile(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the save agent profile.
        /// </summary>
        [Test]
        public async Task TestSaveAgentProfileAsync()
        {
            var doc = new AgentProfileDocument
            {
                Agent = Support.Agent,
                ID = "test",
                Content = Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = await _lrs.SaveAgentProfileAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the state of the save.
        /// </summary>
        [Test]
        public void TestSaveState()
        {
            var doc = new StateDocument
            {
                Activity = Support.Activity,
                Agent = Support.Agent,
                ID = "test",
                Content = Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = _lrs.SaveState(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the state of the save.
        /// </summary>
        [Test]
        public async Task TestSaveStateAsync()
        {
            var doc = new StateDocument
            {
                Activity = Support.Activity,
                Agent = Support.Agent,
                ID = "test",
                Content = Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = await _lrs.SaveStateAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the save statement.
        /// </summary>
        [Test]
        public void TestSaveStatement()
        {
            var statement = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Activity
            };

            var lrsRes = _lrs.SaveStatement(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
            Assert.IsNotNull(lrsRes.Content.ID);
        }

        /// <summary>
        /// Tests the save statement.
        /// </summary>
        [Test]
        public async Task TestSaveStatementAsync()
        {
            var statement = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Activity
            };

            var lrsRes = await _lrs.SaveStatementAsync(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
            Assert.IsNotNull(lrsRes.Content.ID);
        }

        /// <summary>
        /// Tests the save statements.
        /// </summary>
        [Test]
        public void TestSaveStatements()
        {
            var statement1 = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Parent
            };

            var statement2 = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Activity,
                Context = Support.Context
            };

            var statements = new List<Statement> {statement1, statement2};

            var lrsRes = _lrs.SaveStatements(statements);
            Assert.IsTrue(lrsRes.Success);
            // TODO: check statements match and ids not null
        }

        /// <summary>
        /// Tests the save statements.
        /// </summary>
        [Test]
        public async Task TestSaveStatementsAsync()
        {
            var statement1 = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Parent
            };

            var statement2 = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Activity,
                Context = Support.Context
            };

            var statements = new List<Statement> { statement1, statement2 };

            var lrsRes = await _lrs.SaveStatementsAsync(statements);
            Assert.IsTrue(lrsRes.Success);
            // TODO: check statements match and ids not null
        }

        /// <summary>
        /// Tests the save statement statement reference.
        /// </summary>
        [Test]
        public void TestSaveStatementStatementRef()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.StatementRef;

            var lrsRes = _lrs.SaveStatement(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        /// <summary>
        /// Tests the save statement statement reference.
        /// </summary>
        [Test]
        public async Task TestSaveStatementStatementRefAsync()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.StatementRef;

            var lrsRes = await _lrs.SaveStatementAsync(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        /// <summary>
        /// Tests the save statement sub statement.
        /// </summary>
        [Test]
        public void TestSaveStatementSubStatement()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.SubStatement;

            Console.WriteLine(statement.ToJSON(true));

            var lrsRes = _lrs.SaveStatement(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        /// <summary>
        /// Tests the save statement sub statement.
        /// </summary>
        [Test]
        public async Task TestSaveStatementSubStatementAsync()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.SubStatement;

            Console.WriteLine(statement.ToJSON(true));

            var lrsRes = await _lrs.SaveStatementAsync(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        /// <summary>
        /// Tests the save statement with identifier.
        /// </summary>
        [Test]
        public void TestSaveStatementWithID()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.Activity;

            var lrsRes = _lrs.SaveStatement(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        /// <summary>
        /// Tests the save statement with identifier.
        /// </summary>
        [Test]
        public async Task TestSaveStatementWithIDAsync()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.Activity;

            var lrsRes = await _lrs.SaveStatementAsync(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        /// <summary>
        /// Tests the void statement.
        /// </summary>
        [Test]
        public void TestVoidStatement()
        {
            var toVoid = Guid.NewGuid();
            var lrsRes = _lrs.VoidStatement(toVoid, Support.Agent);

            Assert.IsTrue(lrsRes.Success, "LRS response successful");
            Assert.AreEqual(new Uri("http://adlnet.gov/expapi/verbs/voided"), lrsRes.Content.Verb.ID,
                "voiding statement uses voided verb");
            Assert.AreEqual(toVoid, ((StatementRef) lrsRes.Content.Target).ID, "voiding statement target correct id");
        }

        /// <summary>
        /// Tests the void statement.
        /// </summary>
        [Test]
        public async Task TestVoidStatementAsync()
        {
            var toVoid = Guid.NewGuid();
            var lrsRes = await _lrs.VoidStatementAsync(toVoid, Support.Agent);

            Assert.IsTrue(lrsRes.Success, "LRS response successful");
            Assert.AreEqual(new Uri("http://adlnet.gov/expapi/verbs/voided"), lrsRes.Content.Verb.ID,
                "voiding statement uses voided verb");
            Assert.AreEqual(toVoid, ((StatementRef)lrsRes.Content.Target).ID, "voiding statement target correct id");
        }
    }
}