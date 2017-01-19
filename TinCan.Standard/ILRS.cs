#region License and Warranty Information

// ==========================================================
//  <copyright file="ILRS.cs" company="iWork Technologies">
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
using System.Threading.Tasks;
using TinCan.Standard.Documents;
using TinCan.Standard.LRSResponses;

#endregion

namespace TinCan.Standard
{
    /// <summary>
    ///     Interface ILRS
    /// </summary>
    public interface ILRS
    {
        /// <summary>
        ///     Abouts this instance.
        /// </summary>
        /// <returns>AboutLRSResponse.</returns>
        AboutLRSResponse About();

        /// <summary>
        ///     Abouts this instance.
        /// </summary>
        /// <returns>AboutLRSResponse.</returns>
        Task<AboutLRSResponse> AboutAsync();

        /// <summary>
        ///     Saves the statement.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns>StatementLRSResponse.</returns>
        StatementLRSResponse SaveStatement(Statement statement);

        /// <summary>
        ///     Saves the statement.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns>StatementLRSResponse.</returns>
        Task<StatementLRSResponse> SaveStatementAsync(Statement statement);

        /// <summary>
        ///     Voids the statement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="agent">The agent.</param>
        /// <returns>StatementLRSResponse.</returns>
        StatementLRSResponse VoidStatement(Guid id, Agent agent);

        /// <summary>
        ///     Voids the statement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="agent">The agent.</param>
        /// <returns>StatementLRSResponse.</returns>
        Task<StatementLRSResponse> VoidStatementAsync(Guid id, Agent agent);

        /// <summary>
        ///     Saves the statements.
        /// </summary>
        /// <param name="statements">The statements.</param>
        /// <returns>StatementsResultLRSResponse.</returns>
        StatementsResultLRSResponse SaveStatements(List<Statement> statements);

        /// <summary>
        ///     Saves the statements.
        /// </summary>
        /// <param name="statements">The statements.</param>
        /// <returns>StatementsResultLRSResponse.</returns>
        Task<StatementsResultLRSResponse> SaveStatementsAsync(List<Statement> statements);

        /// <summary>
        ///     Retrieves the statement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>StatementLRSResponse.</returns>
        StatementLRSResponse RetrieveStatement(Guid id);

        /// <summary>
        ///     Retrieves the statement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>StatementLRSResponse.</returns>
        Task<StatementLRSResponse> RetrieveStatementAsync(Guid id);

        /// <summary>
        ///     Retrieves the voided statement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>StatementLRSResponse.</returns>
        StatementLRSResponse RetrieveVoidedStatement(Guid id);

        /// <summary>
        ///     Retrieves the voided statement asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;StatementLRSResponse&gt;.</returns>
        Task<StatementLRSResponse> RetrieveVoidedStatementAsync(Guid id);

        /// <summary>
        ///     Queries the statements.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>StatementsResultLRSResponse.</returns>
        StatementsResultLRSResponse QueryStatements(StatementsQuery query);

        /// <summary>
        ///     Queries the statements asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>Task&lt;StatementsResultLRSResponse&gt;.</returns>
        Task<StatementsResultLRSResponse> QueryStatementsAsync(StatementsQuery query);

        /// <summary>
        ///     Mores the statements.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>StatementsResultLRSResponse.</returns>
        StatementsResultLRSResponse MoreStatements(StatementsResult result);

        /// <summary>
        ///     Mores the statements asynchronous.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>Task&lt;StatementsResultLRSResponse&gt;.</returns>
        Task<StatementsResultLRSResponse> MoreStatementsAsync(StatementsResult result);

        /// <summary>
        ///     Retrieves the state ids.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="registration">The registration.</param>
        /// <returns>ProfileKeysLRSResponse.</returns>
        ProfileKeysLRSResponse RetrieveStateIds(Activity activity, Agent agent, Guid? registration = null);

        /// <summary>
        ///     Retrieves the state ids asynchronous.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="registration">The registration.</param>
        /// <returns>Task&lt;ProfileKeysLRSResponse&gt;.</returns>
        Task<ProfileKeysLRSResponse> RetrieveStateIdsAsync(Activity activity, Agent agent, Guid? registration = null);

        /// <summary>
        ///     Retrieves the state.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="registration">The registration.</param>
        /// <returns>StateLRSResponse.</returns>
        StateLRSResponse RetrieveState(string id, Activity activity, Agent agent, Guid? registration = null);

