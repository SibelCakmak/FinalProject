using Cor.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public partial class User:IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswortHash { get; set; }
        public bool Status { get; set; }

    }
}
