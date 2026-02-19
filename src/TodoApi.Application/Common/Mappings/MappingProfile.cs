using AutoMapper;
using TodoApi.Application.Todos.DTOs;
using TodoApi.Application.Auth.DTOs;
using TodoApi.Domain.Entities;


namespace TodoApi.Application.Common.Mappings;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity → Response DTO  (read-side only, no domain logic triggered)
        CreateMap<TodoItem, TodoResponse>();
        CreateMap<User, UserResponse>();
    }
}
