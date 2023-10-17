namespace twitter_clone.Services
{
    public class CheckUUID
    {
        public Guid IsValid(string id)
        {
            bool isValid = System.Guid.TryParse(id, out System.Guid result);
            if (isValid)
            {
                return result; 
            }
            else
            {
                return Guid.Empty;
            }
        }
    }
}
