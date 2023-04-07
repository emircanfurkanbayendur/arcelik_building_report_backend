using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;


namespace BuildingReport.Business.Concrete
{
    public class JsonPatchConverter : JsonConverter<JsonPatchDocument>, IJsonPatchConverter
    {
        public bool CanConvert(Type objectType)
        {
            return objectType == typeof(JsonPatchDocument);
        }
        public override JsonPatchDocument ReadJson(JsonReader reader, Type objectType, JsonPatchDocument existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var json = JObject.Load(reader);
            var operations = json.GetValue("operations").ToObject<List<Operation>>();
            var patchedOperations = operations.Select(op => new Microsoft.AspNetCore.JsonPatch.Operations.Operation
            {
                op = op.op,
                path = op.path,
                from = op.from,
                value = op.value
            }).ToList();
            return new JsonPatchDocument(patchedOperations,null);
        }

        public override void WriteJson(JsonWriter writer, JsonPatchDocument value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public JsonPatchDocument FromJson(string json)
        {
            return JsonConvert.DeserializeObject<JsonPatchDocument>(json, this);
        }

        public string ToJson(JsonPatchDocument patch)
        {
            return JsonConvert.SerializeObject(patch, this);
        }

        public JsonPatchDocument<T> Deserialize<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject<JsonPatchDocument<T>>(json, this);
        }
    }

    public class Operation
    {
        public string op { get; set; }
        public string path { get; set; }
        public string from { get; set; }
        public JToken value { get; set; }
    }
}
