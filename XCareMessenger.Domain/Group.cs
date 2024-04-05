using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCareMessenger.Domain
{
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [Key]
        public Guid GroupID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string UserMailID { get; set; }
        public bool IsAdmin { get; set; } = false;
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime lastUpdatedDate { get; set; }
    }
}
