using AvaloniaAlphacodersWallpaperLoader.WallApi.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using WallsAlphaCodersLib.Objects.RequestModels;
using WallsAlphaCodersLib.Objects.ResponseModels;

namespace WallsAlphaCodersLib
{
	public class WallpaperApi
	{
		private HttpClient _httpClient = new HttpClient();

		private const string apiUrl = "https://wall.alphacoders.com/api2.0/get.php";

		private string _Secret = string.Empty;

		private JsonSerializerSettings settings = new JsonSerializerSettings() { Converters = { new StringToIntConverter() } };

		private async Task<TResponse> Request<TResponse>(string @method)
		{
			var res = await _httpClient.GetStringAsync($"{apiUrl}?auth={_Secret}&method={@method}");

			JObject responseObj = JObject.Parse(res);

			var Success = responseObj["success"].Value<bool>();

			if (Success)
				return JsonConvert.DeserializeObject<TResponse>(res, settings);
			else throw new ApiException(responseObj["error"].Value<string?>());
		}

		private async Task<TResponse?> Request<TResponse, TRequestParams>(string @method, TRequestParams @params)
		{
			string url = $"{apiUrl}?auth={_Secret}&method={method}";

			if (@params is null)
				throw new ApiException("Request params is null");

			var typeooft = @params.GetType();
			var type = typeooft.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var typeEnum = type.Select(it => new
			{
				it.Name,
				Value = typeooft.GetProperty(it.Name)?.GetValue(@params)
			}).Where(it => it.Value != null);

			foreach (var i in typeEnum)
				url += $"&{i.Name.ToLower()}={i.Value}";

			var res = await _httpClient.GetStringAsync(url);

			JObject responseObj = JObject.Parse(res);

			var Success = responseObj["success"].Value<bool>();

			return Success ? JsonConvert.DeserializeObject<TResponse>(res, settings) : throw new ApiException(responseObj["error"].Value<string?>());
		}

		public async Task<WallpaperResponse> RandomWallpapers(RandomWallpaperRequest randomWallpaperRequest) =>
					await Request<WallpaperResponse, RandomWallpaperRequest>("random", randomWallpaperRequest);

		public async Task<WallpaperResponse> SearchWallpapers(SearchWallpaperRequest searchWallpaperRequest) =>
					await Request<WallpaperResponse, SearchWallpaperRequest>("search", searchWallpaperRequest);

		public async Task<List<Category>> CategoryList() => await Request<List<Category>>("category_list");

		public async Task<WallpaperResponse> Category(CategoryRequest categoryRequest) =>
					await Request<WallpaperResponse, CategoryRequest>("category", categoryRequest);

		public WallpaperApi(string secret) => _Secret = secret;
	}
}