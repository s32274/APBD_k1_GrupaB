using APBD_k1_GrupaB.Models.DTOs;
using APBD_k1_GrupaB.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_k1_GrupaB.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VisitsController
{
    private IVisitsService _visitsService;
    
    // 1. GET api/visits/{id}
    [HttpGet("{id}")]
    public async Task<ClientVisitDto> GetVisitByClientIdAsync(int id, CancellationToken cancellationToken)
    {
        _visitsService = new VisitsService();
        return await _visitsService.GetVisitByClientIdAsync(id, cancellationToken);
    }

    // // 2. POST api/visits
    // [HttpPost]
    // public async Task<bool> AddVisitAsync(CancellationToken cancellationToken)
    // {
    //     return await _visitsService.AddVisitAsync(cancellationToken);
    // }
}

