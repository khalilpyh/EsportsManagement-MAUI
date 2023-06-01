/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ypeng_EsportsManagementMAUI.Services
{
	public static class Helper
	{
		//Local WebApi. Note: change port to yours that has the api runing on your local machine
		//public static Uri DBUri = new Uri("http://localhost:44326/");

		//WebApi on Azure
		public static Uri DBUri = new Uri("https://esportsmanagementapi.azurewebsites.net");

		public static ApiException CreateApiException(HttpResponseMessage response)
		{
			var httpErrorObject = response.Content.ReadAsStringAsync().Result;

			var anonymousErrorObject =
				new { message = "", errors = new Dictionary<string, string[]>() };

			// Deserialize
			var deserializedErrorObject =
				JsonConvert.DeserializeAnonymousType(httpErrorObject, anonymousErrorObject);

			var ex = new ApiException(response);

			if (deserializedErrorObject.message != null)
			{
				ex.Data.Add(-1, deserializedErrorObject.message);
			}
			if (deserializedErrorObject.errors != null)
			{
				foreach (var err in deserializedErrorObject.errors)
				{
					ex.Data.Add(err.Key, err.Value[0]);
				}
			}
			return ex;
		}
	}
}
