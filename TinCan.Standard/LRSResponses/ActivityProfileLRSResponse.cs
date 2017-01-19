#region License and Warranty Information

// ==========================================================
//  <copyright file="ActivityProfileLRSResponse.cs" company="iWork Technologies">
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

using TinCan.Standard.Documents;

#endregion

namespace TinCan.Standard.LRSResponses
{
    /// <summary>
    /// Class ActivityProfileLRSResponse.
    /// </summary>
    /// <seealso cref="LRSResponse" />
    public class ActivityProfileLRSResponse : LRSResponse
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public ActivityProfileDocument Content { set; get; }
    }
}