using Microsoft.Extensions.Logging;

namespace GrpcHelperLib
{
    public interface IDirectCall
    {
        object DirectCall(string methodName, params object[] args);
    }

    public interface ILog
    {
        ILoggerFactory LoggerFactory { set; }
    }
}
