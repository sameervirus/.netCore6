namespace ArconsSystem.Api.Models.Users
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Permission> Permissions { get; set; } = null!;
    }
}