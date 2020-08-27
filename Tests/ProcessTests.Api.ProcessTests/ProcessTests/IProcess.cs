namespace ProcessTests.Api.ProcessTests.ProcessTests
{
    internal interface IProcess<out T>
    {
        T Run();
    }
}
