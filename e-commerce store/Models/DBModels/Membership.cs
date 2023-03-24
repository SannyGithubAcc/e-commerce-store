using e_commerce_store.Models.DBModels;

namespace e_commerce_store.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }


    }

}
