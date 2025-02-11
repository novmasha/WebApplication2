namespace WebApplication2
{
    public class MyApplication
    {
        private readonly IMessageService _messageService;
        public MyApplication(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task RunAsync()
        {
            Console.WriteLine(_messageService.GetMessage());
            await Task.CompletedTask; // Чтобы компилятор не выдавал предупреждение об отсутствии await
        }
    }
}
