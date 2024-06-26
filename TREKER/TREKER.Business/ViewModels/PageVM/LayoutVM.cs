using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.UserModels;

namespace TREKER.Business.ViewModels.PageVM
{
    public class LayoutVM
    {
        public AppUser? CurrrentUser { get; set; }
        public Dictionary<string,string> Settings { get; set; }
    }
}
