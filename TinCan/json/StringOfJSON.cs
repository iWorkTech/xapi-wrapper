#region License and Warranty Information

// ==========================================================
//  <copyright file="StringOfJSON.cs" company="iWork Technologies">
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

#endregion

namespace TinCan.Json
{
    /// <summary>
    /// Class StringOfJSON.
    /// </summary>
    public class StringOfJSON
    {
        /// <summary>
        /// The source
        /// </summary>
        private readonly string source;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringOfJSON"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public StringOfJSON(string json)
        {
            source = json;
        }

        /// <summary>
        /// To the j object.
        /// </summary>
        /// <returns>JObject.</returns>
        public JObject toJObject()
        {
            if (source == null)
            {
                return null;
            }

            return JObject.Parse(source);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return source;
        }
    }
}