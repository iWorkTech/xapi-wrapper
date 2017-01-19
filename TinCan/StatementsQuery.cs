#region License and Warranty Information

// ==========================================================
//  <copyright file="StatementsQuery.cs" company="iWork Technologies">
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

#endregion

namespace TinCan
{
    /// <summary>
    /// Class StatementsQuery.
    /// </summary>
    public class StatementsQuery
    {
        // TODO: put in common location
        /// <summary>
        /// The iso date time format
        /// </summary>
        private const string ISO_DATE_TIME_FORMAT = "o";
        /// <summary>
        /// The activity identifier
        /// </summary>
        private string _activityId;

        /// <summary>
        /// Gets or sets the agent.
        /// </summary>
        /// <value>The agent.</value>
        public Agent Agent { get; set; }
        /// <summary>
        /// Gets or sets the verb identifier.
        /// </summary>
        /// <value>The verb identifier.</value>
        public Uri VerbId { get; set; }

        /// <summary>
        /// Gets or sets the activity identifier.
        /// </summary>
        /// <value>The activity identifier.</value>
        public string ActivityId
        {
            get { return _activityId; }
            set
            {
                var uri = new Uri(value);
                _activityId = value;
            }
        }

        /// <summary>
        /// Gets or sets the registration.
        /// </summary>
        /// <value>The registration.</value>
        public Guid? Registration { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [related activities].
        /// </summary>
        /// <value><c>null</c> if [related activities] contains no value, <c>true</c> if [related activities]; otherwise, <c>false</c>.</value>
        public bool? RelatedActivities { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [related agents].
        /// </summary>
        /// <value><c>null</c> if [related agents] contains no value, <c>true</c> if [related agents]; otherwise, <c>false</c>.</value>
        public bool? RelatedAgents { get; set; }
        /// <summary>
        /// Gets or sets the since.
        /// </summary>
        /// <value>The since.</value>
        public DateTime? Since { get; set; }
        /// <summary>
        /// Gets or sets the until.
        /// </summary>
        /// <value>The until.</value>
        public DateTime? Until { get; set; }
        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>The limit.</value>
        public int? Limit { get; set; }
        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        /// <value>The format.</value>
        public StatementsQueryResultFormat Format { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="StatementsQuery"/> is ascending.
        /// </summary>
        /// <value><c>null</c> if [ascending] contains no value, <c>true</c> if [ascending]; otherwise, <c>false</c>.</value>
        public bool? Ascending { get; set; }

        /// <summary>
        /// To the parameter map.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        public Dictionary<string, string> ToParameterMap(TCAPIVersion version)
        {
            var result = new Dictionary<string, string>();

            if (Agent != null)
            {
                result.Add("agent", Agent.ToJSON(version));
            }
            if (VerbId != null)
            {
                result.Add("verb", VerbId.ToString());
            }
            if (ActivityId != null)
            {
                result.Add("activity", ActivityId);
            }
            if (Registration != null)
            {
                result.Add("registration", Registration.Value.ToString());
            }
            if (RelatedActivities != null)
            {
                result.Add("related_activities", RelatedActivities.Value.ToString());
            }
            if (RelatedAgents != null)
            {
                result.Add("related_agents", RelatedAgents.Value.ToString());
            }
            if (Since != null)
            {
                result.Add("since", Since.Value.ToString(ISO_DATE_TIME_FORMAT));
            }
            if (Until != null)
            {
                result.Add("until", Until.Value.ToString(ISO_DATE_TIME_FORMAT));
            }
            if (Limit != null)
            {
                result.Add("limit", Limit.ToString());
            }
            if (Format != null)
            {
                result.Add("format", Format.ToString());
            }
            if (Ascending != null)
            {
                result.Add("ascending", Ascending.Value.ToString());
            }

            return result;
        }
    }
}