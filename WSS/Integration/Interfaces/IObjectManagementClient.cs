namespace WSS.API.Integration.Interfaces
{
    public interface IObjectManagementClient
    {
        Task<bool> IsUserOnObjectAsync(Guid objectId, double latitude, double longitude);
        Task<bool> ObjectExistsAndIsActiveAsync(Guid objectId);
    }
}