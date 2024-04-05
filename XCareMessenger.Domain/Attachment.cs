using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCareMessenger.Domain
{
    public class Attachment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [StringLength(50)]
        public int FileName { get; set; }
        [Key]
        public string FileUniqueName { get; set; }
        [StringLength(100)]
        public string Url { get; set; }
        [StringLength(20)]
        public string FileType { get; set; }
        public int FileSize { get; set; }
    }
}
