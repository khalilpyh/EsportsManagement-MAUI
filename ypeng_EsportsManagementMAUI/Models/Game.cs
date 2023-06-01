/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ypeng_EsportsManagementMAUI.Models
{
	public class Game
	{
		public int ID { get; set; }

		public string Summary
		{
			get
			{
				return $"{Publisher} - Released on {ReleaseDate.ToString("yyyy-MM-dd")}";
			}
		}

		public string ReleaseDateFormatted
		{
			get
			{
				return ReleaseDate.ToShortDateString();
			}
		}

		public string Name { get; set; }

		public string Developer { get; set; }

		public string Publisher { get; set; }

		public string Designer { get; set; }

		public string Engine { get; set; }

		public DateTime ReleaseDate { get; set; }

		public ICollection<Team> Teams { get; set; }
	}
}
