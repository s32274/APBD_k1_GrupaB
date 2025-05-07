using APBD_k1_GrupaB.Models.DTOs;

namespace APBD_k1_GrupaB.Services;

public interface IVisitsService
{
    public Task<ClientVisitDto> GetVisitByClientIdAsync(int id, CancellationToken cancellationToken);
    // public Task<int> AddVisitAsync(VisitDto visitDto, CancellationToken cancellationToken);

}