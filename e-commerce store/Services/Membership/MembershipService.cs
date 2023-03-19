using AutoMapper;
using e_commerce_store.Dto.Membership;
using e_commerce_store.Models;

public class MembershipService : IMembershipService
{
    private readonly IMembershipRepository _membershipRepository;
    private readonly IMapper _mapper;

    public MembershipService(IMembershipRepository membershipRepository, IMapper mapper)
    {
        _membershipRepository = membershipRepository;
        _mapper = mapper;
    }

    public async Task<MembershipDto> GetByIdAsync(int id)
    {
        var membership = await _membershipRepository.GetByIdAsync(id);
        return _mapper.Map<MembershipDto>(membership);
    }

    public async Task<List<MembershipDto>> GetAllAsync()
    {
        var memberships = await _membershipRepository.GetAllAsync();
        return _mapper.Map<List<MembershipDto>>(memberships);
    }

    public async Task<MembershipDto> CreateAsync(CreateMembershipDto createMembershipDto)
    {
        var membership = _mapper.Map<Membership>(createMembershipDto);
        var addedMembership = await _membershipRepository.CreateAsync(membership);
        return _mapper.Map<MembershipDto>(addedMembership);
    }

    public async Task<MembershipDto> UpdateAsync(int id, UpdateMembershipDto updateMembershipDto)
    {
        var membership = await _membershipRepository.GetByIdAsync(id);
        if (membership == null)
        {
            return null;
        }

        _mapper.Map(updateMembershipDto, membership);
        var updateMembership = await _membershipRepository.UpdateAsync(membership);
        return _mapper.Map<MembershipDto>(updateMembership);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _membershipRepository.DeleteAsync(id);
    }
}
