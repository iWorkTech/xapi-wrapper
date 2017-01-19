#region License and Warranty Information

// ==========================================================
//  <copyright file="StatementsQueryResultFormat.cs" company="iWork Technologies">
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

namespace TinCan.Standard
{
    /// <summary>
    /// Class StatementsQueryResultFormat. This class cannot be inherited.
    /// </summary>
    public sealed class StatementsQueryResultFormat
    {
        /// <summary>
        /// The ids
        /// </summary>
        public static readonly StatementsQueryResultFormat IDS = new StatementsQueryResultFormat("ids");
        /// <summary>
        /// The exact
        /// </summary>
        public static readonly StatementsQueryResultFormat EXACT = new StatementsQueryResultFormat("exact");
        /// <summary>
        /// The canonical
        /// </summary>
        public static readonly StatementsQueryResultFormat CANONICAL = new StatementsQueryResultFormat("canonical");

        /// <summary>
        /// The text
        /// </summary>
        private readonly string _text;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementsQueryResultFormat"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        private StatementsQueryResultFormat(string value)
        {
            _text = value;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return _text;
        }
    }
}