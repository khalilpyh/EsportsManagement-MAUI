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
	internal interface ITeamRepository
	{
		Task<List<Team>> GetTeams();
		Task<Team> GetTeam(int ID);
		Task<List<Team>> GetTeamsByGame(int GameID);
	}
}
