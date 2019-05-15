using Abp;
using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Addapptables.Boilerplate.Storage
{
    [Table("AppBinaryObjects")]
    public class BinaryObject : Entity<Guid>
    {
        [Required]
        public virtual byte[] Bytes { get; set; }

        public string Type { get; set; }

        public string GenericId { get; set; }

        public BinaryObject()
        {
            Id = SequentialGuidGenerator.Instance.Create();
        }

        public BinaryObject(byte[] bytes, string type)
            : this()
        {
            Bytes = bytes;
            Type = type;
        }
    }
}
