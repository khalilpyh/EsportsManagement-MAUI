/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using ypeng_EsportsManagementMAUI.ViewModels;

namespace ypeng_EsportsManagementMAUI.Views;

public partial class PlayerPage : ContentPage
{
	private readonly PlayerViewModel playerViewModel;

	public PlayerPage(PlayerViewModel playerViewModel)
	{
		InitializeComponent();
		BindingContext = playerViewModel;

		//get a local copy of team viewmodel
		this.playerViewModel = playerViewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		//populate the player data on pageload
		await playerViewModel.LoadPlayers();
	}
}