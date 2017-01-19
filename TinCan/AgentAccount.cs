#region License and Warranty Information

// ==========================================================
//  <copyright file="AgentAccount.cs" company="iWork Technologies">
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
    /// Class AgentAccount.
    /// </summary>
    /// <seealso cref="TinCan.Json.JsonModel" />
    public class AgentAccount : JsonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgentAccount"/> class.
        /// </summary>
        public AgentAccount()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentAccount"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public AgentAccount(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentAccount"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public AgentAccount(JObject jobj)
        {
            if (jobj["homePage"] != null)
            {
                HomePage = new Uri(jobj.Value<string>("homePage"));
            }
            if (jobj["name"] != null)
            {
                Name = jobj.Value<string>("name");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentAccount"/> class.
        /// </summary>
        /// <param name="homePage">The home page.</param>
        /// <param name="name">The name.</param>
        public AgentAccount(Uri homePage, string name)
        {
            this.HomePage = homePage;
            this.Name = name;
        }

        // TODO: check to make sure is absolute?
        /// <summary>
        /// Gets or sets the home page.
        /// </summary>
        /// <value>The home page.</value>
        public Uri HomePage { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// To the j object.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>JObject.</returns>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();
            if (HomePage != null)
            {
                result.Add("homePage", HomePage.ToString());
            }
            if (Name != null)
            {
                result.Add("name", Name);
            }

            return result;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="JObject"/> to <see cref="AgentAccount"/>.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator AgentAccount(JObject jobj)
        {
            return new AgentAccount(jobj);
        }
    }
}