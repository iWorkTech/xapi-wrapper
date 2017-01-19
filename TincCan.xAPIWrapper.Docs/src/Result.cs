#region License and Warranty Information

// ==========================================================
//  <copyright file="Result.cs" company="iWork Technologies">
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
    /// Class Result.
    /// </summary>
    /// <seealso cref="JsonModel" />
    public class Result : JsonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        public Result()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        public Result(StringOfJSON json) : this(json.toJObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        public Result(JObject jobj)
        {
            if (jobj["completion"] != null)
            {
                Completion = jobj.Value<bool>("completion");
            }
            if (jobj["success"] != null)
            {
                Success = jobj.Value<bool>("success");
            }
            if (jobj["response"] != null)
            {
                Response = jobj.Value<string>("response");
            }
            if (jobj["duration"] != null)
            {
                TimeSpan ts;
                TimeSpan.TryParse(jobj.Value<string>("duration"), out ts);
                Duration = ts;
            }
            if (jobj["score"] != null)
            {
                Score = (Score) jobj.Value<JObject>("score");
            }
            if (jobj["extensions"] != null)
            {
                Extensions = (Extensions) jobj.Value<JObject>("extensions");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Result"/> is completion.
        /// </summary>
        /// <value><c>null</c> if [completion] contains no value, <c>true</c> if [completion]; otherwise, <c>false</c>.</value>
        public bool? Completion { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Result"/> is success.
        /// </summary>
        /// <value><c>null</c> if [success] contains no value, <c>true</c> if [success]; otherwise, <c>false</c>.</value>
        public bool? Success { get; set; }
        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>The response.</value>
        public string Response { get; set; }
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public TimeSpan? Duration { get; set; }
        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>The score.</value>
        public Score Score { get; set; }
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

            if (Completion != null)
            {
                result.Add("completion", Completion);
            }
            if (Success != null)
            {
                result.Add("success", Success);
            }
            if (Response != null)
            {
                result.Add("response", Response);
            }
            if (Duration != null)
            {
                result.Add("duration", ((TimeSpan) Duration).ToString());
            }
            if (Score != null)
            {
                result.Add("score", Score.ToJObject(version));
            }
            if (Extensions != null)
            {
                result.Add("extensions", Extensions.ToJObject(version));
            }

            return result;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="JObject"/> to <see cref="Result"/>.
        /// </summary>
        /// <param name="jobj">The jobj.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Result(JObject jobj)
        {
            return new Result(jobj);
        }
    }
}