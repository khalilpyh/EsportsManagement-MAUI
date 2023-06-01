/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ypeng_EsportsManagementMAUI.Data;
using ypeng_EsportsManagementMAUI.Models;
using ypeng_EsportsManagementMAUI.Services;
using ypeng_EsportsManagementMAUI.Views;

namespace ypeng_EsportsManagementMAUI.ViewModels
{
	[QueryProperty(nameof(Player), "Player")]  //wire up the navigation
	public partial class PlayerDetailViewModel : BaseViewModel
    {
		[ObservableProperty]
		private Player player;      //track the player object been passed from the player page

		//declare the repository
		private readonly PlayerRepository playerRepository;

		[ObservableProperty]
		private bool inputEnable;
		[ObservableProperty]
		private bool editEnable;
		[ObservableProperty]
		private bool updateEnable;

		public PlayerDetailViewModel()
        {
			//initialize the repository
			playerRepository = new PlayerRepository();
		}

		[RelayCommand]
		public async Task LoadPage()
		{
			//disable all input controls
			InputEnable = false;
			//display edit button
			EditEnable = true;
			//hide update, cancel button
			UpdateEnable = false;
		}

		[RelayCommand]
		private async Task SetEditMode()
		{
			//enable all input controls
			InputEnable = true;
			//hide edit button
			EditEnable = false;
			//display update, cancel button
			UpdateEnable = true;
		}

		[RelayCommand]
		private async Task UpdatePlayer()
		{
			try
			{
				//update the player
				await playerRepository.UpdatePlayer(Player);

				await DisplayAlert("Info", "Player information is updated!", "Ok");

				//reset controls
				await LoadPage();
			}
			catch (ApiException apiEx)
			{
				var errorString = new StringBuilder();
				errorString.AppendLine("Errors:");
				foreach (var error in apiEx.Errors)
				{
					errorString.AppendLine("-" + error);
				}
				await DisplayAlert("Problem updating the Player:", errorString.ToString(), "Ok");
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
		private async Task RefreshPage()
		{
			await Shell.Current.GoToAsync(nameof(PlayerDetailPage), true, new Dictionary<string, object>
			{
				{ nameof(Player), Player }
			});
		}

		private async Task DisplayAlert(string title, string message, string cancelText)
		{
			await App.Current.MainPage.DisplayAlert(title, message, cancelText);
		}
	}
}
