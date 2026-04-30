using System;

namespace RealEstate.Application.Features.Requests.Models;

public class RequestDetailsDto
{
    public int Id { get; set; }
    public int UnitId { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int UnitArea { get; set; }
    
    public int ApplicantId { get; set; }
    public string ApplicantName { get; set; } = string.Empty;
    public string? ApplicantEmail { get; set; }
    public string ApplicantPhone { get; set; } = string.Empty;
    
    public string Status { get; set; } = string.Empty;
    
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
}
