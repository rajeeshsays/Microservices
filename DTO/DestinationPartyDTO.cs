

namespace TransportService.DTO
{
public class DestinationGroupDTO
{
    public int DestinationGroupId { get; set; }
    public int[] DestinationIds { get; set; } = Array.Empty<int>();
    public long TransportId { get; set; }
}
}