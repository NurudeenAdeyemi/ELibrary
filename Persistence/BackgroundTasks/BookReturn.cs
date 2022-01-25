using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Configurations;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using Persistence.Context;

namespace Persistence.BackgroundTasks
{
    public class BookReturn: BackgroundService
    {
        private readonly BookReturnConfig _configuration;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;

        private readonly ILogger<BookReturn> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;


        public BookReturn(IServiceScopeFactory serviceScopeFactory, IOptions<BookReturnConfig> configuration, ILogger<BookReturn> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration.Value;
            _logger = logger;
            _schedule = CrontabSchedule.Parse(_configuration.CronExpression);
            _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Background Hosted Service for {nameof(BookReturn)}  is starting");
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
                   
                    var dueBooksExists =  await context.BookLendings.Where(b => b.BookReturned == false && b.DueDate <= DateTime.UtcNow).ToListAsync(stoppingToken);
                    if (dueBooksExists == null)
                    {
                        _logger.LogInformation($"No book is due for returning");
                    }
                    foreach (var book in dueBooksExists)
                    {
                        book.ReturnDate = DateTime.UtcNow;
                        book.BookReturned = true;
                        context.BookLendings.Update(book);
                       
                    }
                    await context.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occured reading Book Lending table  in database. {ex.Message}");
                    _logger.LogError(ex, ex.Message);
                }
                _logger.LogInformation($"Background Hosted Service for {nameof(BookReturn)}  is stopping");
                var timeSpan = _nextRun - now;
                await Task.Delay(timeSpan, stoppingToken);
                _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
            }
        }
    }
}
