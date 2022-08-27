using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuSwenseToolsMainWindow.JsonModel
{
    public class JsonReader
    {
        public JsonNodeRootItem JsonTree { get; set; }

        public void Reader(ReadOnlySpan<byte> utf8JsonStream)
        {
            var json = new Utf8JsonReader(utf8JsonStream, isFinalBlock: true, state: default);
            var currentJsonNodeItem = new JsonNodeRootItem();
            var currentPropertyName = "";

            while (json.Read())
            {
                JsonTokenType tokenType = json.TokenType;
                ReadOnlySpan<byte> valueSpan = json.ValueSpan;
                switch (tokenType)
                {
                    case JsonTokenType.StartObject:
                        var jsonStartObject = new JsonNodeRootItem();
                        if (JsonTree == null)
                            JsonTree = jsonStartObject;
                        else
                            currentJsonNodeItem.Items.Add(jsonStartObject);
                        currentJsonNodeItem = jsonStartObject;
                        break;
                    case JsonTokenType.EndObject:
                        break;
                    case JsonTokenType.StartArray:
                        var jsonArrayObject = new JsonNodeArrayItem();
                        break;
                    case JsonTokenType.EndArray:
                        break;
                    case JsonTokenType.PropertyName:
                        currentPropertyName = Encoding.UTF8.GetString(valueSpan);
                        break;
                    case JsonTokenType.String:
                        var jsonStringObject = new JsonNodeStringItem();
                        jsonStringObject.Value = json.GetString();
                        break;
                    case JsonTokenType.Number:
                        var jsonNumberObject = new JsonNodeDecimalItem();
                        jsonNumberObject.Value = json.GetInt64();
                        break;
                    case JsonTokenType.True:
                    case JsonTokenType.False:
                        var jsonBoolObject = new JsonNodeBoolItem();
                        jsonBoolObject.Value = json.GetBoolean();
                        break;
                    case JsonTokenType.Null:
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            utf8JsonStream = utf8JsonStream.Slice((int)json.BytesConsumed);
            JsonReaderState state = json.CurrentState;
        }

        public void Reader(string jsonText)
        {
            using var jsonDocument = JsonDocument.Parse(jsonText);

            foreach (var property in jsonDocument.RootElement.EnumerateObject())
            {
                if(property.Value.ValueKind == JsonValueKind.Object)
                {
                    foreach (var property1 in property.Value.EnumerateObject())
                    {

                    }
                }
            }
        }
    }
}
