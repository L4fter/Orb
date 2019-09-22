namespace Meta.PoorMansDi
{
    public interface IBinding
    {
        void ToFactoryOf<T>();
        void ToSingle<T>();
        void ToSingle(object o);
    }
}