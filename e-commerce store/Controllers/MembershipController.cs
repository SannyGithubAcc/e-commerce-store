using AutoMapper;
using e_commerce_store.Dto;
using e_commerce_store.Dto.Membership;
using e_commerce_store.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace e_commerce_store.Controllers
{

    /// <summary>
    /// Controller for managing memberships.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        /// <summary>
        /// Get a membership by ID.
        /// </summary>
        /// <param name="id">The ID of the membership to retrieve.</param>
        /// <returns>The requested membership.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Get a membership by ID.")]
        public async Task<ActionResult<MembershipDto>> GetById(int id)
        {
            var membership = await _membershipService.GetByIdAsync(id);
            if (membership == null)
            {
                return NotFound();
            }

            return Ok(membership);
        }

        /// <summary>
        /// Get all memberships.
        /// </summary>
        /// <returns>All memberships.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get all memberships.")]
        public async Task<ActionResult<List<MembershipDto>>> GetAll()
        {
            var memberships = await _membershipService.GetAllAsync();
            return Ok(memberships);
        }

        /// <summary>
        /// Create a new membership.
        /// </summary>
        /// <param name="createMembershipDto">The data for the new membership.</param>
        /// <returns>The newly created membership.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(Summary = "Create a new membership.")]
        public async Task<ActionResult<MembershipDto>> Create(CreateMembershipDto createMembershipDto)
        {
            var membership = await _membershipService.CreateAsync(createMembershipDto);
            return CreatedAtAction(nameof(GetById), new { id = membership.Id }, membership);
        }

        /// <summary>
        /// Update an existing membership.
        /// </summary>
        /// <param name="id">The ID of the membership to update.</param>
        /// <param name="updateMembershipDto">The updated data for the membership.</param>
        /// <returns>The updated membership.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Update an existing membership.")]
        public async Task<ActionResult<MembershipDto>> Update(int id, UpdateMembershipDto updateMembershipDto)
        {
            var membership = await _membershipService.UpdateAsync(id, updateMembershipDto);
            if (membership == null)
            {
                return NotFound();
            }

            return Ok(membership);
        }

        /// <summary>
        /// Delete a membership.
        /// </summary>
        /// <param name="id">The ID of the membership to delete.</param>
        /// <returns>A response indicating whether the operation was successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Delete a membership.")]
        public async Task<IActionResult> Delete(int id)
        {
            var membership = await _membershipService.GetByIdAsync(id);
            if (membership == null)
            {
                return NotFound();
            }

            await _membershipService.DeleteAsync(id);
            return NoContent();
        }
    }
}


