using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace blogs.Models
{
    public class ExportComment
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Comment text
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Comment timestamp (UTC)
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
