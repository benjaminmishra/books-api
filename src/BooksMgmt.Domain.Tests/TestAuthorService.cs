using BooksMgmt.Domain;
using Microsoft.Extensions.Logging;
using Moq;

namespace BooksMgmt.Domain.Tests;

public class TestAuthorService
{
    [Theory(DisplayName = "Unit Test for Validate Age")]
    [InlineData(2025,24,12, false)]
    [InlineData(1989, 24, 12, true)]
    [InlineData(1000, 24, 12, true)]
    public void TestValidateAge(int year, int day, int month, bool expected)
    {
        //Arrange
        ILogger<AuthorService> moqLogger =  Mock.Of<ILogger<AuthorService>>();
        AuthorService authorService = new AuthorService(moqLogger);

        //Act
        var output = authorService.ValidateAuthorAge(new DateTime(year,month,day));

        //Assert
        Assert.Equal(expected,output);
    }
}