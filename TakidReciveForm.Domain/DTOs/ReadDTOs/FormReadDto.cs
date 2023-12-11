namespace TakidReciveForm.Domain.DTOs.ReadDTOs;

public record FormReadDto
{
    public Guid FormId { get; set; }
    public string ReceiverName { get; set; } = default!;
    public int VechicleNumber { get; set; }
    public DateTime PickUpTime { get; set; }
    public DateTime DeliveryTime { get; set; }
    public string Image { get; set; } = default!;
    public string ReasonForReceipt { get; set; } = default!;
    public string SignatureUponReceipt { get; set; } = default!;
    public string SignatureUponDelivery { get; set; } = default!;
}