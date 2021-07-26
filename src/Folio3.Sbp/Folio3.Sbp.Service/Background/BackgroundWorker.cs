using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Folio3.Sbp.Service.School.Dto;
using Folio3.Sbp.Service.School.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Folio3.Sbp.Service.Background
{
    /// <summary>
    /// This is a sample background services that receives an int value from controller via ChanelReader and uses the
    /// studentService to add that number of students. This a demonstration and can be modified accordingly to perform
    /// simple tasks, like thumbnail generation, email sending etc.
    /// </summary>
    public class SampleBackgroundJob : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ChannelReader<List<int>> _channel;
        private IServiceScopeFactory ServiceScopeFactory { get; }

        public SampleBackgroundJob(
            ILogger<SampleBackgroundJob> logger,
            ChannelReader<List<int>> channel,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _channel = channel;
            ServiceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            static string SerializeIds(IReadOnlyCollection<int> ids) => string.Join(',', ids.Select(scanId => ids.ToString()));

            await foreach (List<int> ids in _channel.ReadAllAsync(cancellationToken))
            {
                try
                {
                    // https://stackoverflow.com/a/51572736/1353501
                    using var scope = ServiceScopeFactory.CreateScope();
                    var studentService = scope.ServiceProvider.GetRequiredService<StudentService>();

                    int studentCount = ids.First();

                    for (var i = 0; i < studentCount; i++)
                    {
                        await studentService.AddDtoAsync(new StudentDto
                        {
                            FirstMidName = $"Student {i}",
                            LastName = "Anderson",
                            EnrollmentDate = DateTime.UtcNow
                        });
                    }

                    _logger.LogInformation($"Successfully processed backgroundJob [{SerializeIds(ids)}].");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Background job error for Ids [{SerializeIds(ids)}].");
                }
            }
        }
    }

    public static class BackgroundJobExtensions
    {
        public static IServiceCollection ConfigureSampleBackgroundJob(
            this IServiceCollection services)
        {
            services.AddHostedService<SampleBackgroundJob>();

            services.AddSingleton(Channel.CreateUnbounded<List<int>>(new UnboundedChannelOptions { SingleReader = true }));
            services.AddSingleton(svc => svc.GetRequiredService<Channel<List<int>>>().Reader);
            services.AddSingleton(svc => svc.GetRequiredService<Channel<List<int>>>().Writer);
            services.AddSingleton<BackgroundJobManager>();

            return services;
        }
    }

    public class BackgroundJobManager
    {
        private ChannelWriter<List<int>> Channel { get; }

        public BackgroundJobManager(ChannelWriter<List<int>> channel)
        {
            Channel = channel;
        }

        public async Task QueueJobAsync(IEnumerable<int> ids)
        {
            await Channel.WriteAsync(ids.ToList());
        }
    }


}
