using MediatR;
using RealEstate.Application.Features.Projects.Commands.AddPropertyForProject;

namespace RealEstate.Application.Features.Requests.Commands.ApproveRequest;

public class ApproveRequestCommand : IRequest<bool>
{
    public int Id { get; set; }

    public List<PaymentPlanDtoCreateReqest> PaymentPlans { get; set; }=new List<PaymentPlanDtoCreateReqest>();

}
public class PaymentPlanDtoCreateReqest
{
    public decimal ? CommisionRate {  get; set; }
    public int? InstallmentMoths { get; set; }
    public decimal? InstallmentDownPayment { get; set; }
    public string PaymentType { get; set; } = string.Empty;
}