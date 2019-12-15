using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FaceRecognition.DataBase.Schema
{
    [Table("Role")]
    public class Role: TableHaveIdInt
    {

        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}