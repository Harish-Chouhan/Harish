// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
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
			if (TabBarContainer != null) {
				TabBarContainer.Dispose ();
				TabBarContainer = null;
			}

			if (ContainerView != null) {
				ContainerView.Dispose ();
				ContainerView = null;
			}
		}
	}
}
