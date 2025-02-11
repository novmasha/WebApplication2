using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; //����������� ����������� ������������ ���� ��� ������ � DI � �������������
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
            using IHost host = Host.CreateDefaultBuilder(args) //����� ������� � ������������� ���� ����������
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                //Host.CreateDefaultBuilder(args): ������� ����������� ����-������, ������� �������������
                //��������� ������������ �� ���������� ��������� � ���������� ��������� ������
                //.ConfigureAppConfiguration((hostingContext, configuration) => { ... }):  ��������� ��������� ��������� ������������
                //� ������ ������, �� ��������� ���������� ��������� � ��������� ��������� ������
                {
                    // ������������ ����������
                    configuration.Sources.Clear();
                    configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    configuration.AddEnvironmentVariables();
                    configuration.AddCommandLine(args);
                })
                //������������ ���� ������� � ���������� DI
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddSingleton<IMessageService, MessageService>(); // ������������ MessageService ��� ���������� ���������� IMessageService
                                                                              // AddSingleton ��������, ��� ����� ������ ������ ���� ��������� MessageService �� ��� ����� ����� ����������
                    services.AddTransient<MyApplication>();  //������������ MyApplication ��� transient, ��� �����, ����� ����� ���� �������� ��������� MyApplication �� ���������� D
                })
                .Build(); //������ ���� ����������

            var app = host.Services.GetRequiredService<MyApplication>();
            await app.RunAsync(); //������ ����������
        }
    }
}
