using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace GameIdeaService.Infrastructure.Models
{
    [Table("game_ideas")]
    public class SupabaseGameIdea : BaseModel
    {
        [PrimaryKey("id")]
        public string Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("genre")]
        public string Genre { get; set; }

        [Column("platform")]
        public string Platform { get; set; }

        [Column("trend_topic")]
        public string TrendTopic { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
