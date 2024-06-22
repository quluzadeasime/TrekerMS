//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using TREKER.DAL.Context;

//namespace TREKER.Business.BackgroundServices
//{
//    public class UnverifiedAccountCleanupService : BackgroundService
//    {
//        private readonly IServiceProvider _serviceProvider;

//        public UnverifiedAccountCleanupService(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                await Task.Delay(TimeSpan.FromHours(30), stoppingToken);

//                using (var scope = _serviceProvider.CreateScope())
//                {
//                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//                    var threshold = DateTime.Now.AddMinutes(-10);

//                    var unverifiedAccountsToDelete = dbContext.Users
//                        .Where(u => !u.EmailConfirmed && u.CreatedDate < threshold);

//                    dbContext.Users.RemoveRange(unverifiedAccountsToDelete);
//                    await dbContext.SaveChangesAsync(stoppingToken);
//                }
//            }
//        }
//    }
//}
