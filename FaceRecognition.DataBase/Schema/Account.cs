using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FaceRecognition.DataBase.Schema
{
    [Table("TaiKhoan")]
    public class Account: TableHaveIdInt
    {
        public Account()
        {
            Imagers = new HashSet<Imager>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(256)]
        public string hash_Pass { get; set; }

        [Required]
        [StringLength(50)]
        public string salt_Pass { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        public int Id_User { get; set; }

        public int Id_Role { get; set; }

        [ForeignKey("Id_User")]
        public virtual User User { get; set; }

        [ForeignKey("Id_Role")]
        public virtual Role Role { get; set; }

        public virtual ICollection<Imager> Imagers { get; set; }



    }
}