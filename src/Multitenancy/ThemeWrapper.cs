namespace Multitenancy
{
    public class ThemeWrapper<TTheme> : ITheme<TTheme>
    {
        public TTheme Value { get; }

        public ThemeWrapper(TTheme theme)
        {
            Value = theme;
        }
    }
}