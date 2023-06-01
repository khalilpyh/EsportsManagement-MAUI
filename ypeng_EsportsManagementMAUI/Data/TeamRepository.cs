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
	public class TeamRepository : ITeamRepository
	{
		readonly HttpClient client = new HttpClient();

		public TeamRepository()
		{
			client.BaseAddress = Helper.DBUri;
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<Team> GetTeam(int ID)
		{
			var response = await client.GetAsync($"api/Teams/inc/{ID}");

			if (response.IsSuccessStatusCode)
			{
				Team team = await response.Content.ReadAsAsync<Team>();
				return team;
			}
			else
			{
				var ex = Helper.CreateApiException(response);
				throw ex;
			}
		}

		public async Task<List<Team>> GetTeams()
		{
			HttpResponseMessage response = await client.GetAsync("api/Teams/inc");

			if (response.IsSuccessStatusCode)
			{
				List<Team> teams = await response.Content.ReadAsAsync<List<Team>>();
				return teams;
			}
			else
			{
				var ex = Helper.CreateApiException(response);
				throw ex;
			}
		}

		public async Task<List<Team>> GetTeamsByGame(int GameID)
		{
			var response = await client.GetAsync($"api/Teams/ByGameInc/{GameID}");

			if (response.IsSuccessStatusCode)
			{
				List<Team> teams = await response.Content.ReadAsAsync<List<Team>>();
				return teams;
			}
			else
			{
				var ex = Helper.CreateApiException(response);
				throw ex;
			}
		}
	}
}
