using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FaceRecognition.DataBase.Schema
{
    [Table("Imager")]
    public class Imager: TableHaveIdInt
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int Id_Account { get; set; }

        public string Img { get; set; }



        [ForeignKey("Id_Account")]
        public virtual Account Account { get; set; }
    }
}