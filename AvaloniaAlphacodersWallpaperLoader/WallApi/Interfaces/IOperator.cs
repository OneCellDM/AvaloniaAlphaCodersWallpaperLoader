namespace WallsAlphaCodersLib.Interfaces
{
    public enum Operator
    {
        max,
        equal,
        min,
    }

    public interface IOperator
    {
        public Operator? @operator { get; set; }
    }
}