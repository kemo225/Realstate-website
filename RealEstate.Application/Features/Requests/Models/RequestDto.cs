using System;

namespace RealEstate.Application.Features.Requests.Models;

public class RequestDto
{
    public int Id { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public string ApplicantName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
}
