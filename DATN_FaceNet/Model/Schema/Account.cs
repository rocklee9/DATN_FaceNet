using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_FaceNet.Model.Schema
{
    public class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
    }
}
