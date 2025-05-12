using FluentAssertions;
using UnitTestScenatios.Example_two;

namespace UnitTestScenatios.UnitTests.Example_two;

public class EmailValidatorTests
{
    private readonly EmailValidator emailValidator;

    public EmailValidatorTests()
    {
        this.emailValidator = new EmailValidator();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void IsValidEmail_ShouldReturnFalse_WhenEmailIsNullOrWhitespace(string email)
    {
        // act
        var isValidEmail = emailValidator.IsValidEmail(email);
        
        // assertion
        isValidEmail.Should().Be(false);
    }

    [Theory]
    [InlineData("ali@gmail.com", true)]
    [InlineData("ali@ali.com", true)]
    [InlineData("ali@.com", false)]
    [InlineData("ali@.", false)]
    [InlineData("ali@", false)]
    public void IsValidEmail_ShouldReturnTrue_WhenEmailIsValid(string email, bool isValid)
    {
        // act 
        var isValidEmail = this.emailValidator.IsValidEmail(email);
        
        // assertion
        isValidEmail.Should().Be(isValid);
    }
}