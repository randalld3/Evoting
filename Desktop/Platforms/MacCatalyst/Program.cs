using ObjCRuntime;
using UIKit;

namespace Desktop;

public class Program
{
	// This is the main entry point of the application.
	static void Main(string[] args)
	{
		// Rider on macOS doesn't work otherwise, so go figure.
		Thread.Sleep(5000);	
		// if you want to use a different Application Delegate class from "AppDelegate"
		// you can specify it here.
		UIApplication.Main(args, null, typeof(AppDelegate));
	}
}
