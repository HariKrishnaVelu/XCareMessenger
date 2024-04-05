using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCareMessenger.Domain
{
    public class Profile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string UniqueName { get; set; }
        [StringLength(100)]
        public string Url { get; set; }
        [StringLength(20)]
        public string Type { get; set; }        
    }
}
