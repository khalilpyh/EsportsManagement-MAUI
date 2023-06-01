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
using ypeng_EsportsManagementMAUI.Data;
using ypeng_EsportsManagementMAUI.Models;
using ypeng_EsportsManagementMAUI.Services;
using ypeng_EsportsManagementMAUI.Views;

namespace ypeng_EsportsManagementMAUI.ViewModels
{
	public partial class GameViewModel : BaseViewModel
	{
		//declare the repository
		private readonly GameRepository gameRepository;

		[ObservableProperty]
		private bool isRefreshing;

		//declare a collection to track game data
		public ObservableCollection<Game> Games { get; private set; } = new ObservableCollection<Game>();
	
		public GameViewModel() {
			//initialize the repository
			gameRepository = new GameRepository();
		}

		[RelayCommand]
		public async Task LoadGames()	//retrieve all game records
		{
			if (this.IsLoading)
				return;

			try
			{
				IsLoading = true;
				var games = await gameRepository.GetGames();

				//make sure the games collection is empty
				Games.Clear();

				foreach (var game in games)
				{
					Games.Add(game);
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
				await DisplayAlert("Failed to load the list of Games:", errorString.ToString(), "Ok");
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
		private async Task NavToTeamPage(Game game)
		{
			if(game == null)
				await DisplayAlert("Error ", "Failed to retreive this Game.", "Ok");

			await Shell.Current.GoToAsync(nameof(TeamPage), true, new Dictionary<string, object>
			{
				{ nameof(Game), game }
			});
		}

		private async Task DisplayAlert(string title, string message, string cancelText)
		{
			await App.Current.MainPage.DisplayAlert(title, message, cancelText);
		}
	}
}
