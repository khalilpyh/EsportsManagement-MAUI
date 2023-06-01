/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ypeng_EsportsManagementMAUI.Models;

namespace ypeng_EsportsManagementMAUI.Data
{
	internal interface IPlayerRepository
	{
		Task<List<Player>> GetPlayers();
		Task<Player> GetPlayer(int ID);
		Task<List<Player>> GetPlayersByTeam(int TeamID);
		Task AddPlayer(Player PlayerToAdd);
		Task UpdatePlayer(Player PlayerToUpdate);
		Task DeletePlayer(Player PlayerToDelete);
	}
}
