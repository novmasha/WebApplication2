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
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                //Host.CreateDefaultBuilder(args): Создает стандартный хост-билдер, который автоматически
                //загружает конфигурацию из переменных окружения и аргументов командной строки
                //.ConfigureAppConfiguration((hostingContext, configuration) => { ... }):  Позволяет настроить источники конфигурации
                //в данном случае, он добавляет переменные окружения и аргументы командной строки
                {
                    // Конфигурация приложения
                    configuration.Sources.Clear();
                    configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    configuration.AddEnvironmentVariables();
                    configuration.AddCommandLine(args);
                })
                //регистрируем наши сервисы в контейнере DI
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddSingleton<IMessageService, MessageService>(); // Регистрирует MessageService как реализацию интерфейса IMessageService
                                                                              // AddSingleton означает, что будет создан только один экземпляр MessageService на все время жизни приложения
                    services.AddTransient<MyApplication>();  //Регистрируем MyApplication как transient, это нужно, чтобы можно было получить экземпляр MyApplication из контейнера D
                })
                .Build(); //Строит хост приложения

            var app = host.Services.GetRequiredService<MyApplication>();
            await app.RunAsync(); //Запуск приложения
        }
    }
}
