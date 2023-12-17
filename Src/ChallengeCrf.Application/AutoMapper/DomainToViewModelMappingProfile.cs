using AutoMapper;
using ChallengeCrf.Application.ViewModel;
using ChallengeCrf.Domain.Models;

namespace ChallengeCrf.Application.AutoMapper;

public  class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile() 
    {
        
        CreateMap<CashFlow, CashFlowViewModel>().ConstructUsing(c => new CashFlowViewModel(c.CashFlowId, c.Description, c.Amount, c.Entry, c.Date, c.Action));


        CreateMap<CashFlow, CashFlowViewModel>().ConstructUsing(c => new CashFlowViewModel(c.CashFlowId, c.Description, c.Amount, c.Entry, c.Date, c.Action));
    }
}
