using e_commerce_store.Data;
using e_commerce_store.Models;
using Microsoft.EntityFrameworkCore;

public class MembershipRepository : IMembershipRepository
    {
        private readonly EcommerceStoreDbContext _dbContext;

        public MembershipRepository(EcommerceStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Membership> GetByIdAsync(int id)
        {
            return await _dbContext.Membership.FindAsync(id);
        }

        public async Task<List<Membership>> GetAllAsync()
        {
            return await _dbContext.Membership.ToListAsync();
        }

        public async Task<Membership> CreateAsync(Membership membership)
        {
            await _dbContext.Membership.AddAsync(membership);
            await _dbContext.SaveChangesAsync();
            return membership;
        }

        public async Task<Membership> UpdateAsync(Membership membership)
        {
            _dbContext.Membership.Update(membership);
            await _dbContext.SaveChangesAsync();
            return membership;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var membership = await GetByIdAsync(id);
            if (membership == null)
            {
                return false;
            }

            _dbContext.Membership.Remove(membership);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }


