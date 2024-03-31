using LegacyApp;

namespace LegacyAppTests;

public class UnitTest1
{
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Without_At_And_Dot()
    {
        //Arrange
        string firstName = "John";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(1980, 1, 1);
        int clientId = 1;
        string email = "doe";
        var service = new UserService();

        //Act
        bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);

        //Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void Get_Credit_Limit_Returns_Correct_Value()
    {
        //Arrange 
        int correctVal = 200;
        var birthDate = DateTime.Now;
        UserCreditService userCreditService = new UserCreditService();
        
        //Act 
        int returnedVal = userCreditService.GetCreditLimit("Kowalski", birthDate);
        bool isEqual = correctVal == returnedVal;

        //Assert
        Assert.Equal(true, isEqual);
    }
    
    [Fact]
    public void Correctly_Checks_If_Given_Name_Is_Empty()
    {
        UserService userService = new UserService();
        string fName = "";
        string lName = "Kowalski";
        string email = "jan.kowalski@poczta.pl";
        DateTime birthDate = DateTime.Now;
        int clientId = 100;

        bool res = userService.AddUser(fName, lName, email, birthDate, clientId);
        
        Assert.Equal(false, res);
    }
    
    [Fact]
    public void Correctly_Checks_If_Given_Surname_Is_Empty()
    {
        UserService userService = new UserService();
        string fName = "Jan";
        string lName = "";
        string email = "jan.kowalski@poczta.pl";
        DateTime birthDate = DateTime.Now;
        int clientId = 100;

        bool res = userService.AddUser(fName, lName, email, birthDate, clientId);
        
        Assert.Equal(false, res);
    }
    
    [Fact]
    public void AddUser_ValidDataForRegularClient_ReturnsTrue()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        bool result = userService.AddUser("John", "Doe", "john.doe@example.com", new DateTime(1990, 1, 1), 1);

        // Assert
        Assert.Equal(true ,result);
    }
    
    [Fact]
    public void AddUser_ValidDataForVeryImportantClient_ReturnsTrue()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        bool result = userService.AddUser("John", "Doe", "john.doe@example.com", new DateTime(1990, 1, 1), 2);

        // Assert
        Assert.Equal(true, result);
    }
    
    [Fact]
    public void AddUser_ValidDataForImportantClient_ReturnsTrue()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        bool result = userService.AddUser("John", "Doe", "john.doe@example.com", new DateTime(1990, 1, 1), 3);

        // Assert
        Assert.Equal(true, result);
    }
    
    [Fact]
    public void AddUser_ValidDataForClient_ReturnsTrue()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        bool result = userService.AddUser("John", "Doe", "john.doe@example.com", new DateTime(1990, 1, 1), 1);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void Retunrs_False_If_User_Has_CreditLimit_And_His_CreditLimit_Is_Less_Than_500()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        bool result = userService.AddUser("John", "Kowalski", "kowalski@wp.pl", new DateTime(1990, 12, 12), 1);
        
        // Assert
        Assert.Equal(false, result);
    }
}