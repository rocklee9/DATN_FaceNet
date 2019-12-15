using DATN_FaceNet.Model;
using DATN_FaceNet.Model.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_FaceNet.Controller
{
     class ImagerManagementController
    {
        private static ImagerManagementController _instance;

        private ImagerManagementController()
        {
        }

        public static ImagerManagementController Instance => _instance ?? (_instance = new ImagerManagementController());
        public List<Imager> getImagers(int Id)
        {
            return ImagerManagementModel.Instance.getImagers(Id);
        }

        public void DelImager(int Id)
        {
            ImagerManagementModel.Instance.DelImager(Id);
        }
    }
}
