using System.Reflection;
using FluentAssertions;
using UnitTestScenatios.Example_one;

namespace UnitTestScenatios.UnitTests.Example_one;

public class LoadBalancerTests
{
    private readonly LoadBalancer _sut;

    public LoadBalancerTests()
    {
        _sut = new LoadBalancer(["con1", "con2", "con3"]);
    }
    
    [Fact]
    public void MoveNext_ShouldItrateOverConnections()
    {
        var tenant1 = _sut.MoveNext();
        var tenant2 = _sut.MoveNext();
        var tenant3 = _sut.MoveNext();
        var tenant4 = _sut.MoveNext();

        tenant1.ConnectionString.Should().Be("con1");
        tenant1.Id.Should().Be(1);
        tenant1.Predicate.Should().Be('a');
        
        tenant2.ConnectionString.Should().Be("con2");
        tenant2.Id.Should().Be(2);
        tenant2.Predicate.Should().Be('b');
        
        tenant3.ConnectionString.Should().Be("con3");
        tenant3.Id.Should().Be(3);  
        tenant3.Predicate.Should().Be('c');
        
        tenant4.ConnectionString.Should().Be("con1");
        tenant4.Id.Should().Be(1);
        tenant4.Predicate.Should().Be('a');
    }

    [Theory]
    [InlineData('a', "con1")]
    [InlineData('b', "con2")]
    [InlineData('c', "con3")]
    public void GetConnectionStringByPredicateId_ShouldReturnCorrectConnectionString(char act, string expected)
    {
        // arrange
        var con = _sut.GetConnectionStringByPredicateId(act);

        // act
        con.Should().Be(expected);
    }
    
    [Fact]
    public void GetConnectionStringByPredicateId_ShouldThrowExceptionWhenIndexWasOutOfRange()
    {
        // arrange
        var getConAction = () => _sut.GetConnectionStringByPredicateId('f');

        // act
        getConAction.Should().Throw<IndexOutOfRangeException>();
    }
    
}