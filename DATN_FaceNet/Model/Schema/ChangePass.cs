using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_FaceNet.Model.Schema
{
    public class ChangePass
    {
        public string OldPass { get; set; }
        public string Pass { get; set; }
        public string Pass_2 { get; set; }
    }
}
