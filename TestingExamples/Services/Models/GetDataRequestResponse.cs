using Services.DataModels;

namespace Services.Models;

public class GetDataRequestResponse
{
    public List<StructuredData> StructuredData { get; set; }
}