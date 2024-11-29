namespace ProductUserApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Rol { get; set; }
        public string Token { get; set; }

        public bool IsPasswordEditable { get; set; }

        public bool IsModified { get; set; }

    }

}
