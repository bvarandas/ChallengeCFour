using AutoMapper;
using ChallengeCrf.Application.ViewModel;
using ChallengeCrf.Domain.Models;

namespace ChallengeCrf.Application.AutoMapper;

public  class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile() 
    {
        CreateMap<CashFlow, CashFlowViewModel>();
    }
}
