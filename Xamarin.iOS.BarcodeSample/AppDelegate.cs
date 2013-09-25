using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using cdeutsch;

namespace Xamarin.iOS.BarcodeSample
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		Xamarin_iOS_BarcodeSampleViewController viewController;
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			JsBridge.EnableJsBridge();

			window = new UIWindow (UIScreen.MainScreen.Bounds);

			// get useragent
			UIWebView agentWebView = new UIWebView ();
			var userAgent = agentWebView.EvaluateJavascript ("navigator.userAgent");
			agentWebView = null;
			userAgent += " XamarinBarcodeSampleApp";

			// set default useragent
			NSDictionary dictionary = NSDictionary.FromObjectAndKey(NSObject.FromObject(userAgent), NSObject.FromObject("UserAgent"));
			NSUserDefaults.StandardUserDefaults.RegisterDefaults(dictionary);

			viewController = new Xamarin_iOS_BarcodeSampleViewController ();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();

			viewController.MainWebView.LoadRequest(new NSUrlRequest(new NSUrl("http://xamarinbarcodesample.apphb.com/")));

			// listen for the event triggered by the browser.
			viewController.MainWebView.AddEventListener( "scanBarcode", delegate(FireEventData arg) {

				// show a native action sheet
				BeginInvokeOnMainThread (delegate { 
					//NOTE: On Android you MUST pass a Context into the Constructor!
					var scanningOptions = new ZXing.Mobile.MobileBarcodeScanningOptions();
					scanningOptions.PossibleFormats = new List<ZXing.BarcodeFormat>() { 
						ZXing.BarcodeFormat.All_1D
					};
					scanningOptions.TryInverted = true;
					scanningOptions.TryHarder = true;
					scanningOptions.AutoRotate = false;
					var scanner = new ZXing.Mobile.MobileBarcodeScanner();
					scanner.TopText = "Hold camera up to barcode to scan";
					scanner.BottomText = "Barcode will automatically scan";
					scanner.Scan(scanningOptions).ContinueWith(t => {   
						if (t.Result != null) {
							//Console.WriteLine("Scanned Barcode: " + t.Result.Text);

							// pass barcode back to browser.
							viewController.MainWebView.FireEvent( "scanComplete", new {
								code = t.Result.Text
							});
						}
					});
				});

			});
			
			return true;
		}
	}
}

