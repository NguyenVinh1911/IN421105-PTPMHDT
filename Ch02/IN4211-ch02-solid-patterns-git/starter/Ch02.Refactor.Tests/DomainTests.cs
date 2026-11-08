using EduHub.Ch02.Domain;
using Xunit;

namespace Ch02.Refactor.Tests;

public class DomainTests
{
    [Fact]
    public void Club_Create_EmptyName_ThrowsArgumentException()
    {
        // Arrange
        var name = "   ";

        // Act
        var act = () => Club.Create(name, "mô tả");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Club_Create_NameLongerThanMax_ThrowsArgumentException()
    {
        // Arrange
        var name = new string('a', Club.MaxNameLength + 1);

        // Act
        var act = () => Club.Create(name, "mô tả");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Club_Create_ValidName_ReturnsClubWithId()
    {
        // Arrange
        var name = "Tech Club";

        // Act
        var club = Club.Create(name, "mô tả");

        // Assert
        Assert.Equal(name, club.Name);
        Assert.NotEqual(Guid.Empty, club.Id);
    }

    [Fact]
    public void Club_HasRoom_BelowMax_ReturnsTrue()
    {
        // Arrange
        var club = Club.Create("Tech Club", "mô tả");

        // Act
        var result = club.HasRoom(Club.MaxMembers - 1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Club_HasRoom_AtMax_ReturnsFalse()
    {
        // Arrange
        var club = Club.Create("Tech Club", "mô tả");

        // Act
        var result = club.HasRoom(Club.MaxMembers);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Event_Register_NewMember_ReturnsTrue()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), Title = "Workshop" };
        var memberId = Guid.NewGuid();

        // Act
        var result = ev.Register(memberId);

        // Assert
        Assert.True(result);
        Assert.Contains(memberId, ev.RegisteredMemberIds);
    }

    [Fact]
    public void Event_Register_SameMemberTwice_ReturnsFalse()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), Title = "Workshop" };
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        // Act
        var result = ev.Register(memberId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Event_CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), Title = "Workshop" };
        var memberId = Guid.NewGuid();

        // Act
        var act = () => ev.CheckIn(memberId);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void Event_CheckIn_RegisteredMember_AddsToCheckedIn()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), Title = "Workshop" };
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        // Act
        ev.CheckIn(memberId);

        // Assert
        Assert.Contains(memberId, ev.CheckedInMemberIds);
    }
}
