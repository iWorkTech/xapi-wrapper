#region License and Warranty Information

// ==========================================================
//  <copyright file="ContextActivities.cs" company="iWork Technologies">
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
using TinCan.Json;

#endregion

namespace TinCan
{
    /// <summary>
    /// Class ContextActivities.
    /// </summary>
    /// <seealso cref="TinCan.Json.JsonModel" />
    public class ContextActivities : JsonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContextActivities"/> class.
        /// </summary>
        public ContextActivities()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextActivities"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public ContextActivities(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextActivities"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public ContextActivities(JObject jobj)
        {
            if (jobj["parent"] != null)
            {
                Parent = new List<Activity>();
                foreach (var jToken in jobj["parent"])
                {
                    var jactivity = (JObject) jToken;
                    if (jactivity == null) throw new ArgumentNullException(nameof(jactivity));
                    Parent.Add((Activity) jactivity);
                }
            }
            if (jobj["grouping"] != null)
            {
                Grouping = new List<Activity>();
                foreach (var jToken in jobj["grouping"])
                {
                    var jactivity = (JObject) jToken;
                    if (jactivity == null) throw new ArgumentNullException(nameof(jactivity));
                    Grouping.Add((Activity) jactivity);
                }
            }
            if (jobj["category"] != null)
            {
                Category = new List<Activity>();
                foreach (var jToken in jobj["category"])
                {
                    var jactivity = (JObject) jToken;
                    if (jactivity == null) throw new ArgumentNullException(nameof(jactivity));
                    Category.Add((Activity) jactivity);
                }
            }
            if (jobj["other"] != null)
            {
                Other = new List<Activity>();
                foreach (var jToken in jobj["other"])
                {
                    var jactivity = (JObject) jToken;
                    if (jactivity == null) throw new ArgumentNullException(nameof(jactivity));
                    Other.Add((Activity) jactivity);
                }
            }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public List<Activity> Parent { get; set; }
        /// <summary>
        /// Gets or sets the grouping.
        /// </summary>
        /// <value>The grouping.</value>
        public List<Activity> Grouping { get; set; }
        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public List<Activity> Category { get; set; }
        /// <summary>
        /// Gets or sets the other.
        /// </summary>
        /// <value>The other.</value>
        public List<Activity> Other { get; set; }

        /// <summary>
        /// To the j object.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>JObject.</returns>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();

            if (Parent != null && Parent.Count > 0)
            {
                var jparent = new JArray();
                result.Add("parent", jparent);

                foreach (var activity in Parent)
                {
                    jparent.Add(activity.ToJObject(version));
                }
            }
            if (Grouping != null && Grouping.Count > 0)
            {
                var jgrouping = new JArray();
                result.Add("grouping", jgrouping);

                foreach (var activity in Grouping)
                {
                    jgrouping.Add(activity.ToJObject(version));
                }
            }
            if (Category != null && Category.Count > 0)
            {
                var jcategory = new JArray();
                result.Add("category", jcategory);

                foreach (var activity in Category)
                {
                    jcategory.Add(activity.ToJObject(version));
                }
            }
            if (Other != null && Other.Count > 0)
            {
                var jother = new JArray();
                result.Add("other", jother);

                foreach (var activity in Other)
                {
                    jother.Add(activity.ToJObject(version));
                }
            }

            return result;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="JObject"/> to <see cref="ContextActivities"/>.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator ContextActivities(JObject jobj)
        {
            return new ContextActivities(jobj);
        }
    }
}