using EduardNikitinKT_31_23.db;
using EduardNikitinKT_31_23.Dtos;
using EduardNikitinKT_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace EduardNikitinKT_31_23.Interfaces.GroupInterfaces;

public interface IGroupService
{
    Task<GroupDto[]> GetGroupsAsync(
        CancellationToken cancellationToken = default);

    Task<GroupDto?> GetGroupByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<GroupDto> CreateGroupAsync(
        GroupCreateDto requestDto,
        CancellationToken cancellationToken = default);

    Task<GroupDto?> UpdateGroupAsync(
        int id,
        GroupUpdateDto requestDto,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteGroupAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<GroupDto[]> GetGroupsFilteredAsync(
        int? specialityId = null,
        int? course = null,
        CancellationToken cancellationToken = default);
}





public class GroupService : IGroupService
{
    private readonly StudentDbContext _dbContext;

    public GroupService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GroupDto[]> GetGroupsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Groups
            .Select(g => new GroupDto
            {
                GroupId = g.GroupId,
                GroupName = g.GroupName,
                Course = g.Course,
                SpecialityId = g.SpecialityId
            })
            .ToArrayAsync(cancellationToken);
    }

    public async Task<GroupDto?> GetGroupByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Groups
            .Where(g => g.GroupId == id)
            .Select(g => new GroupDto
            {
                GroupId = g.GroupId,
                GroupName = g.GroupName,
                Course = g.Course,
                SpecialityId = g.SpecialityId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<GroupDto> CreateGroupAsync(
        GroupCreateDto requestDto,
        CancellationToken cancellationToken = default)
    {
        var group = new Group
        {
            GroupName = requestDto.GroupName,
            Course = requestDto.Course,
            SpecialityId = requestDto.SpecialityId
        };

        _dbContext.Groups.Add(group);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new GroupDto
        {
            GroupId = group.GroupId,
            GroupName = group.GroupName,
            Course = group.Course,
            SpecialityId = group.SpecialityId
        };
    }

    public async Task<GroupDto?> UpdateGroupAsync(
        int id,
        GroupUpdateDto requestDto,
        CancellationToken cancellationToken = default)
    {
        var group = await _dbContext.Groups
            .FirstOrDefaultAsync(g => g.GroupId == id, cancellationToken);

        if (group == null)
            return null;

        group.GroupName = requestDto.GroupName;
        group.Course = requestDto.Course;
        group.SpecialityId = requestDto.SpecialityId;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new GroupDto
        {
            GroupId = group.GroupId,
            GroupName = group.GroupName,
            Course = group.Course,
            SpecialityId = group.SpecialityId
        };
    }

    public async Task<bool> DeleteGroupAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var group = await _dbContext.Groups
            .FirstOrDefaultAsync(g => g.GroupId == id, cancellationToken);

        if (group == null)
            return false;

        _dbContext.Groups.Remove(group);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }


    public async Task<GroupDto[]> GetGroupsFilteredAsync(
        int? specialityId = null,
        int? course = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Group> query = _dbContext.Groups;

        if (specialityId.HasValue)
        {
            query = query.Where(g => g.SpecialityId == specialityId.Value);
        }

        if (course.HasValue)
        {
            query = query.Where(g => g.Course == course.Value);
        }

        return await query
            .Select(g => new GroupDto
            {
                GroupId = g.GroupId,
                GroupName = g.GroupName,
                Course = g.Course,
                SpecialityId = g.SpecialityId
            })
            .ToArrayAsync(cancellationToken);
    }
}