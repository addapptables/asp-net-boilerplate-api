namespace Addapptables.Boilerplate.MultiTenancy.Rules.Models
{
    public class TenantModel
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }

        public string AdminEmailAddress { get; set; }

        public string ConnectionString { get; set; }

        public int? EditionId { get; set; }

        public bool IsActive { get; set; }
    }
}
