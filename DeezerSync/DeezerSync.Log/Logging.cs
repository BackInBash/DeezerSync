using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace DeezerSync.Log
{
    /// <summary>
    /// Log Class
    /// </summary>
    public class NLogger
    {
        private readonly ILogger<NLogger> _logger;

        public NLogger(ILogger<NLogger> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Debug Log
        /// </summary>
        /// <param name="name"></param>
        public void Debug(string name)
        {
            _logger.LogDebug(20, "{Action}", name);
        }
        /// <summary>
        /// Info Log
        /// </summary>
        /// <param name="name"></param>
        public void Info(string name)
        {
            _logger.LogInformation(20, "{Action}", name);
        }
        /// <summary>
        /// Error Log
        /// </summary>
        /// <param name="name"></param>
        public void Error(string name)
        {
            _logger.LogError(20, "{Action}", name);
        }
        /// <summary>
        /// Warning Log
        /// </summary>
        /// <param name="name"></param>
        public void Warning(string name, Exception e = null)
        {
            _logger.LogWarning(e, "{Action}", name);
        }
    }
    public class Logging
    {
        /// <summary>
        /// Private Logger Injector
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceProvider BuildDi(IConfiguration config)
        {
            return new ServiceCollection()
               .AddTransient<NLogger>()
               .AddLogging(loggingBuilder =>
               {
                   // configure Logging with NLog
                   loggingBuilder.ClearProviders();
                   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                   loggingBuilder.AddNLog(config);
               })
               .BuildServiceProvider();
        }
    }
}
