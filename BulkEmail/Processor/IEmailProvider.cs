namespace BulkEmail.Processor
{
    public interface IEmailProvider
    {
        void Send(string name, string address);
    }
}