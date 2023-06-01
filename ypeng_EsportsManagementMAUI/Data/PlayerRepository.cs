/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ypeng_EsportsManagementMAUI.Models;
using ypeng_EsportsManagementMAUI.Services;

namespace ypeng_EsportsManagementMAUI.Data
{
	public class PlayerRepository : IPlayerRepository
	{
		readonly HttpClient client = new HttpClient();

		public PlayerRepository()
		{
			client.BaseAddress = Helper.DBUri;
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<Player> GetPlayer(int ID)
		{
			var response = await client.GetAsync($"api/Players/{ID}");

			if (response.IsSuccessStatusCode)
			{
				Player player = await response.Content.ReadAsAsync<Player>();
				return player;
			}
			else
			{
				var ex = Helper.CreateApiException(response);
				throw ex;
			}
		}

		public async Task<List<Player>> GetPlayers()
		{
			HttpResponseMessage response = await client.GetAsync("api/Players");

			if (response.IsSuccessStatusCode)
			{
				List<Player> players = await response.Content.ReadAsAsync<List<Player>>();
				return players;
			}
			else
			{
				var ex = Helper.CreateApiException(response);
				throw ex;
			}
		}

		public async Task<List<Player>> GetPlayersByTeam(int TeamID)
		{
			var response = await client.GetAsync($"api/Players/ByTeam/{TeamID}");

			if (response.IsSuccessStatusCode)
			{
				List<Player> players = await response.Content.ReadAsAsync<List<Player>>();
				return players;
			}
			else
			{
				var ex = Helper.CreateApiException(response);
				throw ex;
			}
		}

		public async Task AddPlayer(Player PlayerToAdd)
		{
			PlayerToAdd.Team = null;
			var response = await client.PostAsJsonAsync("/api/Players", PlayerToAdd);
			if (!response.IsSuccessStatusCode)
			{
				var ex = Helper.CreateApiException(response);
				throw ex;
			}
		}

		public async Task DeletePlayer(Player PlayerToDelete)
		{

			var response = await client.DeleteAsync($"/api/Players/{PlayerToDelete.ID}");
			if (!response.IsSuccessStatusCode)
			{
				var ex = Helper.CreateApiException(response);
				throw ex;
			}
		}

		public async Task UpdatePlayer(Player PlayerToUpdate)
		{
			PlayerToUpdate.Team = null;
			var response = await client.PutAsJsonAsync($"/api/Players/{PlayerToUpdate.ID}", PlayerToUpdate);
			if (!response.IsSuccessStatusCode)
			{
				var ex = Helper.CreateApiException(response);
				throw ex;
			}
		}
	}
}
