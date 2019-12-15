using DATN_FaceNet.Model.Schema;
using FaceRecognition.Common;
using FaceRecognition.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TblImager = FaceRecognition.DataBase.Schema.Imager;

namespace DATN_FaceNet.Model
{
    public class TrainingModel
    {
        private DataContext context = new DataContext();
        private static TrainingModel _instance;

        private TrainingModel()
        {
        }

        public static TrainingModel Instance => _instance ?? (_instance = new TrainingModel());
        public int AddImager(string imager)
        {
            try
            {
                context.Imagers.Add(new TblImager
                {
                    Id_Account = Common.User.Id,
                    Img = imager
                });
                context.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }

        }

        public List<Option> GetOptionRecogtion()
        {
            try
            {
                return context.TypeOfInputs.Where(x => !x.DelFlag).Select(x => new Option
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            }
            catch
            {
                return null;
            }
        }

        public List<Option> GetOptionTraining()
        {
            try
            {
                return context.TypeOfTrainings.Where(x => !x.DelFlag).Select(x => new Option
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
