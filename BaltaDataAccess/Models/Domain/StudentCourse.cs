using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaltaDataAccess.Models.Domain
{
    public class StudentCourse
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public int Progress { get; set; }
        public bool Favorite { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

    }
}
