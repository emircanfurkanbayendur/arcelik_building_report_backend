using Microsoft.AspNetCore.JsonPatch;

public interface IJsonPatchConverter
{
    JsonPatchDocument<T> Deserialize<T>(string json) where T : class;
    string ToJson(JsonPatchDocument patch);
    JsonPatchDocument FromJson(string json);
}
