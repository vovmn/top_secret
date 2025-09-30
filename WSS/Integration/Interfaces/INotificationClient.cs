namespace WSS.API.Integration.Interfaces
{
    public interface INotificationClient
    {
        Task NotifyControlAboutCompletedWork(Guid objectId, Guid workItemId);

        Task NotifyControlAboutChangeRequest(Guid objectId);
    }
}