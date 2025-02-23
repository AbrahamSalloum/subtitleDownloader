using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subsl.Models
{
    public static class SearchInput
    {
        public static string? ai_translated { get; set; }

        public static int? episode_number { get; set; }
        public static string? foreign_parts_only { get; set; }
        public static string? hearing_impaired { get; set; }

        public static int? id { get; set; }

        public static int? imdb_id { get; set; }
        public static string? languages { get; set; }
        public static string? machine_translated { get; set; }
        public static string? moviehash { get; set; }
        public static string? moviehash_match { get; set; }
        public static string? order_by { get; set; }
        public static string? order_direction { get; set; }
        public static int? page { get; set; }
        public static int? parent_feature_id { get; set; }
        public static int? parent_imdb_id { get; set; }
        public static int? parent_tmdb_id { get; set; }
        public static string? query { get; set; }
        public static int? season_number { get; set; }
        public static int? tmdb_id { get; set; } = null;
        public static string? trusted_sources { get; set; }
        public static string? type { get; set; }
        public static int? uploader_id { get; set; }
        public static int? year { get; set; }

        public static  Dictionary<string, object> Query = new Dictionary<string, object>();
    }

}
