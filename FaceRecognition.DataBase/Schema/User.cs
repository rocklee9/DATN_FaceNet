using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FaceRecognition.DataBase.Schema
{
    [Table("User")]
    public class User: TableHaveIdInt
    {
       
        public User()
        {
            Accounts= new HashSet<Account>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        public bool Gender { get; set; }

        public DateTime Birthday { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}