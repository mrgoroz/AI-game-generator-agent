using System;

namespace GameIdeaService.Domain.Events
{
    public class TrendDiscovered
    {
        public string TrendName { get; set; }
        public DateTime DiscoveredAt { get; set; }
    }
}
