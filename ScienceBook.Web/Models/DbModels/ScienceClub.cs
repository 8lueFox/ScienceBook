﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.DbModels
{
    public class ScienceClub
    {
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public int CategoryOfScienceClubID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public byte[] Logo { get; set; }

        public virtual Department Department { get; set; }
        public virtual CategoryOfScienceClub CategoryOfScienceClub { get; set; }
        public virtual List<Member> Members { get; set; }
        public virtual List<Task> Tasks { get; set; }
        public virtual List<CategoryOfTask> CategoriesOfTasks { get; set; }
    }
}