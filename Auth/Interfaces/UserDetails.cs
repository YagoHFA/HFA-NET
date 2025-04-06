namespace HFA.Auth.Interfaces
{
    public interface UserDetails
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        ICollection<Permission> Permissions { get;}
    }
}