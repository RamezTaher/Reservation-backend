using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Core.Utils
{
	public static class ShortUrlHelper
    {
		public static string ConvertToShortUrl(string oldUrl)
		{
			try
			{
				var client = new RestClient("https://api.short.io/");
				client.AddDefaultHeader("Authorization", "sk_oZa8D9lHX3FzMWpR");
				var req = new RestRequest("links", Method.Post);
				req.AddParameter("domain", "link.zamazemah.com.sa");
				req.AddParameter("originalURL", oldUrl);
				var res = client.Execute(req);
				if (res.IsSuccessful)
				{
					var link = JsonConvert.DeserializeObject<dynamic>(res.Content);
					return link.shortURL;
				}
				else
				{
					return oldUrl;
				}

			}
			catch (Exception e)
			{
				Console.Out.WriteLine("-----------------");
				Console.Out.WriteLine(e.Message);
				return oldUrl;
			}
		}
	}
	public class ApiResult
	{
		public string short_url { get; set; }
	}
}
