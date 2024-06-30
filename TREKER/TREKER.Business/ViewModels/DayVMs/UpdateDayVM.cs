using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.Commons;
using TREKER.Core.Entities;

namespace TREKER.Business.ViewModels.DayVMs
{
    public class UpdateDayVM : BaseEntityVm
    {
        public string? Description { get; set; }
        public int TrekkingId { get; set; }

        // View Models
        public ICollection<Trekking> Trekkings { get; set; }
    }

}
