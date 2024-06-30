using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities;

namespace TREKER.Business.ViewModels.PageVM
{
    public class TrekkingVM
    {
        public IQueryable<Trekking>? Trekkings { get; set; }
        public IQueryable<Difficulty>? Difficulties { get; set; }
    }
}
