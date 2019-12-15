using DATN_FaceNet.Model.Schema;
using FaceRecognition.DataBase;
using System.Collections.Generic;
using System.Linq;


namespace DATN_FaceNet.Model
{
    public class ImagerManagementModel
    {
        private DataContext context = new DataContext();
        private static ImagerManagementModel _instance;
        private ImagerManagementModel()
        {
        }
        public static ImagerManagementModel Instance => _instance ?? (_instance = new ImagerManagementModel());
        public List<Imager> getImagers(int Id)
        {
            return context.Imagers.Where(x =>x.Id_Account==Id && !x.DelFlag).Select(x => new Imager
            {
                Id = x.Id,
                Base64Img = x.Img
            }).ToList();
        }

        public void DelImager(int Id)
        {
            var Imager = context.Imagers.FirstOrDefault(x=>x.Id==Id && !x.DelFlag);
            if(Imager != null)
            {
                Imager.DelFlag = true;
                context.SaveChanges();
            }

        }
    }
}
