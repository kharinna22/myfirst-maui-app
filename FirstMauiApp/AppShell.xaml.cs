﻿namespace FirstMauiApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(Views.NotePage), typeof(Views.NotePage));
		Routing.RegisterRoute(nameof(Views.GuessTheNumberPage), typeof(Views.GuessTheNumberPage));
	}
}
