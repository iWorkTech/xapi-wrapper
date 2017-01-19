using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinCan.Standard;
using TinCan.Standard.Documents;
using TinCan.Standard.LRSResponses;

namespace TinCan.xAPIWrapper
{
    /// <summary>
    ///     Interface IxAPIWrapper
    /// </summary>
    internal interface IXAPIWrapper
    {
        /// <summary>
        ///     Abouts this instance.
        /// </summary>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<AboutLRSResponse> About();

        /// <summary>
        ///     Changes the configuration.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        void ChangeConfig(string endpoint, string username, string password);

        /// <summary>
        ///     Prepares the statement.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <param name="verb">The verb.</param>
        /// <param name="target">The object.</param>
        /// <returns>Statement.</returns>
        Statement PrepareStatement(Agent agent, Verb verb, IStatementTarget target);

        /// <summary>
        ///     Prepares the statement.
        /// </summary>
        /// <param name="agentEmail"></param>
        /// <param name="verb">The verb.</param>
        /// <param name="activityType"></param>
        /// <returns>Statement.</returns>
        Statement PrepareStatement(string agentEmail, string verb, string activityType);

        /// <summary>
        ///     Sends the statement.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns>LRSResponse.</returns>
        Task<StatementLRSResponse> SendStatement(Statement statement);

        /// <summary>
        ///     Sends the statements.
        /// </summary>
        /// <param name="statements">The statements.</param>
        /// <returns>LRSResponse.</returns>
        Task<StatementsResultLRSResponse> SendStatements(List<Statement> statements);

        /// <summary>
        ///     Gets the statements.
        /// </summary>
        /// <param name="searchParams">The search parameters.</param>
        /// <returns>List&lt;Statement&gt;.</returns>
        Task<StatementsResultLRSResponse> GetStatements(StatementsQuery searchParams);

        /// <summary>
        ///     Gets the statements.
        /// </summary>
        /// <param name="since"></param>
        /// <param name="limit"></param>
        /// <returns>List&lt;Statement&gt;.</returns>
        Task<StatementsResultLRSResponse> GetStatements(DateTime since, int limit);

        /// <summary>
        /// Gets the activities.
        /// </summary>
        /// <param name="activityId">The activity identifier.</param>
        /// <param name="activity">The activity.</param>
        /// <returns>List&lt;Activity&gt;.</returns>
        Task<ActivityProfileLRSResponse> GetActivity(string activityId, Activity activity);

        /// <summary>
        ///     Sends the state.
        /// </summary>
        /// <param name="activityId">The activity identifier.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="registration">The registration.</param>
        /// <param name="stateVal">The state value.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="noneMatchHash">The none match hash.</param>
        /// <returns>LRSResponse.</returns>
        Task<LRSResponse> SendState(string activityId, Agent agent, string stateId, Guid? registration, string stateVal,
            string matchHash, string noneMatchHash);

        /// <summary>
        ///     Gets the state.
        /// </summary>
        /// <param name="activityId">The activity identifier.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="registration">The registration.</param>
        /// <param name="stateVal">The state value.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="noneMatchHash">The none match hash.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<StateLRSResponse> GetState(string activityId, Agent agent, string stateId, Guid? registration, string stateVal,
            string matchHash, string noneMatchHash);

        /// <summary>
        ///     Deletes the state.
        /// </summary>
        /// <param name="activityId">The activity identifier.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="registration">The registration.</param>
        /// <param name="stateVal">The state value.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="noneMatchHash">The none match hash.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<LRSResponse> DeleteState(string activityId, Agent agent, string stateId, Guid? registration,
            string stateVal,
            string matchHash, string noneMatchHash);

        /// <summary>
        ///     Sends the activity profile.
        /// </summary>
        /// <param name="activityId">The activity identifier.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="profilEval">The profil eval.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="noneMatchHash">The none match hash.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<LRSResponse> SendActivityProfile(string activityId, string profileId, string profilEval, string matchHash,
            string noneMatchHash);

        /// <summary>
        ///     Gets the activity profile.
        /// </summary>
        /// <param name="activityId">The activity identifier.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="since">The since.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<ActivityProfileLRSResponse> GetActivityProfile(string activityId, string profileId, DateTime? since);

        /// <summary>
        ///     Deletes the activity profile.
        /// </summary>
        /// <param name="activityId">The activity identifier.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="noneMatchHash">The none match hash.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<LRSResponse> DeleteActivityProfile(string activityId, string profileId, string matchHash,
            string noneMatchHash);

        /// <summary>
        ///     Gets the agents.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<AgentProfileLRSResponse> GetAgent(Agent agent);

        /// <summary>
        ///     Sends the agent profile.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="profilEval">The profil eval.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="noneMatchHash">The none match hash.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<LRSResponse> SendAgentProfile(Agent agent, string profileId, string profilEval, string matchHash,
            string noneMatchHash);

        /// <summary>
        ///     Gets the agent profile.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="since">The since.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<LRSResponse> GetAgentProfile(string agentId, string profileId, DateTime? since);

        /// <summary>
        ///     Deletes the agent profile.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <param name="matchHash">The match hash.</param>
        /// <param name="noneMatchHash">The none match hash.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<LRSResponse> DeleteAgentProfile(Agent agent, string profileId, string matchHash, string noneMatchHash);
    }
}