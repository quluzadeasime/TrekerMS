using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities;

namespace TREKER.Business.ViewModels.PageVM
{
    public class TablesVM
    {
        public IQueryable<TeamMember>? TeamMembers { get; set; }
        public IQueryable<Blog>? Blogs { get; set; }
        public IQueryable<Facility>? Facilities { get; set; }
        public IQueryable<Feature>? Features { get; set; }
        public IQueryable<Region>? Regions { get; set; }
        public IQueryable<Trekking>? Trekkings { get; set; }
        public IQueryable<Destination>? Destinations { get; set; }
        public IQueryable<Difficulty>? Difficulties { get; set; }
    }
}
