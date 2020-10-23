using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScienceBook.Web.Models.DbModels
{
    [Table("Posts")]
    public class Post
    {
        public int ID { get; set; }
        public int ScienceClubID { get; set; }
        public int MemberID { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Text { get; set; }
        public DateTime PublicationDay { get; set; }
        public byte[] Img { get; set; }

        public virtual ScienceClub ScienceClub { get; set; }
        public virtual Member Member { get; set; }
    }
}