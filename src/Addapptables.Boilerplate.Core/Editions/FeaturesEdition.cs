using Abp.Application.Editions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Addapptables.Boilerplate.Editions
{
    public class FeaturesEdition : Edition
    {
        public decimal? Price { get; set; }

        public bool? IsFree { get; set; }

        public EditionTypeEnum? EditionType { get; set; }

        public int? TrialDayCount { get; set; }
        
        public int? NumberOfUsers { get; set; }
    }
}
