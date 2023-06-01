/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ypeng_EsportsManagementMAUI.Services
{
	public class ApiException : Exception
	{
		public HttpResponseMessage Response { get; set; }

		public ApiException(HttpResponseMessage response)
		{
			this.Response = response;
		}

		public HttpStatusCode StatusCode
		{
			get
			{
				return this.Response.StatusCode;
			}
		}

		public IEnumerable<string> Errors
		{
			get
			{
				return this.Data.Values.Cast<string>().ToList();
			}
		}
	}
}