        /// <summary>
        ///     Retrieves the state asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="registration">The registration.</param>
        /// <returns>Task&lt;StateLRSResponse&gt;.</returns>
        Task<StateLRSResponse> RetrieveStateAsync(string id, Activity activity, Agent agent, Guid? registration = null);

        /// <summary>
        ///     Saves the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>LRSResponse.</returns>
        LRSResponse SaveState(StateDocument state);

        /// <summary>
        ///     Saves the state asynchronous.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<LRSResponse> SaveStateAsync(StateDocument state);

        /// <summary>
        ///     Deletes the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>LRSResponse.</returns>
        LRSResponse DeleteState(StateDocument state);

        /// <summary>
        ///     Deletes the state asynchronous.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<LRSResponse> DeleteStateAsync(StateDocument state);

        /// <summary>
        ///     Clears the state.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="registration">The registration.</param>
        /// <returns>LRSResponse.</returns>
        LRSResponse ClearState(Activity activity, Agent agent, Guid? registration = null);

        /// <summary>
        ///     Clears the state asynchronous.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="registration">The registration.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<LRSResponse> ClearStateAsync(Activity activity, Agent agent, Guid? registration = null);

        /// <summary>
        ///     Retrieves the activity profile ids.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns>ProfileKeysLRSResponse.</returns>
        ProfileKeysLRSResponse RetrieveActivityProfileIds(Activity activity);

        /// <summary>
        ///     Retrieves the activity profile ids asynchronous.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns>Task&lt;ProfileKeysLRSResponse&gt;.</returns>
        Task<ProfileKeysLRSResponse> RetrieveActivityProfileIdsAsync(Activity activity);

        /// <summary>
        ///     Retrieves the activity profile.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="activity">The activity.</param>
        /// <returns>ActivityProfileLRSResponse.</returns>
        ActivityProfileLRSResponse RetrieveActivityProfile(string id, Activity activity);

        /// <summary>
        ///     Retrieves the activity profile asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="activity">The activity.</param>
        /// <returns>Task&lt;ActivityProfileLRSResponse&gt;.</returns>
        Task<ActivityProfileLRSResponse> RetrieveActivityProfileAsync(string id, Activity activity);

        /// <summary>
        ///     Saves the activity profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>LRSResponse.</returns>
        LRSResponse SaveActivityProfile(ActivityProfileDocument profile);

        /// <summary>
        ///     Saves the activity profile asynchronous.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<LRSResponse> SaveActivityProfileAsync(ActivityProfileDocument profile);

        /// <summary>
        ///     Deletes the activity profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>LRSResponse.</returns>
        LRSResponse DeleteActivityProfile(ActivityProfileDocument profile);

        /// <summary>
        ///     Deletes the activity profile asynchronous.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<LRSResponse> DeleteActivityProfileAsync(ActivityProfileDocument profile);

        /// <summary>
        ///     Retrieves the agent profile ids.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <returns>ProfileKeysLRSResponse.</returns>
        ProfileKeysLRSResponse RetrieveAgentProfileIds(Agent agent);

        /// <summary>
        ///     Retrieves the agent profile ids asynchronous.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <returns>Task&lt;ProfileKeysLRSResponse&gt;.</returns>
        Task<ProfileKeysLRSResponse> RetrieveAgentProfileIdsAsync(Agent agent);

        /// <summary>
        ///     Retrieves the agent profile.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="agent">The agent.</param>
        /// <returns>AgentProfileLRSResponse.</returns>
        AgentProfileLRSResponse RetrieveAgentProfile(string id, Agent agent);

        /// <summary>
        ///     Retrieves the agent profile asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="agent">The agent.</param>
        /// <returns>Task&lt;AgentProfileLRSResponse&gt;.</returns>
        Task<AgentProfileLRSResponse> RetrieveAgentProfileAsync(string id, Agent agent);

        /// <summary>
        ///     Saves the agent profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>LRSResponse.</returns>
        LRSResponse SaveAgentProfile(AgentProfileDocument profile);

        /// <summary>
        ///     Saves the agent profile asynchronous.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<LRSResponse> SaveAgentProfileAsync(AgentProfileDocument profile);

        /// <summary>
        ///     Deletes the agent profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>LRSResponse.</returns>
        LRSResponse DeleteAgentProfile(AgentProfileDocument profile);

        /// <summary>
        ///     Deletes the agent profile asynchronous.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>Task&lt;LRSResponse&gt;.</returns>
        Task<LRSResponse> DeleteAgentProfileAsync(AgentProfileDocument profile);
    }
}