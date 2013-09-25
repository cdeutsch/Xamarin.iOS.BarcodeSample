using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Xamarin.iOS.BarcodeSample
{
	public partial class Xamarin_iOS_BarcodeSampleViewController : UIViewController
	{
		public Xamarin_iOS_BarcodeSampleViewController () : base ("Xamarin_iOS_BarcodeSampleViewController", null)
		{
		}

		public UIWebView MainWebView {
			get {
				return this.webView;
			}
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

