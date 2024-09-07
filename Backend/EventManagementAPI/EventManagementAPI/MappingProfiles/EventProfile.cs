using AutoMapper;
using EventManagementAPI.DTOs;
using EventManagementAPI.Models;

namespace EventManagementAPI.MappingProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Registration, RegistrationDTO>().ReverseMap();
            CreateMap<TicketType, TicketTypeDTO>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();

        }
    }
}
