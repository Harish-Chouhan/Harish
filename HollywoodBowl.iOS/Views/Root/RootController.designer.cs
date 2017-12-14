// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace HollywoodBowl.iOS.Views.Root
{
    [Register ("RootController")]
    partial class RootController
    {
        [Outlet]
        public UIKit.UIView ContainerView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView TabBarContainer { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ContainerView != null) {
                ContainerView.Dispose ();
                ContainerView = null;
            }

            if (TabBarContainer != null) {
                TabBarContainer.Dispose ();
                TabBarContainer = null;
            }
        }
    }
}