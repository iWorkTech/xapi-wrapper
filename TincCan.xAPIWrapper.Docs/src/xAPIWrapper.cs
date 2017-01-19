using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinCan.Standard;
using TinCan.Standard.Documents;
using TinCan.Standard.LRSResponses;

namespace TinCan.xAPIWrapper
{
    /// <summary>
    /// Class xAPIWrapper.
    /// </summary>
    /// <seealso cref="TinCan.xAPIWrapper.IXAPIWrapper" />
    /// <seealso cref="IXAPIWrapper" />
    /// <seealso cref="IXAPIWrapper" />
    /// <seealso cref="System.IDisposable" />
    public class APIWrapper : IXAPIWrapper, IDisposable
    {
        /// <summary>
        /// The LRS
        /// </summary>
        private RemoteLRS _lrs;

        /// <summary>
        /// The username
        /// </summary>
        private string _username;
        /// <summary>
        /// The password
        /// </summary>
        private string _password;
        /// <summary>
        /// The endpoint
        /// </summary>
        private string _endpoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="APIWrapper" /> class.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public APIWrapper(string endpoint, string username, string password)
        {
            Init(endpoint, username, password);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            _lrs = null;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void Init(string endpoint, string username, string password)
        {
            _endpoint = string.IsNullOrWhiteSpace(username) ? "https://cloud.scorm.com/tc/U2S4SI5FY0/sandbox/" : endpoint;
            _username = string.IsNullOrWhiteSpace(username) ? "Nja986GYE1_XrWMmFUE" : username;
            _password = string.IsNullOrWhiteSpace(password) ? "Bd9lDr1kjaWWY6RID_4" : password;
           
            _lrs = new RemoteLRS(_endpoint, _username, _password);
        }

        /// <summary>
        /// Abouts this instance.
        /// </summary>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        public async Task<AboutLRSResponse> About()
        {
            var lrsRes = await _lrs.AboutAsync();
            return lrsRes;
        }

        /// <summary>
        /// Changes the configuration.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void ChangeConfig(string endpoint, string username, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prepares the statement.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <param name="verb">The verb.</param>
        /// <param name="target">The target.</param>
        /// <returns>Statement.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Statement PrepareStatement(Agent agent, Verb verb, IStatementTarget target)
        {
            var statement = new Statement
            {
                Actor = agent,
                Target = target,
                Verb = verb
            };

            return statement;
        }

        /// <summary>
        /// Prepares the statement.
        /// </summary>
        /// <param name="agentEmail">The agent.</param>
        /// <param name="verb">The verb.</param>
        /// <param name="activityType">The target.</param>
        /// <returns>Statement.</returns>
        public Statement PrepareStatement(string agentEmail, string verb, string activityType)
        {
            var agent = new Agent {Mbox = "mailto:" + agentEmail};

            var lverb = new Verb
            {
                ID = new Uri(string.Format("http://adlnet.gov/expapi/verbs/{0}", verb)),
                Display = new LanguageMap()
            };

            lverb.Display.Add("en-US", "experienced");

            var target = new Activity { ID = string.Format("http://adlnet.gov/expapi/activities/{0}", activityType) };

            var statement = new Statement
            {
                Actor = agent,
                Target = target,
                Verb = lverb
            };

            return statement;
        }

        /// <summary>
        /// Sends the statement.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns>LRSResponse.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<StatementLRSResponse> SendStatement(Statement statement)
        {
            return await _lrs.SaveStatementAsync(statement);          
        }

        /// <summary>
        /// Sends the statement.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <param name="verb">The verb.</param>
        /// <param name="target">The target.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        public async Task<LRSResponse> SendStatement(Agent agent, Verb verb, IStatementTarget target)
        {
            var authority = new Agent
            {
                Mbox = "mailto:admin@adl.net",
                Account = new AgentAccount { Name = "ADL Administrator" },
                Name = "Admin"
            };

            var statement = new Statement
            {
                Version = TCAPIVersion.Latest(),
                Actor = agent,
                Target = target,
                Authority = authority,
                Verb = verb
            };

            return await _lrs.SaveStatementAsync(statement);
        }

        /// <summary>
        /// Sends the statements.
        /// </summary>
        /// <param name="statements">The statements.</param>
        /// <returns>LRSResponse.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<StatementsResultLRSResponse> SendStatements(List<Statement> statements)
        {
           return await _lrs.SaveStatementsAsync(statements);
        }

        /// <summary>
        /// Gets the statements.
        /// </summary>
        /// <param name="searchParams">The search parameters.</param>
        /// <returns>List&lt;Statement&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<StatementsResultLRSResponse> GetStatements(StatementsQuery searchParams)
        {
            return await _lrs.QueryStatementsAsync(searchParams);
        }

        /// <summary>
        /// Gets the statements.
        /// </summary>
        /// <param name="since">Since a particular date</param>
        /// <param name="limit">Limit or size of the resultset</param>
        /// <returns>List&lt;Statement&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<StatementsResultLRSResponse> GetStatements(DateTime since, int limit)
        {
            var queryParams = new StatementsQuery
            {
                Since = since,
                Limit = limit
            };
            return await _lrs.QueryStatementsAsync(queryParams);
        }

        /// <summary>
        /// Gets the activities.
        /// </summary>
        /// <param name="activityId">The activity identifier.</param>
        /// <returns>List&lt;Activity&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<ActivityProfileLRSResponse> GetActivity(string activityId, Activity activity)
        {
            return await _lrs.RetrieveActivityProfileAsync(activityId, activity);
        }

        /// <summary>
        /// Sends the state.
        /// </summary>
        /// <param name="activityId">The activity identifier.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="registration">The registration.</param>
        /// <param name="stateVal">The state value.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="noneMatchHash">The none match hash.</param>
        /// <returns>LRSResponse.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<LRSResponse> SendState(string activityId, Agent agent, string stateId, Guid? registration, string stateVal, string matchHash,
            string noneMatchHash)
        {
            var activity = new Activity {ID = activityId};

            var doc = new StateDocument
            {   Activity = activity,
                Agent = agent,
                ID = stateId,
                Content = Encoding.UTF8.GetBytes(stateVal),
                Registration = registration
            };

            return await _lrs.SaveStateAsync(doc);
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <param name="activityId">The activity identifier.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="registration">The registration.</param>
        /// <param name="stateVal">The state value.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="noneMatchHash">The none match hash.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<StateLRSResponse> GetState(string activityId, Agent agent, string stateId, Guid? registration, string stateVal, string matchHash,
            string noneMatchHash)
        {
            var activity = new Activity {ID = activityId};
            return await _lrs.RetrieveStateAsync(stateId, activity, agent, registration);
        }

