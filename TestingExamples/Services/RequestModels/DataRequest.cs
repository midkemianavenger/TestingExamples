namespace Services.RequestModels;

public class DataRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string DataName { get; set; }
    public int DataIndicator { get; set; }
}