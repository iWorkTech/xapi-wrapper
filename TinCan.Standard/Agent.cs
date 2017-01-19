#region License and Warranty Information

// ==========================================================
//  <copyright file="Agent.cs" company="iWork Technologies">
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

using Newtonsoft.Json.Linq;
using TinCan.Standard.Json;

#endregion

namespace TinCan.Standard
{
    /// <summary>
    /// Class Agent.
    /// </summary>
    /// <seealso cref="JsonModel" />
    /// <seealso cref="IStatementTarget" />
    public class Agent : JsonModel, IStatementTarget
    {
        /// <summary>
        /// The object type
        /// </summary>
        public static readonly string OBJECT_TYPE = "Agent";

        /// <summary>
        /// Initializes a new instance of the <see cref="Agent"/> class.
        /// </summary>
        public Agent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Agent"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public Agent(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Agent"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public Agent(JObject jobj)
        {
            if (jobj["name"] != null)
            {
                Name = jobj.Value<string>("name");
            }

            if (jobj["mbox"] != null)
            {
                Mbox = jobj.Value<string>("mbox");
            }
            if (jobj["Mbox_Sha1Sum"] != null)
            {
                Mbox_Sha1Sum = jobj.Value<string>("Mbox_Sha1Sum");
            }
            if (jobj["openid"] != null)
            {
                Openid = jobj.Value<string>("openid");
            }
            if (jobj["account"] != null)
            {
                Account = (AgentAccount) jobj.Value<JObject>("account");
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the mbox.
        /// </summary>
        /// <value>The mbox.</value>
        public string Mbox { get; set; }
        /// <summary>
        /// Gets or sets the mbox sha1sum.
        /// </summary>
        /// <value>The mbox sha1sum.</value>
        public string Mbox_Sha1Sum { get; set; }
        /// <summary>
        /// Gets or sets the openid.
        /// </summary>
        /// <value>The openid.</value>
        public string Openid { get; set; }
        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        /// <value>The account.</value>
        public AgentAccount Account { get; set; }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public virtual string ObjectType => OBJECT_TYPE;

        /// <summary>
        /// To the j object.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>JObject.</returns>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject {{"objectType", ObjectType}};

            if (Name != null)
            {
                result.Add("name", Name);
            }

            if (Account != null)
            {
                result.Add("account", Account.ToJObject(version));
            }
            else if (Mbox != null)
            {
                result.Add("mbox", Mbox);
            }
            else if (Mbox_Sha1Sum != null)
            {
                result.Add("Mbox_Sha1Sum", Mbox_Sha1Sum);
            }
            else if (Openid != null)
            {
                result.Add("openid", Openid);
            }

            return result;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="JObject"/> to <see cref="Agent"/>.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Agent(JObject jobj)
        {
            return new Agent(jobj);
        }
    }
}