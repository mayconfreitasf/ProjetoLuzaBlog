using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjetoLuzaBlog.Dal;
using ProjetoLuzaBlog.Pages.Autenticacao;
using ProjetoLuzaBlog.WebSocket;
using System.Configuration;

namespace ProjetoLuzaBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<AppDbContext>
                (options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString
                    ("DefaultConnection"))
                );

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
           .AddEntityFrameworkStores<AppDbContext>()
           .AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Autenticacao/Login";  // Caminho personalizado para login
                options.AccessDeniedPath = "/TemplateBlog"; // Caminho para p�ginas de acesso negado
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Autenticacao/Login";
                    options.AccessDeniedPath = "/TemplateBlog";  // Caminho para p�ginas de acesso negado
                });

            builder.Services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    // Aqui voc� pode configurar op��es adicionais de Razor Pages se necess�rio
                });


            // Registrar o WebSocketService como Singleton
            builder.Services.AddSingleton<IServicoWebSocket, ServicoWebSocket>();  // Singleton: Uma �nica inst�ncia para toda a aplica��o

            // Registrar o NotificacaoService
            builder.Services.AddScoped<ServicoNotificacao>();  // Scoped: Uma inst�ncia por requisi��o HTTP


            var app = builder.Build();


            // Habilitar WebSocket
            app.UseWebSockets();           

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Mapear WebSocket
            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/ws", async context =>
                {
                    var servicoWebSocket = context.RequestServices.GetRequiredService<IServicoWebSocket>();
                    await servicoWebSocket.HandleWebSocketConexao(context);
                });
            });

            app.MapRazorPages();
            app.MapGet("/", () => Results.Redirect("TemplateBlog"));

            app.Run();
        }

    }
}