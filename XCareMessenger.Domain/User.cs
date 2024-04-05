using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace XCareMessenger.Domain
{    
    public enum Gender
    {
        Male,
        Female,
        NotMentioned
    }
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [Key]
        [StringLength(50)]
        public string UserMailID { get; set; }
        public int Gender { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(15)]
        public string Mobile { get; set; }
        public int ProfileID { get; set; }
        public Profile Profile { get; set; }
        public DateTime CreatedDate { get; set; }        
        public DateTime lastUpdatedDate { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }

    }
}