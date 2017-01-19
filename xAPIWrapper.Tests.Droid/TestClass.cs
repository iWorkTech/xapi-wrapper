using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App.Admin;
using NUnit.Framework;
using TinCan.Standard;
using TinCan.Standard.Documents;
using TinCan.Standard.LRSResponses;
using TinCan.xAPIWrapper;

namespace xAPIWrapper.Tests.Droid
{
    /// <summary>
    ///     Class TestClass.
    /// </summary>
    [TestFixture]
    public class TestClass
    {
        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        [SetUp]
        public void Init()
        {
            _xAPIWrapper = new APIWrapper(string.Empty, string.Empty, string.Empty);
            _guid = Guid.NewGuid();
        }

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
        [TearDown]
        public void Dispose()
        {
            _xAPIWrapper = null;
        }

        /// <summary>
        ///     The LRS
        /// </summary>
        private APIWrapper _xAPIWrapper;

        private Guid _guid;

        [Test]
        public async Task TestGetAbout()
        {
            var task = await _xAPIWrapper.About();
            Assert.IsTrue(task.Success);
            Assert.IsInstanceOf<About>(task.Content);
        }

        /// <summary>
        ///     Tests the get statements since.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task TestGetActivity()
        {
            var task = await _xAPIWrapper.GetActivity("test", Support.Activity);
            Assert.IsTrue(task.Success);
            Assert.IsInstanceOf<Activity>(task.Content.Activity);
        }

        /// <summary>
        ///     Tests the get statements.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task TestGetStatements()
        {
            var queryParams = new StatementsQuery
            {
                Agent = Support.Agent,
                VerbId = Support.Verb.ID,
                ActivityId = Support.Parent.ID,
                RelatedActivities = true,
                RelatedAgents = true,
                Format = StatementsQueryResultFormat.IDS,
                Limit = 10
            };
            var task = await _xAPIWrapper.GetStatements(queryParams);
            Assert.IsTrue(task.Success);
        }

        /// <summary>
        ///     Tests the get statements since.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task TestGetStatementsSince()
        {
            var queryParams = new StatementsQuery
            {
                Since = new DateTime(2017, 1, 12),
                Limit = 10
            };
            var task = await _xAPIWrapper.GetStatements(queryParams);

            Assert.IsTrue(task.Success);
        }

        /// <summary>
        ///     Tests the about.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task TestSendStatement()
        {
            var statement = _xAPIWrapper.PrepareStatement("tincancsharp@tincanapi.com", "experienced", "Activity");
            var task = await _xAPIWrapper.SendStatement(statement);
            Assert.IsTrue(task.Success);
        }

        /// <summary>
        ///     Tests the about.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task TestSendStatement1()
        {
            var statement = _xAPIWrapper.PrepareStatement(Support.Agent, Support.Verb, Support.Activity);
            var task = await _xAPIWrapper.SendStatement(statement);
            Assert.IsTrue(task.Success);
        }

        /// <summary>
        ///     Tests the send statements.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task TestSendStatements()
        {
            var statement = _xAPIWrapper.PrepareStatement(Support.Agent, Support.Verb, Support.Activity);
            var statement1 = _xAPIWrapper.PrepareStatement(Support.Agent, Support.Verb, Support.Activity);
            var statement2 = _xAPIWrapper.PrepareStatement(Support.Agent, Support.Verb, Support.Activity);
            var statements = new List<Statement> {statement, statement1, statement2};
            var task = await _xAPIWrapper.SendStatements(statements);
            Assert.IsTrue(task.Success);
        }

        [Test]
        public async Task TestSendState()
        {
            var task = await _xAPIWrapper.SendState("test", Support.Agent, "testState", _guid, string.Empty,
                string.Empty, string.Empty);
            Assert.IsTrue(task.Success);
        }

        /// <summary>
        /// Tests the state of the get.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task TestGetState()
        {
            var task = await _xAPIWrapper.GetState("test", Support.Agent, "testState", _guid, string.Empty,
                string.Empty, string.Empty);
            Assert.IsTrue(task.Success);
            Assert.IsInstanceOf<Activity>(task.Content.Activity);
            if (task.Content.Registration != null) Assert.AreEqual(task.Content.Registration.Value, _guid);
        }
    }
}