using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScienceBook.Web.Models.ViewModels
{
    [Serializable]
    public class SearchingResult
    {
        public string Name { get; set; }
        public byte[] Logo { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
    }
}