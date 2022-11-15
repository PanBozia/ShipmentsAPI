using AutoMapper;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.Entities;

namespace ShipmentsAPI
{
    public class ShipmentsMappingProfile : Profile
    {
        public ShipmentsMappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            //.ForMember(m =>m.PurchaseOrders, c => c.MapFrom(s => s.PurchaseOrders) );
            CreateMap<PurchaseOrder, PurchaseOrderDto>();
            CreateMap<Forwarder, ForwarderDto>();
            CreateMap<Incoterm, IncotermDto>(); 
            CreateMap<Shipment, ShipmentDto>();
            CreateMap<Status, StatusDto>();
            CreateMap<WarehouseArea, WarehouseAreaDto>();
            CreateMap<CreateCustomerDto, Customer>();
        }
    }
}
