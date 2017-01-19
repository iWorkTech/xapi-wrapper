#region License and Warranty Information

// ==========================================================
//  <copyright file="ViewController.cs" company="iWork Technologies">
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
using TinCan.xAPIWrapper;
using UIKit;

#endregion

namespace xAPIWrapper.iOS
{
    public partial class ViewController : UIViewController
    {
        int _count = 1;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            Button.AccessibilityIdentifier = "myButton";
            Button.TouchUpInside += async delegate
            {
                var apiWrapper = new APIWrapper(string.Empty, string.Empty, string.Empty);
                var statement = apiWrapper.PrepareStatement("test@ald.net", "experienced", "Activity");
                var task = await apiWrapper.SendStatement(statement);
                var title = $"sending {_count} statement!";
                if (task.Success)
                {
                    title = $"{_count++} statements sent!";
                }

                Button.SetTitle(title, UIControlState.Normal);

            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}