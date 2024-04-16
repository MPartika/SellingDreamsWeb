using SellingDreamsCommandHandler.Authenticate;

namespace SellingDreamsTest;

public class AuthenticationHelperTest
{
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestPasswordVerification()
    {
        var password = "password";
        var passwordHash = AuthenticationHelper.HashPassword(password, out byte[] salt);
        var IsVerified = AuthenticationHelper.VerifyPassword(password, passwordHash, salt);
        Assert.IsTrue(IsVerified);
    }
}