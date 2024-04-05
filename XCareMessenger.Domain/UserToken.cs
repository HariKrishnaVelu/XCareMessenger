using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCareMessenger.Domain
{
    public class UserToken
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(50)]
        public string UserMailID { get; set; }
        [Key]
        public string RefreshToken { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } 
        public DateTime LastUpdatedDate { get; set; }
        public User User { get; set; }
    }
}
