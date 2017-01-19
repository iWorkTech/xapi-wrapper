#region License and Warranty Information

// ==========================================================
//  <copyright file="Score.cs" company="iWork Technologies">
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
using TinCan.Standard.Json;

#endregion

namespace TinCan.Standard
{
    /// <summary>
    /// Class Score.
    /// </summary>
    /// <seealso cref="JsonModel" />
    public class Score : JsonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Score" /> class.
        /// </summary>
        public Score()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Score"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public Score(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Score"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public Score(JObject jobj)
        {
            if (jobj["scaled"] != null)
            {
                Scaled = jobj.Value<double>("scaled");
            }
            if (jobj["raw"] != null)
            {
                Raw = jobj.Value<double>("raw");
            }
            if (jobj["min"] != null)
            {
                Min = jobj.Value<double>("min");
            }
            if (jobj["max"] != null)
            {
                Max = jobj.Value<double>("max");
            }
        }

        /// <summary>
        /// Gets or sets the scaled.
        /// </summary>
        /// <value>The scaled.</value>
        public double? Scaled { get; set; }
        /// <summary>
        /// Gets or sets the raw.
        /// </summary>
        /// <value>The raw.</value>
        public double? Raw { get; set; }
        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public double? Min { get; set; }
        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public double? Max { get; set; }

        /// <summary>
        /// To the j object.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>JObject.</returns>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();

            if (Scaled != null)
            {
                result.Add("scaled", Scaled);
            }
            if (Raw != null)
            {
                result.Add("raw", Raw);
            }
            if (Min != null)
            {
                result.Add("min", Min);
            }
            if (Max != null)
            {
                result.Add("max", Max);
            }

            return result;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="JObject"/> to <see cref="Score"/>.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Score(JObject jobj)
        {
            return new Score(jobj);
        }
    }
}