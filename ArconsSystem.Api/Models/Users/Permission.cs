namespace ArconsSystem.Api.Models.Users
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Role> Roles { get; set; } = null!;
    }
}