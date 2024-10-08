namespace HRM.Data.ViewModels
{
    public class ProfileVM
    {
        public string Username { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
