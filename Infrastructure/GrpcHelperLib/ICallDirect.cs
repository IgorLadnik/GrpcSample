namespace GrpcHelperLib
{
    public interface ICallDirect
    {
        object Call(string methodName, params object[] args);
    }
}
