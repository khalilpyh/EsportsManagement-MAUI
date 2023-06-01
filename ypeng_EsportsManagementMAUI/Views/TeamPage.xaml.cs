/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using ypeng_EsportsManagementMAUI.ViewModels;

namespace ypeng_EsportsManagementMAUI.Views;

public partial class TeamPage : ContentPage
{
	private readonly TeamViewModel teamViewModel;

	public TeamPage(TeamViewModel teamViewModel)
	{
		InitializeComponent();
		BindingContext = teamViewModel;

		//get a local copy of team viewmodel
		this.teamViewModel = teamViewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		//populate the team data on pageload
		await teamViewModel.LoadTeams();
	}
}