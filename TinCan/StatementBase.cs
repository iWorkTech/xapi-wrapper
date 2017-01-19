#region License and Warranty Information

// ==========================================================
//  <copyright file="StatementBase.cs" company="iWork Technologies">
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
using TinCan.Json;

#endregion

namespace TinCan
{
    /// <summary>
    /// Class StatementBase.
    /// </summary>
    /// <seealso cref="TinCan.Json.JsonModel" />
    public abstract class StatementBase : JsonModel
    {
        /// <summary>
        /// The iso date time format
        /// </summary>
        private const string ISO_DATE_TIME_FORMAT = "o";

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementBase"/> class.
        /// </summary>
        protected StatementBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementBase"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        protected StatementBase(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementBase"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        protected StatementBase(JObject jobj)
        {
            if (jobj["actor"] != null)
            {
                if (jobj["actor"]["objectType"] != null && (string) jobj["actor"]["objectType"] == Group.OBJECT_TYPE)
                {
                    Actor = (Group) jobj.Value<JObject>("actor");
                }
                else
                {
                    Actor = (Agent) jobj.Value<JObject>("actor");
                }
            }
            if (jobj["verb"] != null)
            {
                Verb = (Verb) jobj.Value<JObject>("verb");
            }
            if (jobj["object"] != null)
            {
                if (jobj["object"]["objectType"] != null)
                {
                    if ((string) jobj["object"]["objectType"] == Group.OBJECT_TYPE)
                    {
                        Target = (Group) jobj.Value<JObject>("object");
                    }
                    else if ((string) jobj["object"]["objectType"] == Agent.OBJECT_TYPE)
                    {
                        Target = (Agent) jobj.Value<JObject>("object");
                    }
                    else if ((string) jobj["object"]["objectType"] == Activity.OBJECT_TYPE)
                    {
                        Target = (Activity) jobj.Value<JObject>("object");
                    }
                    else if ((string) jobj["object"]["objectType"] == StatementRef.OBJECT_TYPE)
                    {
                        Target = (StatementRef) jobj.Value<JObject>("object");
                    }
                }
                else
                {
                    Target = (Activity) jobj.Value<JObject>("object");
                }
            }
            if (jobj["result"] != null)
            {
                Result = (Result) jobj.Value<JObject>("result");
            }
            if (jobj["context"] != null)
            {
                Context = (Context) jobj.Value<JObject>("context");
            }
            if (jobj["timestamp"] != null)
            {
                Timestamp = jobj.Value<DateTime>("timestamp");
            }
        }

        /// <summary>
        /// Gets or sets the actor.
        /// </summary>
        /// <value>The actor.</value>
        public Agent Actor { get; set; }
        /// <summary>
        /// Gets or sets the verb.
        /// </summary>
        /// <value>The verb.</value>
        public Verb Verb { get; set; }
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        public IStatementTarget Target { get; set; }
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public Result Result { get; set; }
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public Context Context { get; set; }
        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>The timestamp.</value>
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// To the j object.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>JObject.</returns>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();

            if (Actor != null)
            {
                result.Add("actor", Actor.ToJObject(version));
            }

            if (Verb != null)
            {
                result.Add("verb", Verb.ToJObject(version));
            }

            if (Target != null)
            {
                result.Add("object", Target.ToJObject(version));
            }
            if (Result != null)
            {
                result.Add("result", Result.ToJObject(version));
            }
            if (Context != null)
            {
                result.Add("context", Context.ToJObject(version));
            }
            if (Timestamp != null)
            {
                result.Add("timestamp", Timestamp.Value.ToString(ISO_DATE_TIME_FORMAT));
            }

            return result;
        }
    }
}