namespace WallsAlphaCodersLib.Interfaces
{
    public enum Sort
    {
        newest,
        rating,
        views,
        favorites,
    }

    public interface ISort
    {
        public Sort? sort { get; set; }
    }
}