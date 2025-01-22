using KiderApp.Models.Base;

namespace KiderApp.Models
{
    public class Designation:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Agent> Agents { get; set; }
    }
}
