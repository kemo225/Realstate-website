using System.Collections.Generic;

namespace RealEstate.Application.Features.Properties.Models;

public record CreatePropertyRequest(
     int ProjectId,
    string Name,
    decimal Price,
    int NoKithcen,
    int NoBedRoom,
    int NoBathRoom,
    int OwnerId);
