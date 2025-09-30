namespace WSS.API.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IChangeRequestRepository ChangeRequests { get; }
        IWorkCompletionReportRepository CompletionReports { get; }
        IWorkItemRepository WorkItems { get; }
        IWorkScheduleRepository WorkSchedules { get; }

        void Dispose();
        Task<int> SaveChangesAsync();
    }
}