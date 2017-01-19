#region License and Warranty Information

// ==========================================================
//  <copyright file="Statement.cs" company="iWork Technologies">
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
    /// Class Statement.
    /// </summary>
    /// <seealso cref="TinCan.StatementBase" />
    public class Statement : StatementBase
    {
        // TODO: put in common location
        /// <summary>
        /// The iso date time format
        /// </summary>
        private const string ISO_DATE_TIME_FORMAT = "o";
        //public List<Attachment> attachments { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Statement" /> class.
        /// </summary>
        public Statement()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Statement"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public Statement(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Statement"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public Statement(JObject jobj) : base(jobj)
        {
            if (jobj["id"] != null)
            {
                ID = new Guid(jobj.Value<string>("id"));
            }
            if (jobj["stored"] != null)
            {
                Stored = jobj.Value<DateTime>("stored");
            }
            if (jobj["authority"] != null)
            {
                Authority = (Agent) jobj.Value<JObject>("authority");
            }
            if (jobj["version"] != null)
            {
                Version = (TCAPIVersion) jobj.Value<string>("version");
            }

            //
            // handle SubStatement as target which isn't provided by StatementBase
            // because SubStatements are not allowed to nest
            //
            if (jobj["object"] != null && (string) jobj["object"]["objectType"] == SubStatement.OBJECT_TYPE)
            {
                Target = (SubStatement) jobj.Value<JObject>("object");
            }
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid? ID { get; set; }
        /// <summary>
        /// Gets or sets the stored.
        /// </summary>
        /// <value>The stored.</value>
        public DateTime? Stored { get; set; }
        /// <summary>
        /// Gets or sets the authority.
        /// </summary>
        /// <value>The authority.</value>
        public Agent Authority { get; set; }
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public TCAPIVersion Version { get; set; }

        /// <summary>
        /// To the j object.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>JObject.</returns>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = base.ToJObject(version);

            if (ID != null)
            {
                result.Add("id", ID.ToString());
            }
            if (Stored != null)
            {
                result.Add("stored", Stored.Value.ToString(ISO_DATE_TIME_FORMAT));
            }
            if (Authority != null)
            {
                result.Add("authority", Authority.ToJObject(version));
            }
            if (version != null)
            {
                result.Add("version", version.ToString());
            }

            return result;
        }

        /// <summary>
        /// Stamps this instance.
        /// </summary>
        public void Stamp()
        {
            if (ID == null)
            {
                ID = Guid.NewGuid();
            }
            if (Timestamp == null)
            {
                Timestamp = DateTime.UtcNow;
            }
        }
    }
}