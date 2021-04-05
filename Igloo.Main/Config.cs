using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Igloo.Main
{
    public class StepInstructions
    {
        public int Step { get; set; }
        public string Value { get; set; }
    }

    public class Pengulet
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }
        [JsonPropertyName("steps")]
        public List<StepInstructions> Steps { get; set; }
    }

    public class PenguletsList
    {
        public List<Pengulet> Pengulets { get; set; }
    }
    
    public static class Config
    {
        public static PenguletsList Read()
        {
            var _jsonDocumentOptions = new JsonSerializerOptions();
            
            var inputStream = File.ReadAllText("./igloo.json");
            // inputStream = inputStream.Replace(@"[", "\"Pengulets\":[");
            
            return JsonSerializer.Deserialize<PenguletsList>(inputStream, _jsonDocumentOptions);
        }
    }
}