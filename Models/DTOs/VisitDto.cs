using System.ComponentModel.DataAnnotations;

namespace APBD_k1_GrupaB.Models.DTOs;

public class VisitDto
{
    public int visitId { get; set; }
    public int clientId { get; set; }
    
    [StringLength(14)]
    public string mechanicLicenseNumber { get; set; }
    public List<VisitServiceDto> visitServices { get; set; }
}