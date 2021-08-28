using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace WallBox
{
    public static  class WallBoxApi
    {
       

        public static async Task<string> GetImageUrlAsync(DataModel.ImageModel imageModel)
        {

           return "https://wallbox.ru"+ Parser.DownloadUrlParser(
                await Request("https://wallbox.ru" + Parser.LoadPageParser(
                            await Request("https://wallbox.ru" + imageModel.LoadPageUrl)
                    )
                )
              );
            
        }
        public static async Task<(List<DataModel.ImageModel>, List<int>)> GetCategoryPageData(string Url)
        {
            var url = Url.Replace("?amp=1","");
            if (Url.Contains("wallbox.ru"))
                return Parser.PageParser(await Request(url));

            return Parser.PageParser(await Request("https://wallbox.ru/" + url));
        }
        public static async Task<(List<DataModel.ImageModel>, List<int>)> GetCategoryPageData(string categoryUrl,int page)=>
              await GetCategoryPageData(categoryUrl + "/page-" + page);
      
        public static async Task<List<DataModel.CategoryModel>> GetCategoriesAsync()=>
                    Parser.CategoriesParser(await Request("https://wallbox.ru"));

        public static  Task<string> Request(String url)
        {
            HttpClient client = new HttpClient();
            return client.GetStringAsync(url);
        }
    }
}
