using System.ComponentModel.DataAnnotations;

namespace ShipmentsAPI.DtoModels
{
    public class CreateWarehouseAreaDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
