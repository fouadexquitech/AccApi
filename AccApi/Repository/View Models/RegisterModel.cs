using System.ComponentModel.DataAnnotations;

namespace AccApi.Repository.View_Models
{
    public class RegisterModel
    {
        public string? PhoneNumber { get; set; }
        public string DisplayName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public int SupplierId { get; set; }

        public bool LockoutEnabled { get; set; }
    }
}
