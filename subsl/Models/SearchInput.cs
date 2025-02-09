using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subsl.Models
{
    public class SearchInput
    {
        public string? ai_translated { get; set; }
        
        public int? episode_number { get; set; }
        public string? foreign_parts_only { get; set; }
        public string? hearing_impaired { get; set; }

        public int? id { get; set; }

        public int? imdb_id { get; set; } 
        public string? languages { get; set; }
        public string? machine_translated { get; set; }
        public string? moviehash { get; set; }
        public string? moviehash_match { get; set; }
        public string? order_by { get; set; }
        public string? order_direction { get; set; }
        public int? page { get; set; }
        public int? parent_feature_id { get; set; } 
        public int? parent_imdb_id { get; set; } 
        public int? parent_tmdb_id { get; set; } 
        public string? query { get; set; }
        public int? season_number { get; set; } 
        public int? tmdb_id { get; set; } = null;
        public string? trusted_sources { get; set; }
        public string? type { get; set; }
        public int? uploader_id { get; set; } 
        public int? year { get; set; }
    }

}