        /// <summary>
        /// Deletes the state.
        /// </summary>
        /// <param name="activityId">The activity identifier.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="registration">The registration.</param>
        /// <param name="stateVal">The state value.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="noneMatchHash">The none match hash.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<LRSResponse> DeleteState(string activityId, Agent agent, string stateId, Guid? registration, string stateVal, string matchHash,
            string noneMatchHash)
        {
            var activity = new Activity { ID = activityId };

            var doc = new StateDocument
            {
                Activity = activity,
                Agent = agent,
                ID = stateId,
                Content = Encoding.UTF8.GetBytes(stateVal),
                Registration = registration
            };

            return await _lrs.DeleteStateAsync(doc);
        }

        /// <summary>
        /// Sends the activity profile.
        /// </summary>
        /// <param name="activityId">The activity identifier.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="profilEval">The profil eval.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="noneMatchHash">The none match hash.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        public async Task<LRSResponse> SendActivityProfile(string activityId, string profileId, string profilEval, string matchHash, string noneMatchHash)
        {
            var activity = new Activity { ID = activityId };

            var doc = new ActivityProfileDocument
            {
                Activity = activity,
                ID = profileId,
                Content = Encoding.UTF8.GetBytes(profilEval)
            };

            return await _lrs.SaveActivityProfileAsync(doc);
        }

        /// <summary>
        /// Gets the activity profile.
        /// </summary>
        /// <param name="activityId">The activity identifier.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="since">The since.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<ActivityProfileLRSResponse> GetActivityProfile(string activityId, string profileId, DateTime? since)
        {
            var activity = new Activity {ID = activityId};
            return await _lrs.RetrieveActivityProfileAsync(profileId, activity);
        }

        /// <summary>
        /// Deletes the activity profile.
        /// </summary>
        /// <param name="activityId">The activity identifier.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="noneMatchHash">The none match hash.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<LRSResponse> DeleteActivityProfile(string activityId, string profileId, string matchHash, string noneMatchHash)
        {
            var activity = new Activity { ID = activityId };

            var doc = new ActivityProfileDocument
            {
                Activity = activity,
                ID = profileId,
            };

            return await _lrs.DeleteActivityProfileAsync(doc);

        }

        /// <summary>
        /// Gets the agents.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<AgentProfileLRSResponse> GetAgent(Agent agent)
        {
           return await _lrs.RetrieveAgentProfileAsync(agent.Mbox, agent);
        }

        /// <summary>
        /// Sends the agent profile.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="profilEval">The profil eval.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="noneMatchHash">The none match hash.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<LRSResponse> SendAgentProfile(Agent agent, string profileId, string profilEval, string matchHash, string noneMatchHash)
        {
            var doc = new AgentProfileDocument
            {
                Agent = agent,
                ID = profileId,
                Content = Encoding.UTF8.GetBytes(profilEval)
            };

            return await _lrs.SaveAgentProfileAsync(doc);
        }

        /// <summary>
        /// Gets the agent profile.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="since">The since.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<LRSResponse> GetAgentProfile(string agentId, string profileId, DateTime? since)
        {
            var agent = new Agent {Mbox = agentId};
            return await _lrs.RetrieveAgentProfileAsync(profileId, agent);
        }

        /// <summary>
        /// Deletes the agent profile.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="noneMatchHash">The none match hash.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<LRSResponse> DeleteAgentProfile(Agent agent, string profileId, string matchHash, string noneMatchHash)
        {
            var doc = new AgentProfileDocument
            {
                Agent = agent,
                ID = profileId,
            };

            return await _lrs.DeleteAgentProfileAsync(doc);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Dispose()
        {
            _lrs = null;
        }

    }
}