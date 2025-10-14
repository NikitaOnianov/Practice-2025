using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice2025.Models
{
    public partial class UserDTO
    {
        public long UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string UserType { get; set; }

        public string UserLogin { get; set; } = null!;
    }
}
