namespace Meta.PoorMansDi
{
    public interface IResolver
    {
        T Resolve<T>();
    }
}