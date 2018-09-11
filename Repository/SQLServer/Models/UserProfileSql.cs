using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleBot.Repository.SQLServer.Models
{
    [Table("UserProfile")]
    public class UserProfileSql
    {
        [Column("Id")]
        public string IdSql { get; set; }
        public int Mensagens { get; set; }
    }
}