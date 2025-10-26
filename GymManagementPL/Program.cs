using GymManagementBLL.BusinessServices.Implemintation;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementDAL.Data.SeedDara;
using GymManagementDAL.Repositories.Implemintation;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.UnitOfWork;
using GymManagementPL.Mapping;
using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GymManagementPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Dependency Injection

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Dependency Injection for DbContext
            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            //Dependency Injection for Repositories and Services 

            //builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            builder.Services.AddScoped(typeof(ISessionRepository),typeof(SessionRepository));

            builder.Services.AddScoped(typeof(IAnalyticsService), typeof(AnalyticService));

            builder.Services.AddScoped<IMemberService,MemberService>();

            builder.Services.AddScoped<ITrainerService, TrainerService>();
            
            builder.Services.AddScoped<IPlanService, PlanService>();

            builder.Services.AddScoped<ISessionService, SessionService>();


            builder.Services.AddAutoMapper(X=>X.AddProfile(new MappingProfile()));
            #endregion

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>(); // expicit Injection
            //var pendingMigrations = dbContext.Database.GetPendingMigrations();
            //if (pendingMigrations?.Any() ?? false)
            //{
            //    dbContext.Database.Migrate();
            //}
            GymDbContextSeeding.SeedData(dbContext);

            #region Configure Pipline [MidelWares]


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")// segments / segments /
                .WithStaticAssets(); 
            #endregion

            app.Run();
        }
    }
}
