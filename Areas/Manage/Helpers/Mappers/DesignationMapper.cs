using AutoMapper;
using KiderApp.Areas.Manage.ViewModels.Designation;
using KiderApp.Models;

namespace KiderApp.Areas.Manage.Helpers.Mappers
{
    public class DesignationMapper : Profile
    {
        public DesignationMapper()
        {
            CreateMap<CreateDesignationVm,Designation>().ReverseMap();
            CreateMap<UpdateDesignationVm, Designation>().ReverseMap();
        }
    }
}
