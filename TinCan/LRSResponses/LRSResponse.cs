#region License and Warranty Information

// ==========================================================
//  <copyright file="LRSResponse.cs" company="iWork Technologies">
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
using System.Text;

#endregion

namespace TinCan.LRSResponses
{
    //
    // this isn't abstract because some responses for an LRS won't have content
    // so in those cases we can get by just returning this base response
    //
    /// <summary>
    /// Class LRSResponse.
    /// </summary>
    public class LRSResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LRSResponse"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success { get; set; }
        /// <summary>
        /// Gets or sets the HTTP exception.
        /// </summary>
        /// <value>The HTTP exception.</value>
        public Exception HttpException { get; set; }
        /// <summary>
        /// Gets or sets the error MSG.
        /// </summary>
        /// <value>The error MSG.</value>
        public string ErrMsg { get; set; }

        /// <summary>
        /// Sets the error MSG from bytes.
        /// </summary>
        /// <param name="content">The content.</param>
        public void SetErrMsgFromBytes(byte[] content)
        {
            ErrMsg = Encoding.UTF8.GetString(content);
        }
    }
}