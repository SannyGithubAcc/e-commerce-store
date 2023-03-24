using e_commerce_store.Models.Dto.Order;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_store.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

       public string Email { get; set; }

        public bool IsActive { get; set; }

        public string Phone { get; set; }


    }


}
