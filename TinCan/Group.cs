#region License and Warranty Information

// ==========================================================
//  <copyright file="Group.cs" company="iWork Technologies">
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
using Newtonsoft.Json.Linq;
using TinCan.Json;

#endregion

namespace TinCan
{
    /// <summary>
    /// Class Group.
    /// </summary>
    /// <seealso cref="TinCan.Agent" />
    public class Group : Agent
    {
        /// <summary>
        /// The object type
        /// </summary>
        public static readonly new string OBJECT_TYPE = "Group";

        /// <summary>
        /// Initializes a new instance of the <see cref="Group"/> class.
        /// </summary>
        public Group()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Group"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public Group(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Group"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public Group(JObject jobj) : base(jobj)
        {
            if (jobj["member"] != null)
            {
                Member = new List<Agent>();
                foreach (var jToken in jobj["member"])
                {
                    var jagent = (JObject) jToken;
                    if (jagent == null) throw new ArgumentNullException(nameof(jagent));
                    Member.Add(new Agent(jagent));
                }
            }
        }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public override string ObjectType => OBJECT_TYPE;

        /// <summary>
        /// Gets or sets the member.
        /// </summary>
        /// <value>The member.</value>
        public List<Agent> Member { get; set; }

        /// <summary>
        /// To the j object.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>JObject.</returns>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = base.ToJObject(version);
            if (Member != null && Member.Count > 0)
            {
                var jmember = new JArray();
                result.Add("member", jmember);

                foreach (var agent in Member)
                {
                    jmember.Add(agent.ToJObject(version));
                }
            }

            return result;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="JObject"/> to <see cref="Group"/>.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Group(JObject jobj)
        {
            return new Group(jobj);
        }
    }
}