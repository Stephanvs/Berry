namespace Berry.Core
{
    public interface IApplication<out TNavigationContext>
    {
        TNavigationContext CurrentNavigationContext { get; }
        //TViewModel GetViewModel<TViewModel>();
    }
}
