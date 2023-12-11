using System.ComponentModel.DataAnnotations;

namespace TakidReciveForm.Domain.DTOs.WriteDTOs;

public record FormWriteDto
{
    [Required]
    public string ReceiverName { get; set; } = default!;
    [Required]
    public int VechicleNumber { get; set; }
    [Required]
    public string PickUpTime { get; set; } = default!;
    [Required]
    public string DeliveryTime { get; set; } = default!;
    [Required]
    public string Image { get; set; } = default!;
    [Required]
    public string ReasonForReceipt { get; set; } = default!;
    [Required]
    public string SignatureUponReceipt { get; set; } = default!;
    [Required]
    public string SignatureUponDelivery { get; set; } = default!;
}