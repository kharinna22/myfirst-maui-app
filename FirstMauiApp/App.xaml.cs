﻿using FirstMauiApp.Data;

namespace FirstMauiApp;

public partial class App : Application
{
	public static RecipesDatabase Database { get; } = new ();

	public App()
	{
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHFqVkFrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRcQl5gSX9SdENgWXpfcH0=;Mgo+DSMBPh8sVXJ1S0d+X1ZPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSX1QdkRiW3ldcHFXQmk=;ORg4AjUWIQA/Gnt2VFhhQlJDfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5QdUViWH1Yc3RQRWlV;MTU2MDIxMUAzMjMxMmUzMTJlMzMzN2hmREF0MXVBRzNVN1lYRHRvS0RZY3Y3UGd3UUtvaXpVUE4vbFV0MUVGcWM9;MTU2MDIxMkAzMjMxMmUzMTJlMzMzN0hFWE5IQ2ltMzFXT2hWTlFpYXo0NjBWdXp5cWpHU3ZJVEFDRHNVUk5QMFU9;NRAiBiAaIQQuGjN/V0d+XU9HcVRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31TdUZjWH5cdndVQGFbWA==;MTU2MDIxNEAzMjMxMmUzMTJlMzMzN1VBYVRCcldGbENVNXlnTkI5Y2REZWdqSjlZeWxhbkoyUFdMSWVTejNLMWc9;MTU2MDIxNUAzMjMxMmUzMTJlMzMzN0s0cjBLQ0UvQUZ0RnZFd05SNUhKZEd3VitiVVFNNWFRbXV4T29FUnFyVmc9;Mgo+DSMBMAY9C3t2VFhhQlJDfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5QdUViWH1Yc3RSRmZV;MTU2MDIxN0AzMjMxMmUzMTJlMzMzN1ZkSnVCMkNGcUlHYVNBMlFadFlOMnBwNnkraFluKzZXNmRIQklQTFJWU0k9;MTU2MDIxOEAzMjMxMmUzMTJlMzMzN0pDNk1wc01XTEQrVjEzZ3ZmVUdWM0kxbGFlS3oxdE5LQzBVTUhwS0llSUE9;MTU2MDIxOUAzMjMxMmUzMTJlMzMzN1VBYVRCcldGbENVNXlnTkI5Y2REZWdqSjlZeWxhbkoyUFdMSWVTejNLMWc9");
		InitializeComponent();

		MainPage = new AppShell();

        Database.Init().Wait(4500);
    }
}
