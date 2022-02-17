using System;
using System.Text.Json.Serialization;

namespace TodoApi.Models
{
    public class TodoModel
    {
        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("completed")]
        public bool IsCompleted { get; set; }

        [JsonPropertyName("text")]
        public string Description { get; set; }

        [JsonPropertyName("dueDate")]
        public DateTime? DueDate { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
