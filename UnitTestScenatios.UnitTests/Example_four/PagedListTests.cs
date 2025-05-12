using FluentAssertions;
using UnitTestScenatios.Example_four;

namespace UnitTestScenatios.UnitTests.Example_four;

public class PagedListTests
{
    [Fact]
    public async Task Constructor_ShouldInitializeProperties()
    {
        // arrange
        var items = new List<string> { "item1", "item2", "item3", "item4" };
        var expectedPagedList = new List<string> { "item2" };

        // act
        var pagedList = await PagedList<string>.Paginate(items.AsQueryable(), 2, 1);

        // assertion
        pagedList.TotalItems.Should().Be(4);
        pagedList.Items.Should().HaveCount(1);
        pagedList.PageNumber.Should().Be(2);
        pagedList.PageSize.Should().Be(1);
        pagedList.HasNextPage.Should().BeTrue();
        pagedList.HasPreviousPage.Should().BeTrue();
        pagedList.Items.Should().BeEquivalentTo(expectedPagedList);
        
    }
}