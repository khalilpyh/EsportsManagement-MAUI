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
	public class GameRepository : IGameRepository
	{
		readonly HttpClient client = new HttpClient();

		public GameRepository()
		{
			client.BaseAddress = Helper.DBUri;
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<Game> GetGame(int ID)
		{
			var response = await client.GetAsync($"api/Games/{ID}");

			if (response.IsSuccessStatusCode)
			{
				Game game = await response.Content.ReadAsAsync<Game>();
				return game;
			}
			else
			{
				var ex = Helper.CreateApiException(response);
				throw ex;
			}
		}

		public async Task<List<Game>> GetGames()
		{
			var response = await client.GetAsync("api/Games");

			if (response.IsSuccessStatusCode)
			{
				List<Game> games = await response.Content.ReadAsAsync<List<Game>>();
				return games;
			}
			else
			{
				var ex = Helper.CreateApiException(response);
				throw ex;
			}
		}
	}
}
