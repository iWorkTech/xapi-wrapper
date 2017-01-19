#region License and Warranty Information

// ==========================================================
//  <copyright file="Context.cs" company="iWork Technologies">
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
using TinCan.Standard.Json;

#endregion

namespace TinCan.Standard
{
    /// <summary>
    /// Class Context.
    /// </summary>
    /// <seealso cref="JsonModel" />
    public class Context : JsonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        public Context()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public Context(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public Context(JObject jobj)
        {
            if (jobj["registration"] != null)
            {
                Registration = new Guid(jobj.Value<string>("registration"));
            }
            if (jobj["instructor"] != null)
            {
                // TODO: can be Group?
                Instructor = (Agent) jobj.Value<JObject>("instructor");
            }
            if (jobj["team"] != null)
            {
                // TODO: can be Group?
                Team = (Agent) jobj.Value<JObject>("team");
            }
            if (jobj["contextActivities"] != null)
            {
                ContextActivities = (ContextActivities) jobj.Value<JObject>("contextActivities");
            }
            if (jobj["revision"] != null)
            {
                Revision = jobj.Value<string>("revision");
            }
            if (jobj["platform"] != null)
            {
                Platform = jobj.Value<string>("platform");
            }
            if (jobj["language"] != null)
            {
                Language = jobj.Value<string>("language");
            }
            if (jobj["statement"] != null)
            {
                Statement = (StatementRef) jobj.Value<JObject>("statement");
            }
            if (jobj["extensions"] != null)
            {
                Extensions = (Extensions) jobj.Value<JObject>("extensions");
            }
        }

        /// <summary>
        /// Gets or sets the registration.
        /// </summary>
        /// <value>The registration.</value>
        public Guid? Registration { get; set; }
        /// <summary>
        /// Gets or sets the instructor.
        /// </summary>
        /// <value>The instructor.</value>
        public Agent Instructor { get; set; }
        /// <summary>
        /// Gets or sets the team.
        /// </summary>
        /// <value>The team.</value>
        public Agent Team { get; set; }
        /// <summary>
        /// Gets or sets the context activities.
        /// </summary>
        /// <value>The context activities.</value>
        public ContextActivities ContextActivities { get; set; }
        /// <summary>
        /// Gets or sets the revision.
        /// </summary>
        /// <value>The revision.</value>
        public string Revision { get; set; }
        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>The platform.</value>
        public string Platform { get; set; }
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; }
        /// <summary>
        /// Gets or sets the statement.
        /// </summary>
        /// <value>The statement.</value>
        public StatementRef Statement { get; set; }
        /// <summary>
        /// Gets or sets the extensions.
        /// </summary>
        /// <value>The extensions.</value>
        public Extensions Extensions { get; set; }

        /// <summary>
        /// To the j object.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>JObject.</returns>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();

            if (Registration != null)
            {
                result.Add("registration", Registration.ToString());
            }
            if (Instructor != null)
            {
                result.Add("instructor", Instructor.ToJObject(version));
            }
            if (Team != null)
            {
                result.Add("team", Team.ToJObject(version));
            }
            if (ContextActivities != null)
            {
                result.Add("contextActivities", ContextActivities.ToJObject(version));
            }
            if (Revision != null)
            {
                result.Add("revision", Revision);
            }
            if (Platform != null)
            {
                result.Add("platform", Platform);
            }
            if (Language != null)
            {
                result.Add("language", Language);
            }
            if (Statement != null)
            {
                result.Add("statement", Statement.ToJObject(version));
            }
            if (Extensions != null)
            {
                result.Add("extensions", Extensions.ToJObject(version));
            }

            return result;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="JObject"/> to <see cref="Context"/>.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Context(JObject jobj)
        {
            return new Context(jobj);
        }
    }
}