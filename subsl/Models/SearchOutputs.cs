﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subsl.Models
{



    public class SearchResults
    {
        public int? total_pages { get; set; }
        public int? total_count { get; set; }
        public int? per_page { get; set; }
        public int? page { get; set; }
        public ObservableCollection<ItemList> data { get; set; }
    }

    public class ItemList
    {
        public string? id { get; set; }
        public string? type { get; set; }
        public Attributes? attributes { get; set; }
    }

    public class Attributes
    {
        public string? subtitle_id { get; set; }
        public string? language { get; set; }
        public int? download_count { get; set; }
        public int? new_download_count { get; set; }
        public bool? hearing_impaired { get; set; }
        public bool? hd { get; set; }
        public float? fps { get; set; }
        public int? votes { get; set; }
        public float? ratings { get; set; }
        public bool? from_trusted { get; set; }
        public bool? foreign_parts_only { get; set; }
        public DateTime? upload_date { get; set; }
        public string[]? file_hashes { get; set; }
        public bool? ai_translated { get; set; }
        public int? nb_cd { get; set; }
        public string? slug { get; set; }
        public bool? machine_translated { get; set; }
        public string? release { get; set; }
        public string? comments { get; set; }
        public int? legacy_subtitle_id { get; set; }
        public int? legacy_uploader_id { get; set; }
        public Uploader? uploader { get; set; }
        public Feature_Details? feature_details { get; set; }
        public string? url { get; set; }
        public Related_Links[]? related_links { get; set; }
        public ObservableCollection<File>? files { get; set; }
    }

    public class Uploader
    {
        public int? uploader_id { get; set; }
        public string? name { get; set; }
        public string? rank { get; set; }
    }

    public class Feature_Details
    {
        public int? feature_id { get; set; }
        public string? feature_type { get; set; }
        public int? year { get; set; }
        public string? title { get; set; }
        public string? movie_name { get; set; }
        public int? imdb_id { get; set; }
        public int? tmdb_id { get; set; }
        public int? season_number { get; set; }
        public int? episode_number { get; set; }
        public int? parent_imdb_id { get; set; }
        public string? parent_title { get; set; }
        public int? parent_tmdb_id { get; set; }
        public int? parent_feature_id { get; set; }
    }

    public class Related_Links
    {
        public string? label { get; set; }
        public string? url { get; set; }
        public string? img_url { get; set; }
    }

    public class File
    {
        public int file_id { get; set; }
        public int? cd_number { get; set; }
        public string? file_name { get; set; }
    }


     static class MpvInput
    {
        public static string? Filepath { get; set; }
        public static string? Filename { get; set; }

    }

}
