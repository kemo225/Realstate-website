using MediatR;
using RealEstate.Application.Features.Facilities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.Features.Service.Queries.GetService
{
    public record GetServiceQuery : IRequest<IEnumerable<SercviceDto>>;

}
