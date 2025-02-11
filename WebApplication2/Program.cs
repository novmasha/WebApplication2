using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; //импортируют необходимые пространства имен для работы с DI и конфигурацией
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication2 
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args) //метод создает и конфигурирует хост приложения
                //регистрируем наши сервисы в контейнере DI
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddSingleton<IMessageService, MessageService>(); // Регистрирует MessageService как реализацию интерфейса IMessageService
                                                                              // AddSingleton означает, что будет создан только один экземпляр MessageService на все время жизни приложения
                    services.AddTransient<MyApplication>();  //Регистрируем MyApplication как transient, это нужно, чтобы можно было получить экземпляр MyApplication из контейнера DI
                })
                .Build(); //Строит хост приложения

            var app = host.Services.GetRequiredService<MyApplication>();
            await app.RunAsync(); //Запуск приложения
        }
    }
}
