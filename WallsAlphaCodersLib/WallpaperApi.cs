using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
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

		private async Task<TResponse> Request<TResponse>(string @method)
		{
			var res = JsonSerializer.Deserialize<ApiResponse<TResponse>>(
					await _httpClient.GetStringAsync($"{apiUrl}?auth={_Secret}&method={@method}")
				);

			if (res.Success is true)
				return res.Response;
			else throw new ApiException(res.Error);
		}

		private async Task<TResponse>? Request<TResponse, TRequestParams>(string @method, TRequestParams @params)
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
			})
				.Where(it => it.Value != null);

			foreach (var i in typeEnum)
				url += $"&{i.Name.ToLower()}={i.Value}";

			var res = await _httpClient.GetStringAsync(url);
			JsonDocument array = JsonDocument.Parse(res);
			var s = array.RootElement.EnumerateObject().ToList();

			return JsonSerializer.Deserialize<TResponse>("");
		}

		public async Task<WallpaperResponse> RandomWallpapers(RandomWallpaperRequest randomWallpaperRequest) =>
					await Request<WallpaperResponse, RandomWallpaperRequest>("random", randomWallpaperRequest);

		public async Task<WallpaperResponse> SearchWallpapers(SearchWallpaperRequest searchWallpaperRequest) =>
					await Request<WallpaperResponse, SearchWallpaperRequest>("search", searchWallpaperRequest);

		public async Task<CategoryListResponse> CategoryList() =>
					await Request<CategoryListResponse>("category_list");

		public async Task<WallpaperResponse> Category(CategoryRequest categoryRequest) =>
					await Request<WallpaperResponse, CategoryRequest>("category", categoryRequest);

		public WallpaperApi(string secret) => _Secret = secret;
	}
}