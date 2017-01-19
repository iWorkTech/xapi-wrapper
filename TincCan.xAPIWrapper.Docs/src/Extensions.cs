#region License and Warranty Information

// ==========================================================
//  <copyright file="Extensions.cs" company="iWork Technologies">
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
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TinCan.Standard.Json;

#endregion

namespace TinCan.Standard
{
    /// <summary>
    /// Class Extensions.
    /// </summary>
    /// <seealso cref="JsonModel" />
    public class Extensions : JsonModel
    {
        /// <summary>
        /// The map
        /// </summary>
        private readonly Dictionary<Uri, JToken> _map;

        /// <summary>
        /// Initializes a new instance of the <see cref="Extensions"/> class.
        /// </summary>
        public Extensions()
        {
            _map = new Dictionary<Uri, JToken>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extensions"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public Extensions(JObject jobj) : this()
        {
            foreach (var item in jobj)
            {
                _map.Add(new Uri(item.Key), item.Value);
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
                result.Add(entry.Key.ToString(), entry.Value);
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
        /// Performs an explicit conversion from <see cref="JObject"/> to <see cref="Extensions"/>.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Extensions(JObject jobj)
        {
            return new Extensions(jobj);
        }
    }
}