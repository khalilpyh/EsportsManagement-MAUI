/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using ypeng_EsportsManagementMAUI.ViewModels;

namespace ypeng_EsportsManagementMAUI;

public partial class MainPage : ContentPage
{
	private readonly GameViewModel gameViewModel;

	public MainPage(GameViewModel gameViewModel)
	{
		InitializeComponent();
		BindingContext = gameViewModel;

		//get a local copy of game viewmodel
		this.gameViewModel = gameViewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		//populate the game data on pageload
		await gameViewModel.LoadGames();
	}
}

