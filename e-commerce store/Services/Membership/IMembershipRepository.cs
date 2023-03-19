
using e_commerce_store.Models;

public interface IMembershipRepository
    {
        Task<Membership> GetByIdAsync(int id);
        Task<List<Membership>> GetAllAsync();
        Task<Membership> CreateAsync(Membership membership);
        Task<Membership> UpdateAsync(Membership membership);
        Task<bool> DeleteAsync(int id);
    }


