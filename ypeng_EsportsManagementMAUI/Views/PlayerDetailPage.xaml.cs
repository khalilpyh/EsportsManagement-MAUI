/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using ypeng_EsportsManagementMAUI.ViewModels;

namespace ypeng_EsportsManagementMAUI.Views;

public partial class PlayerDetailPage : ContentPage
{
	private readonly PlayerDetailViewModel playerDetailViewModel;

	public PlayerDetailPage(PlayerDetailViewModel playerDetailViewModel)
	{
		InitializeComponent();
		BindingContext = playerDetailViewModel;

		//get a local copy of team viewmodel
		this.playerDetailViewModel = playerDetailViewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		//set up the page on pageload
		await playerDetailViewModel.LoadPage();
	}
}