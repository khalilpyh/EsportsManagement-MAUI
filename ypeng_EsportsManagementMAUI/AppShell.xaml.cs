/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using ypeng_EsportsManagementMAUI.Views;

namespace ypeng_EsportsManagementMAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		//register the team page
		Routing.RegisterRoute(nameof(TeamPage), typeof(TeamPage));

		//register the player page
		Routing.RegisterRoute(nameof(PlayerPage), typeof(PlayerPage));

		//register the player detail page
		Routing.RegisterRoute(nameof(PlayerDetailPage), typeof(PlayerDetailPage));
	}
}
