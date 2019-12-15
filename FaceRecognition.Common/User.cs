using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaceRecognition.Common
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }

        public DateTime Birthday { get; set; }

        public int Role { get; set; }

        public string UserName { get; set; }
    }
}