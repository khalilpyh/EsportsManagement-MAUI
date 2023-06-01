/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ypeng_EsportsManagementMAUI.Models
{
	public class Team
	{
		public int ID { get; set; }

		public string Summary
		{
			get
			{
				if(Players.Count > 0)
					return $"{Name} ({Country}) - {Players.Count} Players";
				else
					return $"{Name} ({Country}) - 0 Player";
			}
		}

		public string PlayerList
		{
			get
			{
				if (Players != null && Players.Count > 0)
				{
					return String.Join(";  ", Players
											.OrderBy(p => p.FirstName)
											.ThenBy(p => p.LastName)
											.Select(p => p.FullName));
				}
				else
				{
					return "Currently No Player";
				}

			}
		}

		public string CreatedDateFormatted
		{
			get
			{
				return CreateDate.ToShortDateString();
			}
		}

		public string TotalWinningsFormatted
		{
			get
			{
				return TotalWinnings.ToString("C2", new CultureInfo("en-US"));
			}
		}


		public string Name { get; set; }

		public string Region { get; set; }

		public string Country { get; set; }

		public DateTime CreateDate { get; set; }

		public double TotalWinnings { get; set; }

		//public int? NumberOfPlayers { get; set; } = null;

		public int GameID { get; set; }

		public Game Game { get; set; }

		public ICollection<Player> Players { get; set; }
	}
}
