namespace MyProject.Core.Identity
{
    public interface IJwtFactory
    {
        string GenerateEncodedToken(string userId, string email);
    }
}
