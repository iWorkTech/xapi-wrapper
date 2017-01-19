#region License and Warranty Information

// ==========================================================
//  <copyright file="Activity.cs" company="iWork Technologies">
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
using TinCan.Standard.Json;

#endregion

namespace TinCan.Standard
{
    /// <summary>
    /// Class Activity.
    /// </summary>
    /// <seealso cref="JsonModel" />
    /// <seealso cref="IStatementTarget" />
    public class Activity : JsonModel, IStatementTarget
    {
        /// <summary>
        /// The object type
        /// </summary>
        public static readonly string OBJECT_TYPE = "Activity";

        /// <summary>
        /// The identifier
        /// </summary>
        private string _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Activity"/> class.
        /// </summary>
        public Activity()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Activity"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public Activity(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Activity"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public Activity(JObject jobj)
        {
            if (jobj["id"] != null)
            {
                var idFromJSON = jobj.Value<string>("id");
                var uri = new Uri(idFromJSON);
                ID = idFromJSON;
            }
            if (jobj["definition"] != null)
            {
                Definition = (ActivityDefinition) jobj.Value<JObject>("definition");
            }
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string ID
        {
            get { return _id; }
            set
            {
                var uri = new Uri(value);
                _id = value;
            }
        }

        /// <summary>
        /// Gets or sets the definition.
        /// </summary>
        /// <value>The definition.</value>
        public ActivityDefinition Definition { get; set; }

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
            var result = new JObject {{"objectType", ObjectType}};

            if (ID != null)
            {
                result.Add("id", ID);
            }
            if (Definition != null)
            {
                result.Add("definition", Definition.ToJObject(version));
            }

            return result;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="JObject"/> to <see cref="Activity"/>.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Activity(JObject jobj)
        {
            return new Activity(jobj);
        }
    }
}