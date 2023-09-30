namespace twitter_clone.Services.Authorization.IAuthorization
{
    public interface IAuthorization
    {
        public string GetToken(string email, string name,Guid id);
        public bool VerifyToken(string token);
        public string GetUserEmailFromToken(string token);

    }
}
