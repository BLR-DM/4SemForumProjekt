using System.ComponentModel.DataAnnotations.Schema;

namespace ContentService.Domain
{
    public abstract class DomainEntity
    {
        public int Id { get; protected set; }
        public uint RowVersion { get; protected set; }
    }
}
