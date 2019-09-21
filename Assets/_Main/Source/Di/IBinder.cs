namespace Meta.PoorMansDi
{
    public interface IBinder
    {
        IBinding Bind<T>();
    }
}