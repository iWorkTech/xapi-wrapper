#region License and Warranty Information

// ==========================================================
//  <copyright file="StatementRef.cs" company="iWork Technologies">
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
    /// Class StatementRef.
    /// </summary>
    /// <seealso cref="TinCan.Json.JsonModel" />
    /// <seealso cref="TinCan.IStatementTarget" />
    public class StatementRef : JsonModel, IStatementTarget
    {
        /// <summary>
        /// The object type
        /// </summary>
        public static readonly string OBJECT_TYPE = "StatementRef";

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementRef"/> class.
        /// </summary>
        public StatementRef()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementRef"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public StatementRef(Guid id)
        {
            ID = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementRef"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public StatementRef(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementRef"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public StatementRef(JObject jobj)
        {
            if (jobj["id"] != null)
            {
                ID = new Guid(jobj.Value<string>("id"));
            }
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid? ID { get; set; }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public string ObjectType => OBJECT_TYPE;

        /// <summary>
        /// To the j object.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>JObject.</returns>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject {{"objectType", ObjectType}};

            if (ID != null)
            {
                result.Add("id", ID.ToString());
            }

            return result;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="JObject"/> to <see cref="StatementRef"/>.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator StatementRef(JObject jobj)
        {
            return new StatementRef(jobj);
        }
    }
}