using AutoMapper;
using KiderApp.Areas.Manage.ViewModels.Agent;
using KiderApp.Models;

namespace KiderApp.Areas.Manage.Helpers.Mappers
{
    public class AgentMapper:Profile
    {
        public AgentMapper()
        {
            CreateMap<CreateAgentVm,Agent>().ReverseMap();
            CreateMap<UpdateAgentVm, Agent>().ReverseMap();
        }
    }
}
