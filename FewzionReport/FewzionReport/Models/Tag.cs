using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FewzionReport.Models
{
    public class Tag
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ShortCode { get; set; }

        public int DisplayOrder { get; set; }

        public bool TagAttachment { get; set; }  // Indicates if an attachment is to show as a tag on task UIs

        public string Color { get; set; }

        public string Icon { get; set; }

        public string IconCode { get; set; }
    }
}
