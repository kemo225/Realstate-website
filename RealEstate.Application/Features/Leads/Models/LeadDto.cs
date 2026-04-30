using System;

namespace RealEstate.Application.Features.Leads.Models;

public class LeadDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string? Email { get; set; }
    public string Phone { get; set; }
    public string ProjectName { get; set; }
    public int UnitId { get; set; }
    public string PropertyName { get; set; }
    public string? Notes { get; set; }
    public string? StatusLead { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
