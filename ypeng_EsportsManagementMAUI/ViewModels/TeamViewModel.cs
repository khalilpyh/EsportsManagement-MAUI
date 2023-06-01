/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ypeng_EsportsManagementMAUI.Data;
using ypeng_EsportsManagementMAUI.Models;
using ypeng_EsportsManagementMAUI.Services;
using ypeng_EsportsManagementMAUI.Views;

namespace ypeng_EsportsManagementMAUI.ViewModels
{
	[QueryProperty(nameof(Game), "Game")]  //wire up the navigation
	public partial class TeamViewModel : BaseViewModel
	{
		[ObservableProperty]
		private Game game;      //track the game object been passed from the main page

		[ObservableProperty]
		private bool isRefreshing;

		//declare the repository
		private readonly TeamRepository teamRepository;

		//declare a collection to track team data
		public ObservableCollection<Team> Teams { get; private set; } = new ObservableCollection<Team>();

		public TeamViewModel()
        {
			//initialize the repository
			teamRepository = new TeamRepository();
		}

		[RelayCommand]
		public async Task LoadTeams()   //retrieve all team records
		{
			if (this.IsLoading)
				return;

			try
			{
				IsLoading = true;
				var teams = await teamRepository.GetTeamsByGame(Game.ID);

				//make sure the team collection is empty
				Teams.Clear();

				foreach (var team in teams)
				{
					Teams.Add(team);
				}
			}
			catch (ApiException apiEx)
			{
				var errorString = new StringBuilder();
				errorString.AppendLine("Errors:");
				foreach (var error in apiEx.Errors)
				{
					errorString.AppendLine("-" + error);
				}
				await DisplayAlert("Failed to load the list of Teams:", errorString.ToString(), "Ok");
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					if (ex.GetBaseException().Message.Contains("connection with the server"))
					{

						await DisplayAlert("Error", "No connection with the server. Please try again.", "Ok");
					}
					else
					{
						await DisplayAlert("Error", "Something went wrong, please try again later.", "Ok");
					}
				}
				else
				{
					if (ex.Message.Contains("NameResolutionFailure"))
					{
						await DisplayAlert("Internet Access Error ", "Cannot resolve the Uri: " + Helper.DBUri.ToString(), "Ok");
					}
					else
					{
						await DisplayAlert("General Error ", ex.Message, "Ok");
					}
				}
			}
			finally
			{
				IsLoading = false;
				IsRefreshing = false;
			}
		}

		[RelayCommand]
		private async Task NavToPlayerPage(Team team)
		{
			if (team == null)
				await DisplayAlert("Error ", "Failed to retreive this Team.", "Ok");

			await Shell.Current.GoToAsync(nameof(PlayerPage), true, new Dictionary<string, object>
			{
				{ nameof(Team), team }
			});
		}

		private async Task DisplayAlert(string title, string message, string cancelText)
		{
			await App.Current.MainPage.DisplayAlert(title, message, cancelText);
		}
	}
}
