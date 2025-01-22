using KiderApp.Models.Base;

namespace KiderApp.Models
{
    public class Agent:BaseEntity
    {
        public string FullName { get; set; }
        public string ImgUrl {get; set;}
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }
    }
}
