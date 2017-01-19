#region License and Warranty Information

// ==========================================================
//  <copyright file="RemoteLRS.cs" company="iWork Technologies">
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
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TinCan.Documents;
using TinCan.Json;
using TinCan.LRSResponses;

#endregion

namespace TinCan
{
    /// <summary>
    ///     Class RemoteLRS.
    /// </summary>
    /// <seealso cref="TinCan.ILRS" />
    public class RemoteLRS : ILRS
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RemoteLRS" /> class.
        /// </summary>
        public RemoteLRS()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RemoteLRS" /> class.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="version">The version.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public RemoteLRS(Uri endpoint, TCAPIVersion version, string username, string password)
        {
            Endpoint = endpoint;
            Version = version;
            SetAuth(username, password);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RemoteLRS" /> class.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="version">The version.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public RemoteLRS(string endpoint, TCAPIVersion version, string username, string password)
            : this(new Uri(endpoint), version, username, password)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RemoteLRS" /> class.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public RemoteLRS(string endpoint, string username, string password)
            : this(endpoint, TCAPIVersion.Latest(), username, password)
        {
        }

        /// <summary>
        ///     Gets or sets the endpoint.
        /// </summary>
        /// <value>The endpoint.</value>
        public Uri Endpoint { get; set; }

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public TCAPIVersion Version { get; set; }

        /// <summary>
        ///     Gets or sets the authentication.
        /// </summary>
        /// <value>The authentication.</value>
        public string Auth { get; set; }

        /// <summary>
        ///     Gets or sets the extended.
        /// </summary>
        /// <value>The extended.</value>
        public Dictionary<string, string> Extended { get; set; }

        /// <summary>
        ///     Abouts this instance.
        /// </summary>
        /// <returns>AboutLRSResponse.</returns>
        public AboutLRSResponse About()
        {
            var r = new AboutLRSResponse();

            var req = new MyHttpRequest
            {
                Method = "GET",
                Resource = "about"
            };

            var res = MakeSyncRequest(req);
            if (res.Status != HttpStatusCode.OK)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            r.Success = true;
            r.Content = new About(Encoding.UTF8.GetString(res.Content));

            return r;
        }

        /// <summary>
        ///     Abouts this instance.
        /// </summary>
        /// <returns>AboutLRSResponse.</returns>
        public async Task<AboutLRSResponse> AboutAsync()
        {
            var r = new AboutLRSResponse();

            var req = new MyHttpRequest
            {
                Method = "GET",
                Resource = "about"
            };

            var res = await MakeAsyncRequest(req);
            if (res.Status != HttpStatusCode.OK)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            r.Success = true;
            r.Content = new About(Encoding.UTF8.GetString(res.Content));

            return r;
        }

        /// <summary>
        ///     Saves the statement.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns>StatementLRSResponse.</returns>
        public StatementLRSResponse SaveStatement(Statement statement)
        {
            var r = new StatementLRSResponse();
            var req = new MyHttpRequest
            {
                QueryParams = new Dictionary<string, string>(),
                Resource = "statements"
            };

            if (statement.ID == null)
            {
                req.Method = "POST";
            }
            else
            {
                req.Method = "PUT";
                req.QueryParams.Add("statementId", statement.ID.ToString());
            }

            req.ContentType = "application/json";
            req.Content = Encoding.UTF8.GetBytes(statement.ToJSON(Version));

            var res = MakeSyncRequest(req);
            if (statement.ID == null)
            {
                if (res.Status != HttpStatusCode.OK)
                {
                    r.Success = false;
                    r.HttpException = res.Ex;
                    r.SetErrMsgFromBytes(res.Content);
                    return r;
                }

                var ids = JArray.Parse(Encoding.UTF8.GetString(res.Content));
                statement.ID = new Guid((string) ids[0]);
            }
            else
            {
                if (res.Status != HttpStatusCode.NoContent)
                {
                    r.Success = false;
                    r.HttpException = res.Ex;
                    r.SetErrMsgFromBytes(res.Content);
                    return r;
                }
            }

            r.Success = true;
            r.Content = statement;

            return r;
        }

        /// <summary>
        ///     Saves the statement.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns>StatementLRSResponse.</returns>
        public async Task<StatementLRSResponse> SaveStatementAsync(Statement statement)
        {
            var r = new StatementLRSResponse();
            var req = new MyHttpRequest
            {
                QueryParams = new Dictionary<string, string>(),
                Resource = "statements"
            };

            if (statement.ID == null)
            {
                req.Method = "POST";
            }
            else
            {
                req.Method = "PUT";
                req.QueryParams.Add("statementId", statement.ID.ToString());
            }

            req.ContentType = "application/json";
            req.Content = Encoding.UTF8.GetBytes(statement.ToJSON(Version));

            var res = await MakeAsyncRequest(req);
            if (statement.ID == null)
            {
                if (res.Status != HttpStatusCode.OK)
                {
                    r.Success = false;
                    r.HttpException = res.Ex;
                    r.SetErrMsgFromBytes(res.Content);
                    return r;
                }

                var ids = JArray.Parse(Encoding.UTF8.GetString(res.Content));
                statement.ID = new Guid((string) ids[0]);
            }
            else
            {
                if (res.Status != HttpStatusCode.NoContent)
                {
                    r.Success = false;
                    r.HttpException = res.Ex;
                    r.SetErrMsgFromBytes(res.Content);
                    return r;
                }
            }

            r.Success = true;
            r.Content = statement;

            return r;
        }

        /// <summary>
        ///     Voids the statement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="agent">The agent.</param>
        /// <returns>StatementLRSResponse.</returns>
        public StatementLRSResponse VoidStatement(Guid id, Agent agent)
        {
            var voidStatement = new Statement
            {
                Actor = agent,
                Verb = new Verb
                {
                    ID = new Uri("http://adlnet.gov/expapi/verbs/voided"),
                    Display = new LanguageMap()
                },
                Target = new StatementRef {ID = id}
            };
            voidStatement.Verb.Display.Add("en-US", "voided");

            return SaveStatement(voidStatement);
        }

        /// <summary>
        ///     Voids the statement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="agent">The agent.</param>
        /// <returns>StatementLRSResponse.</returns>
        public async Task<StatementLRSResponse> VoidStatementAsync(Guid id, Agent agent)
        {
            var voidStatement = new Statement
            {
                Actor = agent,
                Verb = new Verb
                {
                    ID = new Uri("http://adlnet.gov/expapi/verbs/voided"),
                    Display = new LanguageMap()
                },
                Target = new StatementRef {ID = id}
            };
            voidStatement.Verb.Display.Add("en-US", "voided");

            return await SaveStatementAsync(voidStatement);
        }

        /// <summary>
        ///     Saves the statements.
        /// </summary>
        /// <param name="statements">The statements.</param>
        /// <returns>StatementsResultLRSResponse.</returns>
        public StatementsResultLRSResponse SaveStatements(List<Statement> statements)
        {
            var r = new StatementsResultLRSResponse();

            var req = new MyHttpRequest
            {
                Resource = "statements",
                Method = "POST",
                ContentType = "application/json"
            };

            var jarray = new JArray();
            foreach (var st in statements)
                jarray.Add(st.ToJObject(Version));
            req.Content = Encoding.UTF8.GetBytes(jarray.ToString());

            var res = MakeSyncRequest(req);
            if (res.Status != HttpStatusCode.OK)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            var ids = JArray.Parse(Encoding.UTF8.GetString(res.Content));
            for (var i = 0; i < ids.Count; i++)
                statements[i].ID = new Guid((string) ids[i]);

            r.Success = true;
            r.Content = new StatementsResult(statements);

            return r;
        }

        /// <summary>
        ///     Saves the statements.
        /// </summary>
        /// <param name="statements">The statements.</param>
        /// <returns>StatementsResultLRSResponse.</returns>
        public async Task<StatementsResultLRSResponse> SaveStatementsAsync(List<Statement> statements)
        {
            var r = new StatementsResultLRSResponse();

            var req = new MyHttpRequest
            {
                Resource = "statements",
                Method = "POST",
                ContentType = "application/json"
            };

            var jarray = new JArray();
            foreach (var st in statements)
                jarray.Add(st.ToJObject(Version));
            req.Content = Encoding.UTF8.GetBytes(jarray.ToString());

            var res = await MakeAsyncRequest(req);
            if (res.Status != HttpStatusCode.OK)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            var ids = JArray.Parse(Encoding.UTF8.GetString(res.Content));
            for (var i = 0; i < ids.Count; i++)
                statements[i].ID = new Guid((string) ids[i]);

            r.Success = true;
            r.Content = new StatementsResult(statements);

            return r;
        }

        /// <summary>
        ///     Retrieves the statement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>StatementLRSResponse.</returns>
        public StatementLRSResponse RetrieveStatement(Guid id)
        {
            var queryParams = new Dictionary<string, string> {{"statementId", id.ToString()}};

            return GetStatement(queryParams);
        }

        /// <summary>
        ///     Retrieves the statement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>StatementLRSResponse.</returns>
        public async Task<StatementLRSResponse> RetrieveStatementAsync(Guid id)
        {
            var queryParams = new Dictionary<string, string> {{"statementId", id.ToString()}};

            return await GetStatementAsync(queryParams);
        }

        /// <summary>
        ///     Retrieves the voided statement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>StatementLRSResponse.</returns>
        public StatementLRSResponse RetrieveVoidedStatement(Guid id)
        {
            var queryParams = new Dictionary<string, string> {{"voidedStatementId", id.ToString()}};

            return GetStatement(queryParams);
        }

        /// <summary>
        ///     Retrieves the voided statement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>StatementLRSResponse.</returns>
        public async Task<StatementLRSResponse> RetrieveVoidedStatementAsync(Guid id)
        {
            var queryParams = new Dictionary<string, string> {{"voidedStatementId", id.ToString()}};

            return await GetStatementAsync(queryParams);
        }

        /// <summary>
        ///     Queries the statements.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>StatementsResultLRSResponse.</returns>
        public StatementsResultLRSResponse QueryStatements(StatementsQuery query)
        {
            var r = new StatementsResultLRSResponse();

            var req = new MyHttpRequest
            {
                Method = "GET",
                Resource = "statements",
                QueryParams = query.ToParameterMap(Version)
            };

            var res = MakeSyncRequest(req);
            if (res.Status != HttpStatusCode.OK)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            r.Success = true;
            r.Content = new StatementsResult(new StringOfJSON(Encoding.UTF8.GetString(res.Content)));

            return r;
        }

        /// <summary>
        ///     Queries the statements.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>StatementsResultLRSResponse.</returns>
        public async Task<StatementsResultLRSResponse> QueryStatementsAsync(StatementsQuery query)
        {
            var r = new StatementsResultLRSResponse();

            var req = new MyHttpRequest
            {
                Method = "GET",
                Resource = "statements",
                QueryParams = query.ToParameterMap(Version)
            };

            var res = await MakeAsyncRequest(req);
            if (res.Status != HttpStatusCode.OK)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            r.Success = true;
            r.Content = new StatementsResult(new StringOfJSON(Encoding.UTF8.GetString(res.Content)));

            return r;
        }

        /// <summary>
        ///     Mores the statements.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>StatementsResultLRSResponse.</returns>
        public StatementsResultLRSResponse MoreStatements(StatementsResult result)
        {
            var r = new StatementsResultLRSResponse();

            var req = new MyHttpRequest
            {
                Method = "GET",
                Resource = Endpoint.GetLeftPart(UriPartial.Authority)
            };
            if (!req.Resource.EndsWith("/"))
                req.Resource += "/";
            req.Resource += result.More;

            var res = MakeSyncRequest(req);
            if (res.Status != HttpStatusCode.OK)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            r.Success = true;
            r.Content = new StatementsResult(new StringOfJSON(Encoding.UTF8.GetString(res.Content)));

            return r;
        }

        /// <summary>
        ///     Mores the statements.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>StatementsResultLRSResponse.</returns>
        public async Task<StatementsResultLRSResponse> MoreStatementsAsync(StatementsResult result)
        {
            var r = new StatementsResultLRSResponse();

            var req = new MyHttpRequest
            {
                Method = "GET",
                Resource = Endpoint.GetLeftPart(UriPartial.Authority)
            };
            if (!req.Resource.EndsWith("/"))
                req.Resource += "/";
            req.Resource += result.More;

            var res = await MakeAsyncRequest(req);
            if (res.Status != HttpStatusCode.OK)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            r.Success = true;
            r.Content = new StatementsResult(new StringOfJSON(Encoding.UTF8.GetString(res.Content)));

            return r;
        }

        // TODO: since param
        /// <summary>
        ///     Retrieves the state ids.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="registration">The registration.</param>
        /// <returns>ProfileKeysLRSResponse.</returns>
        public ProfileKeysLRSResponse RetrieveStateIds(Activity activity, Agent agent, Guid? registration = null)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"activityId", activity.ID},
                {"agent", agent.ToJSON(Version)}
            };
            if (registration != null)
                queryParams.Add("registration", registration.ToString());

            return GetProfileKeys("activities/state", queryParams);
        }

        // TODO: since param
        /// <summary>
        ///     Retrieves the state ids.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="registration">The registration.</param>
        /// <returns>ProfileKeysLRSResponse.</returns>
        public async Task<ProfileKeysLRSResponse> RetrieveStateIdsAsync(Activity activity, Agent agent,
            Guid? registration = null)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"activityId", activity.ID},
                {"agent", agent.ToJSON(Version)}
            };
            if (registration != null)
                queryParams.Add("registration", registration.ToString());

            return await GetProfileKeysAsync("activities/state", queryParams);
        }

        /// <summary>
        ///     Retrieves the state.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="registration">The registration.</param>
        /// <returns>StateLRSResponse.</returns>
        public StateLRSResponse RetrieveState(string id, Activity activity, Agent agent, Guid? registration = null)
        {
            var r = new StateLRSResponse();

            var queryParams = new Dictionary<string, string>
            {
                {"stateId", id},
                {"activityId", activity.ID},
                {"agent", agent.ToJSON(Version)}
            };

            var state = new StateDocument
            {
                ID = id,
                Activity = activity,
                Agent = agent
            };

            if (registration != null)
            {
                queryParams.Add("registration", registration.ToString());
                state.Registration = registration;
            }

            var resp = GetDocument("activities/state", queryParams, state);
            if (resp.Status != HttpStatusCode.OK && resp.Status != HttpStatusCode.NotFound)
            {
                r.Success = false;
                r.HttpException = resp.Ex;
                r.SetErrMsgFromBytes(resp.Content);
                return r;
            }
            r.Success = true;
            r.Content = state;

            return r;
        }

        /// <summary>
        ///     Retrieves the state.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="registration">The registration.</param>
        /// <returns>StateLRSResponse.</returns>
        public async Task<StateLRSResponse> RetrieveStateAsync(string id, Activity activity, Agent agent,
            Guid? registration = null)
        {
            var r = new StateLRSResponse();

            var queryParams = new Dictionary<string, string>
            {
                {"stateId", id},
                {"activityId", activity.ID},
                {"agent", agent.ToJSON(Version)}
            };

            var state = new StateDocument
            {
                ID = id,
                Activity = activity,
                Agent = agent
            };

            if (registration != null)
            {
                queryParams.Add("registration", registration.ToString());
                state.Registration = registration;
            }

            var resp = await GetDocumentAsync("activities/state", queryParams, state);
            if (resp.Status != HttpStatusCode.OK && resp.Status != HttpStatusCode.NotFound)
            {
                r.Success = false;
                r.HttpException = resp.Ex;
                r.SetErrMsgFromBytes(resp.Content);
                return r;
            }
            r.Success = true;
            r.Content = state;

            return r;
        }

        /// <summary>
        ///     Saves the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>LRSResponse.</returns>
        public LRSResponse SaveState(StateDocument state)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"stateId", state.ID},
                {"activityId", state.Activity.ID},
                {"agent", state.Agent.ToJSON(Version)}
            };
            if (state.Registration != null)
                queryParams.Add("registration", state.Registration.ToString());

            return SaveDocument("activities/state", queryParams, state);
        }

        /// <summary>
        ///     Saves the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>LRSResponse.</returns>
        public async Task<LRSResponse> SaveStateAsync(StateDocument state)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"stateId", state.ID},
                {"activityId", state.Activity.ID},
                {"agent", state.Agent.ToJSON(Version)}
            };
            if (state.Registration != null)
                queryParams.Add("registration", state.Registration.ToString());

            return await SaveDocumentAsync("activities/state", queryParams, state);
        }

        /// <summary>
        ///     Deletes the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>LRSResponse.</returns>
        public LRSResponse DeleteState(StateDocument state)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"stateId", state.ID},
                {"activityId", state.Activity.ID},
                {"agent", state.Agent.ToJSON(Version)}
            };
            if (state.Registration != null)
                queryParams.Add("registration", state.Registration.ToString());

            return DeleteDocument("activities/state", queryParams);
        }

        /// <summary>
        ///     Deletes the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>LRSResponse.</returns>
        public async Task<LRSResponse> DeleteStateAsync(StateDocument state)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"stateId", state.ID},
                {"activityId", state.Activity.ID},
                {"agent", state.Agent.ToJSON(Version)}
            };
            if (state.Registration != null)
                queryParams.Add("registration", state.Registration.ToString());

            return await DeleteDocumentAsync("activities/state", queryParams);
        }

        /// <summary>
        ///     Clears the state.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="registration">The registration.</param>
        /// <returns>LRSResponse.</returns>
        public LRSResponse ClearState(Activity activity, Agent agent, Guid? registration = null)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"activityId", activity.ID},
                {"agent", agent.ToJSON(Version)}
            };
            if (registration != null)
                queryParams.Add("registration", registration.ToString());

            return DeleteDocument("activities/state", queryParams);
        }

        /// <summary>
        ///     Clears the state.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="registration">The registration.</param>
        /// <returns>LRSResponse.</returns>
        public async Task<LRSResponse> ClearStateAsync(Activity activity, Agent agent, Guid? registration = null)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"activityId", activity.ID},
                {"agent", agent.ToJSON(Version)}
            };
            if (registration != null)
                queryParams.Add("registration", registration.ToString());

            return await DeleteDocumentAsync("activities/state", queryParams);
        }

        // TODO: since param
        /// <summary>
        ///     Retrieves the activity profile ids.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns>ProfileKeysLRSResponse.</returns>
        public ProfileKeysLRSResponse RetrieveActivityProfileIds(Activity activity)
        {
            var queryParams = new Dictionary<string, string> {{"activityId", activity.ID}};

            return GetProfileKeys("activities/profile", queryParams);
        }

        // TODO: since param
        /// <summary>
        ///     Retrieves the activity profile ids.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns>ProfileKeysLRSResponse.</returns>
        public async Task<ProfileKeysLRSResponse> RetrieveActivityProfileIdsAsync(Activity activity)
        {
            var queryParams = new Dictionary<string, string> {{"activityId", activity.ID}};

            return await GetProfileKeysAsync("activities/profile", queryParams);
        }

        /// <summary>
        ///     Retrieves the activity profile.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="activity">The activity.</param>
        /// <returns>ActivityProfileLRSResponse.</returns>
        public ActivityProfileLRSResponse RetrieveActivityProfile(string id, Activity activity)
        {
            var r = new ActivityProfileLRSResponse();

            var queryParams = new Dictionary<string, string> {{"profileId", id}, {"activityId", activity.ID}};

            var profile = new ActivityProfileDocument
            {
                ID = id,
                Activity = activity
            };

            var resp = GetDocument("activities/profile", queryParams, profile);
            if (resp.Status != HttpStatusCode.OK && resp.Status != HttpStatusCode.NotFound)
            {
                r.Success = false;
                r.HttpException = resp.Ex;
                r.SetErrMsgFromBytes(resp.Content);
                return r;
            }
            r.Success = true;
            r.Content = profile;

            return r;
        }

        /// <summary>
        ///     Retrieves the activity profile.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="activity">The activity.</param>
        /// <returns>ActivityProfileLRSResponse.</returns>
        public async Task<ActivityProfileLRSResponse> RetrieveActivityProfileAsync(string id, Activity activity)
        {
            var r = new ActivityProfileLRSResponse();

            var queryParams = new Dictionary<string, string> {{"profileId", id}, {"activityId", activity.ID}};

            var profile = new ActivityProfileDocument
            {
                ID = id,
                Activity = activity
            };

            var resp = await GetDocumentAsync("activities/profile", queryParams, profile);
            if (resp.Status != HttpStatusCode.OK && resp.Status != HttpStatusCode.NotFound)
            {
                r.Success = false;
                r.HttpException = resp.Ex;
                r.SetErrMsgFromBytes(resp.Content);
                return r;
            }
            r.Success = true;
            r.Content = profile;

            return r;
        }

        /// <summary>
        ///     Saves the activity profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>LRSResponse.</returns>
        public LRSResponse SaveActivityProfile(ActivityProfileDocument profile)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"profileId", profile.ID},
                {"activityId", profile.Activity.ID}
            };

            return SaveDocument("activities/profile", queryParams, profile);
        }

        /// <summary>
        ///     Saves the activity profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>LRSResponse.</returns>
        public async Task<LRSResponse> SaveActivityProfileAsync(ActivityProfileDocument profile)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"profileId", profile.ID},
                {"activityId", profile.Activity.ID}
            };

            return await SaveDocumentAsync("activities/profile", queryParams, profile);
        }

        /// <summary>
        ///     Deletes the activity profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>LRSResponse.</returns>
        public LRSResponse DeleteActivityProfile(ActivityProfileDocument profile)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"profileId", profile.ID},
                {"activityId", profile.Activity.ID}
            };
            // TODO: need to pass Etag?

            return DeleteDocument("activities/profile", queryParams);
        }

        /// <summary>
        ///     Deletes the activity profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>LRSResponse.</returns>
        public async Task<LRSResponse> DeleteActivityProfileAsync(ActivityProfileDocument profile)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"profileId", profile.ID},
                {"activityId", profile.Activity.ID}
            };
            // TODO: need to pass Etag?

            return await DeleteDocumentAsync("activities/profile", queryParams);
        }

        // TODO: since param
        /// <summary>
        ///     Retrieves the agent profile ids.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <returns>ProfileKeysLRSResponse.</returns>
        public ProfileKeysLRSResponse RetrieveAgentProfileIds(Agent agent)
        {
            var queryParams = new Dictionary<string, string> {{"agent", agent.ToJSON(Version)}};

            return GetProfileKeys("agents/profile", queryParams);
        }

        /// <summary>
        ///     Retrieves the agent profile ids.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <returns>ProfileKeysLRSResponse.</returns>
        public async Task<ProfileKeysLRSResponse> RetrieveAgentProfileIdsAsync(Agent agent)
        {
            var queryParams = new Dictionary<string, string> {{"agent", agent.ToJSON(Version)}};

            return await GetProfileKeysAsync("agents/profile", queryParams);
        }

        /// <summary>
        ///     Retrieves the agent profile.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="agent">The agent.</param>
        /// <returns>AgentProfileLRSResponse.</returns>
        public AgentProfileLRSResponse RetrieveAgentProfile(string id, Agent agent)
        {
            var r = new AgentProfileLRSResponse();

            var queryParams = new Dictionary<string, string> {{"profileId", id}, {"agent", agent.ToJSON(Version)}};

            var profile = new AgentProfileDocument
            {
                ID = id,
                Agent = agent
            };

            var resp = GetDocument("agents/profile", queryParams, profile);
            if (resp.Status != HttpStatusCode.OK && resp.Status != HttpStatusCode.NotFound)
            {
                r.Success = false;
                r.HttpException = resp.Ex;
                r.SetErrMsgFromBytes(resp.Content);
                return r;
            }
            r.Success = true;
            r.Content = profile;

            return r;
        }

        /// <summary>
        ///     Retrieves the agent profile.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="agent">The agent.</param>
        /// <returns>AgentProfileLRSResponse.</returns>
        public async Task<AgentProfileLRSResponse> RetrieveAgentProfileAsync(string id, Agent agent)
        {
            var r = new AgentProfileLRSResponse();

            var queryParams = new Dictionary<string, string> {{"profileId", id}, {"agent", agent.ToJSON(Version)}};

            var profile = new AgentProfileDocument
            {
                ID = id,
                Agent = agent
            };

            var resp = await GetDocumentAsync("agents/profile", queryParams, profile);
            if (resp.Status != HttpStatusCode.OK && resp.Status != HttpStatusCode.NotFound)
            {
                r.Success = false;
                r.HttpException = resp.Ex;
                r.SetErrMsgFromBytes(resp.Content);
                return r;
            }
            r.Success = true;
            r.Content = profile;

            return r;
        }

        /// <summary>
        ///     Saves the agent profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>LRSResponse.</returns>
        public LRSResponse SaveAgentProfile(AgentProfileDocument profile)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"profileId", profile.ID},
                {"agent", profile.Agent.ToJSON(Version)}
            };

            return SaveDocument("agents/profile", queryParams, profile);
        }

        /// <summary>
        ///     Saves the agent profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>LRSResponse.</returns>
        public async Task<LRSResponse> SaveAgentProfileAsync(AgentProfileDocument profile)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"profileId", profile.ID},
                {"agent", profile.Agent.ToJSON(Version)}
            };

            return await SaveDocumentAsync("agents/profile", queryParams, profile);
        }

        /// <summary>
        ///     Deletes the agent profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>LRSResponse.</returns>
        public LRSResponse DeleteAgentProfile(AgentProfileDocument profile)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"profileId", profile.ID},
                {"agent", profile.Agent.ToJSON(Version)}
            };
            // TODO: need to pass Etag?

            return DeleteDocument("agents/profile", queryParams);
        }

        /// <summary>
        ///     Deletes the agent profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>LRSResponse.</returns>
        public async Task<LRSResponse> DeleteAgentProfileAsync(AgentProfileDocument profile)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"profileId", profile.ID},
                {"agent", profile.Agent.ToJSON(Version)}
            };
            // TODO: need to pass Etag?

            return await DeleteDocumentAsync("agents/profile", queryParams);
        }

        /// <summary>
        ///     Deletes the document.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="queryParams">The query parameters.</param>
        /// <returns>LRSResponse.</returns>
        private LRSResponse DeleteDocument(string resource, Dictionary<string, string> queryParams)
        {
            var r = new LRSResponse();

            var req = new MyHttpRequest
            {
                Method = "DELETE",
                Resource = resource,
                QueryParams = queryParams
            };

            var res = MakeSyncRequest(req);
            if (res.Status != HttpStatusCode.NoContent)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            r.Success = true;

            return r;
        }

        /// <summary>
        ///     Deletes the document.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="queryParams">The query parameters.</param>
        /// <returns>LRSResponse.</returns>
        private async Task<LRSResponse> DeleteDocumentAsync(string resource, Dictionary<string, string> queryParams)
        {
            var r = new LRSResponse();

            var req = new MyHttpRequest
            {
                Method = "DELETE",
                Resource = resource,
                QueryParams = queryParams
            };

            var res = await MakeAsyncRequest(req);
            if (res.Status != HttpStatusCode.NoContent)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            r.Success = true;

            return r;
        }

        /// <summary>
        ///     Gets the document.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="queryParams">The query parameters.</param>
        /// <param name="document">The document.</param>
        /// <returns>MyHTTPResponse.</returns>
        private MyHttpResponse GetDocument(string resource, Dictionary<string, string> queryParams, Document document)
        {
            var req = new MyHttpRequest
            {
                Method = "GET",
                Resource = resource,
                QueryParams = queryParams
            };

            var res = MakeSyncRequest(req);
            if (res.Status == HttpStatusCode.OK)
            {
                document.Content = res.Content;
                document.ContentType = res.ContentType;
                document.Timestamp = res.LastModified;
                document.Etag = res.Etag;
            }

            return res;
        }

        /// <summary>
        ///     Gets the document.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="queryParams">The query parameters.</param>
        /// <param name="document">The document.</param>
        /// <returns>MyHTTPResponse.</returns>
        private async Task<MyHttpResponse> GetDocumentAsync(string resource, Dictionary<string, string> queryParams,
            Document document)
        {
            var req = new MyHttpRequest
            {
                Method = "GET",
                Resource = resource,
                QueryParams = queryParams
            };

            var res = await MakeAsyncRequest(req);
            if (res.Status == HttpStatusCode.OK)
            {
                document.Content = res.Content;
                document.ContentType = res.ContentType;
                document.Timestamp = res.LastModified;
                document.Etag = res.Etag;
            }

            return res;
        }

        /// <summary>
        ///     Gets the profile keys.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="queryParams">The query parameters.</param>
        /// <returns>ProfileKeysLRSResponse.</returns>
        private ProfileKeysLRSResponse GetProfileKeys(string resource, Dictionary<string, string> queryParams)
        {
            var r = new ProfileKeysLRSResponse();

            var req = new MyHttpRequest
            {
                Method = "GET",
                Resource = resource,
                QueryParams = queryParams
            };

            var res = MakeSyncRequest(req);
            if (res.Status != HttpStatusCode.OK)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            r.Success = true;

            var keys = JArray.Parse(Encoding.UTF8.GetString(res.Content));
            if (keys.Count > 0)
            {
                r.Content = new List<string>();
                foreach (var key in keys)
                    r.Content.Add((string) key);
            }

            return r;
        }

        /// <summary>
        ///     Gets the profile keys.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="queryParams">The query parameters.</param>
        /// <returns>ProfileKeysLRSResponse.</returns>
        private async Task<ProfileKeysLRSResponse> GetProfileKeysAsync(string resource,
            Dictionary<string, string> queryParams)
        {
            var r = new ProfileKeysLRSResponse();

            var req = new MyHttpRequest
            {
                Method = "GET",
                Resource = resource,
                QueryParams = queryParams
            };

            var res = await MakeAsyncRequest(req);
            if (res.Status != HttpStatusCode.OK)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            r.Success = true;

            var keys = JArray.Parse(Encoding.UTF8.GetString(res.Content));
            if (keys.Count > 0)
            {
                r.Content = new List<string>();
                foreach (var key in keys)
                    r.Content.Add((string) key);
            }

            return r;
        }

        /// <summary>
        ///     Gets the statement.
        /// </summary>
        /// <param name="queryParams">The query parameters.</param>
        /// <returns>StatementLRSResponse.</returns>
        private StatementLRSResponse GetStatement(Dictionary<string, string> queryParams)
        {
            var r = new StatementLRSResponse();

            var req = new MyHttpRequest
            {
                Method = "GET",
                Resource = "statements",
                QueryParams = queryParams
            };

            var res = MakeSyncRequest(req);
            if (res.Status != HttpStatusCode.OK)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            r.Success = true;
            r.Content = new Statement(new StringOfJSON(Encoding.UTF8.GetString(res.Content)));

            return r;
        }

        /// <summary>
        ///     Gets the statement.
        /// </summary>
        /// <param name="queryParams">The query parameters.</param>
        /// <returns>StatementLRSResponse.</returns>
        private async Task<StatementLRSResponse> GetStatementAsync(Dictionary<string, string> queryParams)
        {
            var r = new StatementLRSResponse();

            var req = new MyHttpRequest
            {
                Method = "GET",
                Resource = "statements",
                QueryParams = queryParams
            };

            var res = await MakeAsyncRequest(req);
            if (res.Status != HttpStatusCode.OK)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            r.Success = true;
            r.Content = new Statement(new StringOfJSON(Encoding.UTF8.GetString(res.Content)));

            return r;
        }

        /// <summary>
        ///     Makes the synchronize request.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <returns>MyHTTPResponse.</returns>
        private async Task<MyHttpResponse> MakeAsyncRequest(MyHttpRequest req)
        {
            string url;
            if (req.Resource.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
            {
                url = req.Resource;
            }
            else
            {
                url = Endpoint.ToString();
                if (!url.EndsWith("/") && !req.Resource.StartsWith("/"))
                    url += "/";
                url += req.Resource;
            }

            if (req.QueryParams != null)
            {
                var qs = "";
                foreach (var entry in req.QueryParams)
                {
                    if (qs != "")
                        qs += "&";
                    qs += WebUtility.UrlEncode(entry.Key) + "=" + WebUtility.UrlEncode(entry.Value);
                }
                if (qs != "")
                    url += "?" + qs;
            }

            // TODO: handle special properties we recognize, such as content type, modified since, etc.
            var webReq = (HttpWebRequest) WebRequest.Create(url);
            webReq.Method = req.Method;

            webReq.Headers.Add("X-Experience-API-Version", Version.ToString());
            if (Auth != null)
                webReq.Headers.Add("Authorization", Auth);
            if (req.Headers != null)
                foreach (var entry in req.Headers)
                    webReq.Headers.Add(entry.Key, entry.Value);

            webReq.ContentType = req.ContentType ?? "application/octet-stream";

            if (req.Content != null)
            {
                webReq.ContentLength = req.Content.Length;
                using (var stream = await webReq.GetRequestStreamAsync())
                {
                    stream.Write(req.Content, 0, req.Content.Length);
                }
            }

            MyHttpResponse resp;

            try
            {
                using (var webResp = (HttpWebResponse) webReq.GetResponse())
                {
                    resp = new MyHttpResponse(webResp);
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                    using (var webResp = (HttpWebResponse) ex.Response)
                    {
                        resp = new MyHttpResponse(webResp);
                    }
                else
                    resp = new MyHttpResponse {Content = Encoding.UTF8.GetBytes("Web exception without '.Response'")};
                resp.Ex = ex;
            }

            return resp;
        }

        /// <summary>
        ///     Makes the synchronize request.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <returns>MyHTTPResponse.</returns>
        private MyHttpResponse MakeSyncRequest(MyHttpRequest req)
        {
            string url;
            if (req.Resource.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
            {
                url = req.Resource;
            }
            else
            {
                url = Endpoint.ToString();
                if (!url.EndsWith("/") && !req.Resource.StartsWith("/"))
                    url += "/";
                url += req.Resource;
            }

            if (req.QueryParams != null)
            {
                var qs = "";
                foreach (var entry in req.QueryParams)
                {
                    if (qs != "")
                        qs += "&";
                    qs += WebUtility.UrlEncode(entry.Key) + "=" + WebUtility.UrlEncode(entry.Value);
                }
                if (qs != "")
                    url += "?" + qs;
            }

            // TODO: handle special properties we recognize, such as content type, modified since, etc.
            var webReq = (HttpWebRequest) WebRequest.Create(url);
            webReq.Method = req.Method;

            webReq.Headers.Add("X-Experience-API-Version", Version.ToString());
            if (Auth != null)
                webReq.Headers.Add("Authorization", Auth);
            if (req.Headers != null)
                foreach (var entry in req.Headers)
                    webReq.Headers.Add(entry.Key, entry.Value);

            webReq.ContentType = req.ContentType ?? "application/octet-stream";

            if (req.Content != null)
            {
                webReq.ContentLength = req.Content.Length;
                using (var stream = webReq.GetRequestStream())
                {
                    stream.Write(req.Content, 0, req.Content.Length);
                }
            }

            MyHttpResponse resp;

            try
            {
                using (var webResp = (HttpWebResponse) webReq.GetResponse())
                {
                    resp = new MyHttpResponse(webResp);
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                    using (var webResp = (HttpWebResponse) ex.Response)
                    {
                        resp = new MyHttpResponse(webResp);
                    }
                else
                    resp = new MyHttpResponse {Content = Encoding.UTF8.GetBytes("Web exception without '.Response'")};
                resp.Ex = ex;
            }

            return resp;
        }

        /// <summary>
        ///     See http://www.yoda.arachsys.com/csharp/readbinary.html no license found
        ///     Reads data from a stream until the end is reached. The
        ///     data is returned as a byte array. An IOException is
        ///     thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <param name="initialLength">The initial buffer length</param>
        /// <returns>System.Byte[].</returns>
        private static byte[] ReadFully(Stream stream, int initialLength)
        {
            // If we've been passed an unhelpful initial length, just
            // use 32K.
            if (initialLength < 1)
                initialLength = 32768;

            var buffer = new byte[initialLength];
            var read = 0;

            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    var nextByte = stream.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                        return buffer;

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    var newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte) nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            // Buffer is now too big. Shrink it.
            var ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }

        /// <summary>
        ///     Saves the document.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="queryParams">The query parameters.</param>
        /// <param name="document">The document.</param>
        /// <returns>LRSResponse.</returns>
        private LRSResponse SaveDocument(string resource, Dictionary<string, string> queryParams, Document document)
        {
            var r = new LRSResponse();

            var req = new MyHttpRequest
            {
                Method = "PUT",
                Resource = resource,
                QueryParams = queryParams,
                ContentType = document.ContentType,
                Content = document.Content
            };

            var res = MakeSyncRequest(req);
            if (res.Status != HttpStatusCode.NoContent)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            r.Success = true;

            return r;
        }

        /// <summary>
        ///     Saves the document.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="queryParams">The query parameters.</param>
        /// <param name="document">The document.</param>
        /// <returns>LRSResponse.</returns>
        private async Task<LRSResponse> SaveDocumentAsync(string resource, Dictionary<string, string> queryParams,
            Document document)
        {
            var r = new LRSResponse();

            var req = new MyHttpRequest
            {
                Method = "PUT",
                Resource = resource,
                QueryParams = queryParams,
                ContentType = document.ContentType,
                Content = document.Content
            };

            var res = await MakeAsyncRequest(req);
            if (res.Status != HttpStatusCode.NoContent)
            {
                r.Success = false;
                r.HttpException = res.Ex;
                r.SetErrMsgFromBytes(res.Content);
                return r;
            }

            r.Success = true;

            return r;
        }

        /// <summary>
        ///     Sets the authentication.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void SetAuth(string username, string password)
        {
            Auth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
        }

        /// <summary>
        ///     Class MyHTTPRequest.
        /// </summary>
        private class MyHttpRequest
        {
            /// <summary>
            ///     Gets or sets the method.
            /// </summary>
            /// <value>The method.</value>
            public string Method { get; set; }

            /// <summary>
            ///     Gets or sets the resource.
            /// </summary>
            /// <value>The resource.</value>
            public string Resource { get; set; }

            /// <summary>
            ///     Gets or sets the query parameters.
            /// </summary>
            /// <value>The query parameters.</value>
            public Dictionary<string, string> QueryParams { get; set; }

            /// <summary>
            ///     Gets or sets the headers.
            /// </summary>
            /// <value>The headers.</value>
            public Dictionary<string, string> Headers { get; set; }

            /// <summary>
            ///     Gets or sets the type of the content.
            /// </summary>
            /// <value>The type of the content.</value>
            public string ContentType { get; set; }

            /// <summary>
            ///     Gets or sets the content.
            /// </summary>
            /// <value>The content.</value>
            public byte[] Content { get; set; }
        }

        /// <summary>
        ///     Class MyHTTPResponse.
        /// </summary>
        private class MyHttpResponse
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="MyHttpResponse" /> class.
            /// </summary>
            public MyHttpResponse()
            {
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="MyHttpResponse" /> class.
            /// </summary>
            /// <param name="webResp">The web resp.</param>
            public MyHttpResponse(HttpWebResponse webResp)
            {
                Status = webResp.StatusCode;
                ContentType = webResp.ContentType;
                Etag = webResp.Headers.Get("Etag");
                LastModified = webResp.LastModified;

                using (var stream = webResp.GetResponseStream())
                {
                    Content = ReadFully(stream, (int) webResp.ContentLength);
                }
            }

            /// <summary>
            ///     Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            public HttpStatusCode Status { get; }

            /// <summary>
            ///     Gets or sets the type of the content.
            /// </summary>
            /// <value>The type of the content.</value>
            public string ContentType { get; }

            /// <summary>
            ///     Gets or sets the content.
            /// </summary>
            /// <value>The content.</value>
            public byte[] Content { get; set; }

            /// <summary>
            ///     Gets or sets the last modified.
            /// </summary>
            /// <value>The last modified.</value>
            public DateTime LastModified { get; }

            /// <summary>
            ///     Gets or sets the etag.
            /// </summary>
            /// <value>The etag.</value>
            public string Etag { get; }

            /// <summary>
            ///     Gets or sets the ex.
            /// </summary>
            /// <value>The ex.</value>
            public Exception Ex { get; set; }
        }
    }
}