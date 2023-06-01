/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ypeng_EsportsManagementMAUI.Models
{
	public class Player
	{
		public int ID { get; set; }
		public string FullName
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}

		public int Age
		{
			get
			{
				DateTime today = DateTime.Today;
				int a = today.Year - DOB.Year
					- ((today.Month < DOB.Month || (today.Month == DOB.Month && today.Day < DOB.Day) ? 1 : 0));
				return a;
			}
		}

		public string Summary
		{
			get
			{
				return $"{FullName} (Age: {Age})  - Pos: {Position}";
			}
		}

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Nickname { get; set; }

		public DateTime DOB { get; set; }

		public string Position { get; set; }

		public DateTime JoinDate { get; set; }

		//public Byte[] RowVersion { get; set; }

		public int TeamID { get; set; }

		public Team Team { get; set; }
	}
}
