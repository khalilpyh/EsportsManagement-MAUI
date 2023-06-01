/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ypeng_EsportsManagementMAUI.ViewModels
{
	public partial class BaseViewModel : ObservableObject
	{
		[ObservableProperty]
		private string title;  //track the page title

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(IsNotLoading))]
		private bool isLoading;	//track data loading status

		public bool IsNotLoading => !isLoading;
	}
}