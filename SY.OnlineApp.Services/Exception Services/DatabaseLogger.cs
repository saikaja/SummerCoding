using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SY.OnlineApp.Data;
using SY.OnlineApp.Data.Entities;

public class DatabaseLogger : ILogger
{
    private readonly string _source;
    private readonly IServiceProvider _serviceProvider;

    public DatabaseLogger(string source, IServiceProvider serviceProvider)
    {
        _source = source;
        _serviceProvider = serviceProvider;
    }

    public IDisposable? BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        // Skip logging if not an error or exception
        if (logLevel < LogLevel.Warning && exception == null)
            return;

        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BusinessDbContext>();

        db.Logs.Add(new LogEntry
        {
            ExceptionMessage = exception?.Message ?? formatter(state, exception),
            StackTrace = exception?.StackTrace ?? string.Empty,
            Source = _source,
            LoggedAt = DateTime.UtcNow
        });

        db.SaveChanges();
    }
}
