using DATN_FaceNet.Model;
using DATN_FaceNet.Model.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_FaceNet.Controller
{
    class TrainingController
    {
        private static TrainingController _instance;

        private TrainingController()
        {
        }

        public static TrainingController Instance => _instance ?? (_instance = new TrainingController());

        public int AddImager(string imager)
        {
            return TrainingModel.Instance.AddImager(imager);
        }

        public List<Option> GetOptionRecogtion()
        {
            return TrainingModel.Instance.GetOptionRecogtion();
        }

        public List<Option> GetOptionTraining()
        {
            return TrainingModel.Instance.GetOptionTraining();
        }
    }
}
