namespace Monument
{
    public interface IRuntimeContainer
    {
        T Resolve<T>() where T : class;
    }
}