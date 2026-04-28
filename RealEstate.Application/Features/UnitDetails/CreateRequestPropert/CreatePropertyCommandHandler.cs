using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Features.Properties.Commands.CreateProperty;

public class CreatePropertyCommandHandler : IRequestHandler<CreateRequestPropertPayCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePropertyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateRequestPropertPayCommand command, CancellationToken cancellationToken)
    {
        var property = new Domain.Entities.Unit();

        var request = command.Request;
        var project = await _unitOfWork.Repository<Project>().GetByIdAsync(request.ProjectId);

        if (project == null)
            throw new NotFoundException("Project", request.ProjectId);

        if (project.Properties == null)
            throw new ValidtationException("Project properties cannot be null.");

        if (!await _unitOfWork.Repository<Owner>().ExistsAsync(p => p.Id == request.OwnerId))
            throw new NotFoundException("Owner", request.OwnerId);


        int i = 0;
        foreach (var item in project.Properties) {
            i++;
            if (item.Name.ToLower() == request.Name.ToLower())
            {
                if(item.IsActive)
                    throw new ValidtationException($"A property with the name '{request.Name}' already Active in this project.");
                property = item; 
                break;
            }
            else if(i == project.Properties.Count)
            throw new ValidtationException($"A property with the name '{request.Name}' already exists in this project.");
        }



        await _unitOfWork.Repository<UnitDetail>().AddAsync(new UnitDetail(){
            UnitId = property.Id,
        });
        await _unitOfWork.SaveChangesAsync(cancellationToken);

      

        return Result<int>.Success(property.Id);
    }
}

