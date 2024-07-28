namespace Weather.Forecast.TechnicalStuff.Error;

public class ErrorResponse(string? activityId)
{
    public string? ActivityId { get; set; } = activityId;
    public int? ErrorCode { get; set; }
    public List<string> ValidationErrors { get; set; } = [];
}