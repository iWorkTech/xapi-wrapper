#region License and Warranty Information

// ==========================================================
//  <copyright file="About.cs" company="iWork Technologies">
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
using TinCan.Json;

#endregion

namespace TinCan
{
    /// <summary>
    /// Class About.
    /// </summary>
    /// <seealso cref="TinCan.Json.JsonModel" />
    public class About : JsonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="About"/> class.
        /// </summary>
        /// <param name="str">The string.</param>
        public About(string str) : this(new StringOfJSON(str))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="About"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public About(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="About"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public About(JObject jobj)
        {
            if (jobj["version"] != null)
            {
                Version = new List<TCAPIVersion>();
                foreach (string item in jobj.Value<JArray>("version"))
                {
                    Version.Add((TCAPIVersion) item);
                }
            }
            if (jobj["extensions"] != null)
            {
                Extensions = new Extensions(jobj.Value<JObject>("extensions"));
            }
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public List<TCAPIVersion> Version { get; set; }
        /// <summary>
        /// Gets or sets the extensions.
        /// </summary>
        /// <value>The extensions.</value>
        public Extensions Extensions { get; set; }

        /// <summary>
        /// To the j object.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>JObject.</returns>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();
            if (Version != null)
            {
                var versions = new JArray();
                foreach (var v in Version)
                {
                    versions.Add(v.ToString());
                }
                result.Add("version", versions);
            }

            if (Extensions != null && !Extensions.IsEmpty())
            {
                result.Add("extensions", Extensions.ToJObject(version));
            }

            return result;
        }
    }
}