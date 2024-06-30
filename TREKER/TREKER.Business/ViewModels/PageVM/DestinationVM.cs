using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities;

namespace TREKER.Business.ViewModels.PageVM
{
    public class DestinationVM
    {
        public IQueryable<Destination>? Destinations { get; set; }
        public IQueryable<Region>? Regions { get; set; }
    }
}
