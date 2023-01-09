using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace DataLogic.Magellan.Integration.App;

/// <summary>
/// ILogger implementation for writing errors to file
/// </summary>
public class FileErrorLogger : ILogger
{
    private readonly string _logFileLocation;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logFileLocation">The location of the text log file.
    /// If it doesn't exist, it is created. If it exists, it is overwritten.</param>
    public FileErrorLogger(string logFileLocation)
    {
        _logFileLocation = logFileLocation;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        var msg = formatter(state, exception);

        if (logLevel != LogLevel.Information)
        {
            File.AppendAllText(_logFileLocation, $"{DateTime.Now:O}\tLevel: {logLevel}\tMessage: {msg}\r\n");
        }

        Debug.WriteLine($"{DateTime.Now:O}\tLevel: {logLevel}\tMessage: {msg}\r\n");
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        throw new NotImplementedException();
    }

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        throw new NotImplementedException();
    }
}