namespace Multitenancy
{
    public interface ITheme<out TTheme>
    {
        TTheme Value { get; }
    }
}