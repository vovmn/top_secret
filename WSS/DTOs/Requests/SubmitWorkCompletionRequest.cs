namespace WSS.API.DTOs.Requests
{
    public class SubmitWorkCompletionRequest
    {
        public DateTime? ActualStartDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public string? PhotoFileId { get; set; }
        public string? Comment { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
