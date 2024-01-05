using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class Supplier
    {
        public int SupID { get; set; }
        public string SupName { get; set; }
        public string SupEmail { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsAccountCreated { get; set; }
    }

    //public class RegisterModel
    //{
    //    public string? FirstName { get; set; }
    //    public string? LastName { get; set; }
    //    public string? PhoneNumber { get; set; }
    //    public string DisplayName { get; set; }
    //    public string Email { get; set; }
    //    public int SupplierId { get; set; }

    //}
}
