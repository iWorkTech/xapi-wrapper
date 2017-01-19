#region License and Warranty Information

// ==========================================================
//  <copyright file="JsonModel.cs" company="iWork Technologies">
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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace TinCan.Json
{
    /// <summary>
    /// Class JsonModel.
    /// </summary>
    /// <seealso cref="TinCan.Json.IJsonModel" />
    public abstract class JsonModel : IJsonModel
    {
        // TODO: rename methods to ToJObject and ToJSON
        /// <summary>
        /// To the j object.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>JObject.</returns>
        public abstract JObject ToJObject(TCAPIVersion version);

        /// <summary>
        /// To the j object.
        /// </summary>
        /// <returns>JObject.</returns>
        public JObject ToJObject()
        {
            return ToJObject(TCAPIVersion.Latest());
        }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="pretty">if set to <c>true</c> [pretty].</param>
        /// <returns>System.String.</returns>
        public string ToJSON(TCAPIVersion version, bool pretty = false)
        {
            var formatting = Formatting.None;
            if (pretty)
            {
                formatting = Formatting.Indented;
            }

            return JsonConvert.SerializeObject(ToJObject(version), formatting);
        }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="pretty">if set to <c>true</c> [pretty].</param>
        /// <returns>System.String.</returns>
        public string ToJSON(bool pretty = false)
        {
            return ToJSON(TCAPIVersion.Latest(), pretty);
        }
    }
}