namespace Contactify.Models
{
    public class Contact
    {
        public Guid Id { get; set; }    // Globally Unique Identifier (16-byte binary value)
        public string FullName { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }

    }
}
