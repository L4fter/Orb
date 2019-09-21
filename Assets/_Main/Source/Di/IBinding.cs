namespace Meta.PoorMansDi
{
    public interface IBinding
    {
        void To<T>();
        void ToSingle<T>();
        void ToSingle(object o);
    }
}