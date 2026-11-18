using EduardNikitinKT_31_23.Dtos;
using EduardNikitinKT_31_23.Interfaces.GroupInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduardNikitinKT_31_23.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups(
        CancellationToken cancellationToken)
    {
        var groups = await _groupService.GetGroupsAsync(cancellationToken);

        return Ok(groups);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGroupById(
        int id,
        CancellationToken cancellationToken)
    {
        var group = await _groupService.GetGroupByIdAsync(
            id,
            cancellationToken);

        if (group == null)
            return NotFound();

        return Ok(group);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup(
        GroupCreateDto requestDto,
        CancellationToken cancellationToken)
    {
        var group = await _groupService.CreateGroupAsync(
            requestDto,
            cancellationToken);

        return Ok(group);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGroup(
        int id,
        GroupUpdateDto requestDto,
        CancellationToken cancellationToken)
    {
        var group = await _groupService.UpdateGroupAsync(
            id,
            requestDto,
            cancellationToken);

        if (group == null)
            return NotFound();

        return Ok(group);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroup(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await _groupService.DeleteGroupAsync(
            id,
            cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetGroupsFiltered(
        int? specialityId,
        int? course,
        CancellationToken cancellationToken)
    {
        var groups = await _groupService.GetGroupsFilteredAsync(
            specialityId,
            course,
            cancellationToken);

        return Ok(groups);
    }
}