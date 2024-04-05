using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCareMessenger.Domain
{    
    public enum MessageStatus
    {
        Sent,
        Received,
        Read
    }
    public class UserMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [StringLength(50)]
        public string MsgFrom { get; set; }
        [StringLength(50)]
        public string MsgTo { get; set; }
        [StringLength(200)]
        public string MsgText { get; set; }
        public int? GroupID { get; set; } = null;
        public int AttachmentID { get; set; }
        public Attachment Attachment { get; set; }
        public int Status { get; set; } = (int)MessageStatus.Sent;
        public bool IsSenderDeleted { get; set; }= false;
        public bool IsReceiverDeleted { get; set; } = false;
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } 
       
    }
}
