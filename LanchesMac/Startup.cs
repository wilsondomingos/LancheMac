using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace LanchesMac;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
      options.UseSqlServer(Configuration.GetConnectionString("DefaulConnection")));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        services.AddPaging(options =>
        {
            options.ViewName = "Bootstrap4";
            options.PageParameterName = "pageindex";
        });


        //services.Configure<IdentityOptions>(options =>
        //{
        //    // Default Password settings.
        //    options.Password.RequireDigit = false;
        //    options.Password.RequireLowercase = false;
        //    options.Password.RequireNonAlphanumeric = false;
        //    options.Password.RequireUppercase = false;
        //    options.Password.RequiredLength = 3;
        //    options.Password.RequiredUniqueChars = 1;
        //});

        services.AddTransient<ILancheRepository, LancheRepository>();
        services.AddTransient<IcategoriaRepository, CategoriaRepository>();
        services.AddTransient<IPedidosRepository, PedidoRepository>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();


        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin",
                politica =>
                {
                    politica.RequireRole("Admin");
                });
        });

        services.AddControllersWithViews();

        services.AddMemoryCache();
        services.AddSession();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        
        app.UseStaticFiles();
        app.UseRouting();

        //Criar perfil
        seedUserRoleInitial.SeedRoles();
        //Criar usuarios e atribuir ao perfil
        seedUserRoleInitial.SeedUsers();

        app.UseSession();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
         );

            endpoints.MapControllerRoute(
                name: "CategoriaFiltro",
                pattern: "Lanche/{action}/{categoria?}",
                defaults: new {Controller ="Lanche", Action="List"});

          

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}





   