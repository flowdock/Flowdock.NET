# Flowdock.NET

A Flowdock client for Windows Phone 8

**Note:** This is a Rally employee hackathon project, no promises anything will ever come of this just yet!

![screenshot](https://raw.github.com/RallySoftware/Flowdock.NET/master/screenshot.png)

# To Build

Clone the repo locally as usual, then ...

## Get Visual Studio

You need [Visual Studio 2012 Express for Windows Phone](https://www.microsoft.com/visualstudio/eng/downloads), this is a 1.6GB download, and Microsoft's servers can be very slow at times. So if you want, grab it from the Rally corp network at `groups/Engineering/Installers/Visual Studio 2012/`

## Update nuget packages

The nuget packages are not checking into git. There are a few steps needed to get the packages.

1. Go to Tools > Options > Package Manager > General
  * Check "Allow NuGet to download missing packages during build"
2. In Solution Explorer, right click the solution and click "Enable NuGet Package Restore"
3. Open the NuGet package manager: right click solution and choose "Manage NuGet Packages for Solution..."
  * In here, select to restore missing packages

See [this StackOverflow answer](http://stackoverflow.com/a/11847457/194940) for more details and screenshots. Once all set, you should have these nuget packages installed (as of time of this writing):

* BindableApplicationBar
* MoqaLate
* NUnit.Runners
* NUnit
* RestSharp

## Add DebugLoginInfo.cs

Logging into Flowdock everytime you test out the app is very painful, *especially* on the Windows Phone emulator (you have to use your mouse to click the keyboard keys! oi!). To get around this, in `DEBUG` mode, you will need `DebugLoginInfo.cs` added. This file is in `.gitignore` and so you need to add it manually on your box. Go to Flockdock > Settings and add the class

````
#if DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Settings {
	public static class DebugLoginInfo {
		public static string Username {
			get {
				return "your flowdock username";
			}
		}

		public static string Password {
			get {
				return "your flowdock password";
			}
		}
	}
}
#endif
````

With this in place, the login screen will be prefilled in with your credentials, just click "Log In" and go.

## Run the App

Press F5 or the `Emulator WVGA 512MB` button to run the app. The first time you launch the phone simulator it takes a good while to boot up. You should eventually see the Flowdock login page for the app.

## Rerunning the App

The key is don't close the simulator. Instead stop debugging in Visual Studio (Shift+F5). Then the next time you debug the app will load much quicker.

## Windows Phone Simulator Network Woes

The simulator can be pretty flaky at obtaining a network connection. If you fail to log into the app, this is most likely the problem (but also check to see if Flowdock might be down). If this is the case, you can get the simulator to regain a DHCP lease by turning off your host computer's Wifi and then turning it back on again. I also sometimes find that launching IE in the simulator and refreshing the page until it gains network access again to work too.

# Testing with MoqaLate

The version of the .NET runtime that is used with Windows Phone 8 does not have `System.Reflection.Emit`. This is the magic that makes most mocking libraries like Rhino Mocks or Moq work. This is a major blow to testing phone apps, as there are no mainstream, mature mocking frameworks available.

Jason Roberts created [MoqaLate](http://moqalate.codeplex.com/) as a solution to this problem. It's a bit primative, but it does work and is very helpful for testing (thanks Jason!). Here is [his blog post](http://dontcodetired.com/blog/post/Mocking-Framework-for-Windows-Store-apps-(and-Windows-Phone).aspx) going over how to use MoqaLate.  
  
 Also, here is a quick rundown on it:

 1. In the Flowdock and Flowdock.Client projects, add a post build step (Right click project > Properties > Build Events > Edit Post-Build... ). It should be:
    `$(ProjectDir)\MoqaLateCommandLine\MoqaLate.exe "$(SolutionDir)$(ProjectName)" "$(SolutionDir)Flowdock.UnitTests\Mocks"`

 2. Build the solution
 3. You should find in Flowdock.UnitTests/Mocks folder, a bunch of files named `[Something]MoqaLate.cs`. These are classes generated against all the interfaces found in the Flowdock and Flowdock.Client projects. Use these implementations as mocks in the Flowdock unit tests. See the existing tests for examples.

 ## One important note about MoqaLate

 MoqaLate assumes that your code is written in the "opening brace gets its own line" style, ie, like this:

````
public void Foo()
{
     // ...
}
````

instead of this:

````
public void Foo() {
	//...
}
````

That means **interfaces must be written in the first style** Look at any interface in the solution and you will see that

````
	public interface IMessageBoxService
	{
		void ShowUsersMessageBox(IEnumerable<User> users);
	}
````

Since Rally generally prefers the other style (and I do too!), interfaces are the only files that look like this.

