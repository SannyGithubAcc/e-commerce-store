
using e_commerce_store.Dto.Membership;
using e_commerce_store.Models;

public interface IMembershipService
    {
        Task<MembershipDto> GetByIdAsync(int id);
        Task<List<MembershipDto>> GetAllAsync();
        Task<MembershipDto> CreateAsync(CreateMembershipDto createMembershipDto);
        Task<MembershipDto> UpdateAsync(int id, UpdateMembershipDto updateMembershipDto);
        Task<bool> DeleteAsync(int id);
    }


