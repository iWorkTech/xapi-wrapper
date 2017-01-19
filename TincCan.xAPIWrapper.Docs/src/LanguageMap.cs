#region License and Warranty Information

// ==========================================================
//  <copyright file="LanguageMap.cs" company="iWork Technologies">
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
    /// Class LanguageMap.
    /// </summary>
    /// <seealso cref="JsonModel" />
    public class LanguageMap : JsonModel
    {
        /// <summary>
        /// The map
        /// </summary>
        private readonly Dictionary<string, string> _map;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageMap"/> class.
        /// </summary>
        public LanguageMap()
        {
            _map = new Dictionary<string, string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageMap"/> class.
        /// </summary>
        /// <param name="map">The map.</param>
        public LanguageMap(Dictionary<string, string> map)
        {
            _map = map;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageMap"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public LanguageMap(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageMap"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public LanguageMap(JObject jobj) : this()
        {
            foreach (var entry in jobj)
            {
                _map.Add(entry.Key, (string) entry.Value);
            }
        }

        /// <summary>
        /// To the j object.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>JObject.</returns>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();
            foreach (var entry in _map)
            {
                result.Add(entry.Key, entry.Value);
            }

            return result;
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns><c>true</c> if this instance is empty; otherwise, <c>false</c>.</returns>
        public bool IsEmpty()
        {
            return _map.Count <= 0;
        }

        /// <summary>
        /// Adds the specified language.
        /// </summary>
        /// <param name="lang">The language.</param>
        /// <param name="value">The value.</param>
        public void Add(string lang, string value)
        {
            _map.Add(lang, value);
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="JObject"/> to <see cref="LanguageMap"/>.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator LanguageMap(JObject jobj)
        {
            return new LanguageMap(jobj);
        }
    }
}