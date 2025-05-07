using System.ComponentModel.DataAnnotations;

namespace APBD_k1_GrupaB.Models.DTOs;

public class ClientVisitDto
{
    public DateTime date { get; set; }
    public ClientDto client { get; set; }
    public MechanicDto mechanic { get; set; }
    public List<VisitServiceDto> visitServices { get; set; }
    
}

public class ClientDto
{
    [StringLength(100)]
    public string firstName { get; set; }
    public string lastName { get; set; }
    public DateTime dateOfBirth { get; set; }
}

public class MechanicDto
{
    public int mechanic_id {get; set;}
    [StringLength(14)]
    public string licence_number { get; set; }
}

public class VisitServiceDto
{
    [StringLength(100)]
    public string name { get; set; }
    public decimal service_fee {get; set;}
}