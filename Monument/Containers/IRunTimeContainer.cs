namespace Monument.Containers
{
    public interface IRuntimeContainer
    {
        T Resolve<T>() where T : class;
    }
}