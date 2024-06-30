using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.Commons;

namespace TREKER.Core.Entities
{
    public class Trekking : BaseAuditableEntity
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime Duration { get; set; }
        public int DifficultyId { get; set; }
        public Difficulty Difficulty { get; set; }
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }
        public byte GroupSize { get; set; }
        public float RoadHeight { get; set; }
        public int ReviewCount { get; set; }
        public float Star { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public IQueryable<Day> Days { get; set; }
        public IQueryable<TrekkingFeature> Features { get; set; }
        public IQueryable<TrekkingFacility> Facilities { get; set; }
        public IQueryable<TrekkingImage> Images { get; set; }
    }
}
