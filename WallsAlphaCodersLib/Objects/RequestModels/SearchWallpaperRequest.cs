using System;
using System.Net.NetworkInformation;

namespace WallsAlphaCodersLib.Objects.RequestModels
{
    
    public class SearchWallpaperRequest
    {
        
        /// <summary>
        /// Search term
        /// </summary>
        public  string Term { get; set; }
        
        public  int? InfoLevel { get; set; }
        public  int? Page { get; set; }
        public  int? Width { get; set; }
        public  int? Height { get; set; }
        

    }
}