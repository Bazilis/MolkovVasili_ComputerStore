using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ComputerStore.DAL.Entities
{
    public class UserEntity : IdentityUser
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
