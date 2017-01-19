#region License and Warranty Information

// ==========================================================
//  <copyright file="SubStatement.cs" company="iWork Technologies">
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
using TinCan.Json;

#endregion

namespace TinCan
{
    /// <summary>
    /// Class SubStatement.
    /// </summary>
    /// <seealso cref="TinCan.StatementBase" />
    /// <seealso cref="IStatementTarget" />
    public class SubStatement : StatementBase, IStatementTarget
    {
        /// <summary>
        /// The object type
        /// </summary>
        public static readonly string OBJECT_TYPE = "SubStatement";

        /// <summary>
        /// Initializes a new instance of the <see cref="SubStatement"/> class.
        /// </summary>
        public SubStatement()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubStatement"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public SubStatement(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubStatement"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public SubStatement(JObject jobj) : base(jobj)
        {
        }

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
            var result = base.ToJObject(version);

            result.Add("objectType", ObjectType);

            return result;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="JObject"/> to <see cref="SubStatement"/>.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator SubStatement(JObject jobj)
        {
            return new SubStatement(jobj);
        }
    }
}