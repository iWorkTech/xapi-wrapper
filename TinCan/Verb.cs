#region License and Warranty Information

// ==========================================================
//  <copyright file="Verb.cs" company="iWork Technologies">
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
    /// Class Verb.
    /// </summary>
    /// <seealso cref="TinCan.Json.JsonModel" />
    public class Verb : JsonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Verb"/> class.
        /// </summary>
        public Verb()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Verb"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public Verb(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Verb"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public Verb(JObject jobj)
        {
            if (jobj["id"] != null)
            {
                ID = new Uri(jobj.Value<string>("id"));
            }
            if (jobj["display"] != null)
            {
                Display = (LanguageMap) jobj.Value<JObject>("display");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Verb"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public Verb(Uri uri)
        {
            ID = uri;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Verb"/> class.
        /// </summary>
        /// <param name="str">The string.</param>
        public Verb(string str)
        {
            ID = new Uri(str);
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Uri ID { get; set; }
        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>The display.</value>
        public LanguageMap Display { get; set; }

        /// <summary>
        /// To the j object.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>JObject.</returns>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();
            if (ID != null)
            {
                result.Add("id", ID.ToString());
            }

            if (Display != null && !Display.IsEmpty())
            {
                result.Add("display", Display.ToJObject(version));
            }

            return result;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="JObject"/> to <see cref="Verb"/>.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Verb(JObject jobj)
        {
            return new Verb(jobj);
        }
    }
}