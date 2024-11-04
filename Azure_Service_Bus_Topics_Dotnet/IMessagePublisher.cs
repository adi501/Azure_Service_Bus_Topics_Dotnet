namespace Azure_Service_Bus_Topics_Dotnet
{
    public interface IMessagePublisher
    {
        Task PublisherAsync<T>(T request);
    }
}
