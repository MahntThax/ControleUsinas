using ControleUsinas.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace ControleUsinas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CriaBdSeNaoExiste(host);
            host.Run();
        }

        private static void CriaBdSeNaoExiste(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var servicos = scope.ServiceProvider;
                try
                {
                    var contexto = servicos.GetRequiredService<DbContexto>();
                    InicializadorBD.Inicializa(contexto);
                }
                catch (Exception ex)
                {
                    var logger = servicos.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Um erro ocorreu ao criar o banco de dados.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
