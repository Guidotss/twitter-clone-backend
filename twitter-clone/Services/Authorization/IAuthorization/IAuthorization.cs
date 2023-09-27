namespace twitter_clone.Services.Authorization.IAuthorization
{
    public interface IAuthorization
    {
        public string GetToken(string email, string name);
        public bool VerifyToken(string token);

    }
}
