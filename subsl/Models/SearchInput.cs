using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

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

        public static Dictionary<string, object> Query = new Dictionary<string, object>();


        public static List<FeatureType> FeatureList = new List<FeatureType>()
        {
            new FeatureType() { type= "all", name= "Any" },
            new FeatureType() { type = "Movie", name = "Movie" },
            new FeatureType() { type = "Episode", name = "Episode" }
        };

        public static List<Langdef> LangList = new List<Langdef>()
        {
             new Langdef() { language_code= "", language_name= "Any" },
             new Langdef() { language_code= "af", language_name= "Afrikaans" },
             new Langdef() { language_code= "sq", language_name= "Albanian" },
             new Langdef() { language_code= "ar", language_name= "Arabic" },
             new Langdef() { language_code= "an", language_name= "Aragonese" },
             new Langdef() { language_code= "hy", language_name= "Armenian" },
             new Langdef() { language_code= "at", language_name= "Asturian" },
             new Langdef() { language_code= "eu", language_name= "Basque" },
             new Langdef() { language_code= "be", language_name= "Belarusian" },
             new Langdef() { language_code= "bn", language_name= "Bengali" },
             new Langdef() { language_code= "bs", language_name= "Bosnian" },
             new Langdef() { language_code= "br", language_name= "Breton" },
             new Langdef() { language_code= "bg", language_name= "Bulgarian" },
             new Langdef() { language_code= "my", language_name= "Burmese" },
             new Langdef() { language_code= "ca", language_name= "Catalan" },
             new Langdef() { language_code= "zh-cn", language_name= "Chinese (simplified)" },
             new Langdef() { language_code= "cs", language_name= "Czech" },
             new Langdef() { language_code= "da", language_name= "Danish" },
             new Langdef() { language_code= "nl", language_name= "Dutch" },
             new Langdef() { language_code= "en", language_name= "English" },
             new Langdef() { language_code= "eo", language_name= "Esperanto" },
             new Langdef() { language_code= "et", language_name= "Estonian" },
             new Langdef() { language_code= "fi", language_name= "Finnish" },
             new Langdef() { language_code= "fr", language_name= "French" },
             new Langdef() { language_code= "ka", language_name= "Georgian" },
             new Langdef() { language_code= "de", language_name= "German" },
             new Langdef() { language_code= "gl", language_name= "Galician" },
             new Langdef() { language_code= "el", language_name= "Greek" },
             new Langdef() { language_code= "he", language_name= "Hebrew" },
             new Langdef() { language_code= "hi", language_name= "Hindi" },
             new Langdef() { language_code= "hr", language_name= "Croatian" },
             new Langdef() { language_code= "hu", language_name= "Hungarian" },
             new Langdef() { language_code= "is", language_name= "Icelandic" },
             new Langdef() { language_code= "id", language_name= "Indonesian" },
             new Langdef() { language_code= "it", language_name= "Italian" },
             new Langdef() { language_code= "ja", language_name= "Japanese" },
             new Langdef() { language_code= "kk", language_name= "Kazakh" },
             new Langdef() { language_code= "km", language_name= "Khmer" },
             new Langdef() { language_code= "ko", language_name= "Korean" },
             new Langdef() { language_code= "lv", language_name= "Latvian" },
             new Langdef() { language_code= "lt", language_name= "Lithuanian" },
             new Langdef() { language_code= "lb", language_name= "Luxembourgish" },
             new Langdef() { language_code= "mk", language_name= "Macedonian" },
             new Langdef() { language_code= "ml", language_name= "Malayalam" },
             new Langdef() { language_code= "ms", language_name= "Malay" },
             new Langdef() { language_code= "ma", language_name= "Manipuri" },
             new Langdef() { language_code= "mn", language_name= "Mongolian" },
             new Langdef() { language_code= "no", language_name= "Norwegian" },
             new Langdef() { language_code= "oc", language_name= "Occitan" },
             new Langdef() { language_code= "fa", language_name= "Persian" },
             new Langdef() { language_code= "pl", language_name= "Polish" },
             new Langdef() { language_code= "pt-pt", language_name= "Portuguese" },
             new Langdef() { language_code= "ru", language_name= "Russian" },
             new Langdef() { language_code= "sr", language_name= "Serbian" },
             new Langdef() { language_code= "si", language_name= "Sinhalese" },
             new Langdef() { language_code= "sk", language_name= "Slovak" },
             new Langdef() { language_code= "sl", language_name= "Slovenian" },
             new Langdef() { language_code= "es", language_name= "Spanish" },
             new Langdef() { language_code= "sw", language_name= "Swahili" },
             new Langdef() { language_code= "sv", language_name= "Swedish" },
             new Langdef() { language_code= "sy", language_name= "Syriac" },
             new Langdef() { language_code= "ta", language_name= "Tamil" },
             new Langdef() { language_code= "te", language_name= "Telugu" },
             new Langdef() { language_code= "tl", language_name= "Tagalog" },
             new Langdef() { language_code= "th", language_name= "Thai" },
             new Langdef() { language_code= "tr", language_name= "Turkish" },
             new Langdef() { language_code= "uk", language_name= "Ukrainian" },
             new Langdef() { language_code= "ur", language_name= "Urdu" },
             new Langdef() { language_code= "uz", language_name= "Uzbek" },
             new Langdef() { language_code= "vi", language_name= "Vietnamese" },
             new Langdef() { language_code= "ro", language_name= "Romanian" },
             new Langdef() { language_code= "pt-br", language_name= "Portuguese (Brazilian)" },
             new Langdef() { language_code= "me", language_name= "Montenegrin" },
             new Langdef() { language_code= "zh-tw", language_name= "Chinese (traditional)" },
             new Langdef() { language_code= "ze", language_name= "Chinese bilingual" }
        };
    }
    public class Langdef
    {
        public  string? language_code { get; set; }
        public  string? language_name { get; set; }
    }

    public class FeatureType
    {
        public string? type { get; set; }
        public string? name { get; set; }
    }





}
