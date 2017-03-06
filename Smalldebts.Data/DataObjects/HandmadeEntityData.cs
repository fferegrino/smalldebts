using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smalldebts.Backend.DataObjects
{
#if !MOBILE_SERVICE
    public class HandmadeEntityData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Index(IsClustered = true)]
        public DateTimeOffset? CreatedAt { get; set; }
        public bool Deleted { get; set; }
        [Key]
        public string Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset? UpdatedAt { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }
    }
#endif
}
