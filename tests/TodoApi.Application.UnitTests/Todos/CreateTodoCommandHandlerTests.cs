using AutoMapper;
using FluentAssertions;
using Moq;
using TodoApi.Application.Common.Mappings;
using TodoApi.Application.Interfaces;
using TodoApi.Application.Todos.Commands.CreateTodo;
using TodoApi.Domain.Entities;


namespace TodoApi.Application.UnitTests.Todos;


public class CreateTodoCommandHandlerTests
{
    private readonly Mock<ITodoRepository> _repoMock = new();
    private readonly Mock<IUnitOfWork> _uowMock = new();
    private readonly IMapper _mapper;


    public CreateTodoCommandHandlerTests()
    {
        var config = new MapperConfiguration(c => c.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
    }


    [Fact]
    public async Task Handle_ValidCommand_ReturnsTodoResponse()
    {
        // Arrange
        var cmd = new CreateTodoCommand("Buy milk", null, null, Guid.NewGuid());
        var handler = new CreateTodoCommandHandler(_repoMock.Object, _uowMock.Object, _mapper);


        // Act
        var result = await handler.Handle(cmd, CancellationToken.None);


        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Buy milk");
        _repoMock.Verify(r => r.AddAsync(It.IsAny<TodoItem>(), It.IsAny<CancellationToken>()), Times.Once);
        _uowMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }


    [Fact]
    public async Task Handle_EmptyTitle_ThrowsValidationException()
    {
        // This is tested via ValidationBehavior — see validator tests
        var validator = new CreateTodoCommandValidator();
        var cmd = new CreateTodoCommand("", null, null, Guid.NewGuid());


        var result = await validator.ValidateAsync(cmd);


        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Title");
    }
}
