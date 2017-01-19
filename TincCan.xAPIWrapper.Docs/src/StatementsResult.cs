#region License and Warranty Information

// ==========================================================
//  <copyright file="StatementsResult.cs" company="iWork Technologies">
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

using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TinCan.Standard.Json;

#endregion

namespace TinCan.Standard
{
    /// <summary>
    /// Class StatementsResult.
    /// </summary>
    public class StatementsResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatementsResult"/> class.
        /// </summary>
        public StatementsResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementsResult"/> class.
        /// </summary>
        /// <param name="str">The string.</param>
        public StatementsResult(string str) : this(new StringOfJSON(str))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementsResult"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public StatementsResult(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementsResult"/> class.
        /// </summary>
        /// <param name="statements">The statements.</param>
        public StatementsResult(List<Statement> statements)
        {
            Statements = statements;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementsResult"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public StatementsResult(JObject jobj)
        {
            if (jobj["statements"] != null)
            {
                Statements = new List<Statement>();
                foreach (var item in jobj.Value<JArray>("statements"))
                {
                    Statements.Add(new Statement((JObject) item));
                }
            }
            if (jobj["more"] != null)
            {
                More = jobj.Value<string>("more");
            }
        }

        /// <summary>
        /// Gets or sets the statements.
        /// </summary>
        /// <value>The statements.</value>
        public List<Statement> Statements { get; set; }
        /// <summary>
        /// Gets or sets the more.
        /// </summary>
        /// <value>The more.</value>
        public string More { get; set; }
    }
}