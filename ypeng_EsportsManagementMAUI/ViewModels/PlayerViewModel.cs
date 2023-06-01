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
using System.Text;
using System.Threading.Tasks;
using ypeng_EsportsManagementMAUI.Data;
using ypeng_EsportsManagementMAUI.Models;
using ypeng_EsportsManagementMAUI.Services;
using ypeng_EsportsManagementMAUI.Views;

namespace ypeng_EsportsManagementMAUI.ViewModels
{
	[QueryProperty(nameof(Team), "Team")]  //wire up the navigation
	public partial class PlayerViewModel : BaseViewModel
	{
		[ObservableProperty]
		private Team team;      //track the team object been passed from the team page

		[ObservableProperty]
		private bool isRefreshing;
		[ObservableProperty]
		private bool addFormVisible;
		[ObservableProperty]
		private bool addButtonEnable;

		[ObservableProperty]        //for adding new player
		private string firstName;
		[ObservableProperty]
		private string lastName;
		[ObservableProperty]
		private string nickName;
		[ObservableProperty]
		private DateTime dob;
		[ObservableProperty]
		private string position;
		[ObservableProperty]
		private DateTime joinDate;

		//declare the repository
		private readonly PlayerRepository playerRepository;

		//declare a collection to track player data
		public ObservableCollection<Player> Players { get; private set; } = new ObservableCollection<Player>();

		public PlayerViewModel()
		{
			//initialize the repository
			playerRepository = new PlayerRepository();
		}

		[RelayCommand]
		public async Task LoadPlayers()   //retrieve all team records
		{
			AddFormVisible = false; //disable add player form
			AddButtonEnable = true; //show add player button

			//pre-set date to datepickers
			this.Dob = DateTime.Today;
			this.JoinDate = DateTime.Today;

			if (this.IsLoading)
				return;

			try
			{
				IsLoading = true;
				var players = await playerRepository.GetPlayersByTeam(Team.ID);

				//make sure the player collection is empty
				Players.Clear();

				foreach (var player in players)
				{
					Players.Add(player);
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
		private async Task AddPlayer()
		{
			try
			{
				Player playerToAdd = new Player
				{
					FirstName = this.FirstName,
					LastName = this.LastName,
					Nickname = this.NickName,
					DOB = this.Dob,
					Position = this.Position,
					JoinDate = this.JoinDate,
					TeamID = this.Team.ID       //assign this player to the team displays on page
				};
				//add the player
				await playerRepository.AddPlayer(playerToAdd);
				//reload the player records
				await LoadPlayers();
				//reset and hide the form
				await ResetForm();
			}
			catch (ApiException apiEx)
			{
				var errorString = new StringBuilder();
				errorString.AppendLine("Errors:");
				foreach (var error in apiEx.Errors)
				{
					errorString.AppendLine("-" + error);
				}
				await DisplayAlert("Problem adding the Player:", errorString.ToString(), "Ok");
			}
			catch (Exception ex)
			{
				if (ex.GetBaseException().Message.Contains("connection with the server"))
				{
					await DisplayAlert("Error", "No connection with the server.", "Ok");
				}
				else
				{
					await DisplayAlert("Error", "Could not complete operation.", "Ok");
				}
			}
		}

		[RelayCommand]
		private async Task DisplayAddForm()
		{
			AddButtonEnable = false;  //hided add player button once clicked
			AddFormVisible = true;  //show the add player form
		}

		[RelayCommand]
		private async Task ResetForm()
		{
			//reset input controls
			this.FirstName = String.Empty;
			this.LastName = String.Empty;
			this.NickName = String.Empty;
			this.Dob = DateTime.Today;
			this.Position = String.Empty;
			this.JoinDate = DateTime.Today;
			//hide add player form and show add player button
			AddFormVisible = false;
			AddButtonEnable = true;
		}


		[RelayCommand]
		private async Task DeletePlayer(Player player)
		{
			if (player == null)
				await DisplayAlert("Error ", "Failed to delete this Player.", "Ok");

			var deleteComfirm = await Shell.Current.DisplayAlert("Confirm Delete", "Are you sure you want to delete " + player.FullName + "?", "Yes", "No");

			if (deleteComfirm)
			{
				try
				{
					//delete the player been passed in
					await playerRepository.DeletePlayer(player);
					//reload the player records
					await LoadPlayers();
				}
				catch (ApiException apiEx)
				{
					var errorString = new StringBuilder();
					errorString.AppendLine("Errors:");
					foreach (var error in apiEx.Errors)
					{
						errorString.AppendLine("-" + error);
					}
					await DisplayAlert("Problem deleting the Player:", errorString.ToString(), "Ok");
				}
				catch (Exception ex)
				{
					if (ex.GetBaseException().Message.Contains("connection with the server"))
					{
						await DisplayAlert("Error", "No connection with the server.", "Ok");
					}
					else
					{
						await DisplayAlert("Error", "Could not complete operation.", "Ok");
					}
				}
			}
		}

		[RelayCommand]
		private async Task NavToPlayerDetail(Player player)
		{
			if (player == null)
				await DisplayAlert("Error ", "Failed to retreive this Player.", "Ok");

			await Shell.Current.GoToAsync(nameof(PlayerDetailPage), true, new Dictionary<string, object>
			{
				{ nameof(Player), player }
			});
		}

		private async Task DisplayAlert(string title, string message, string cancelText)
		{
			await App.Current.MainPage.DisplayAlert(title, message, cancelText);
		}
	}
}
