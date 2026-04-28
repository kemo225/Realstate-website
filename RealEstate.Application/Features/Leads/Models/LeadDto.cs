namespace RealEstate.Application.Features.Leads.Models;

public class LeadDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string? Email { get; set; }
    public string Phone { get; set; }
    public string ProjectName { get; set; }
    public int PropertyId { get; set; }
    public string PropertyName { get; set; }
    public string? Notes { get; set; }
}