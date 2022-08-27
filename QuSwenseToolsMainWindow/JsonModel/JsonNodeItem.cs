using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuSwenseToolsMainWindow.JsonModel
{
    public class JsonPropertyNodeItem
    {
        public JsonTokenType TokenType { get; set; }
        public string KeyName { get; set; }
    }

    public class JsonNodeItem<T> : JsonPropertyNodeItem
    {
        public T Value { get; set; }
        public ObservableCollection<JsonNodeItem<T>> Items { get; set; } = new ();
    }

    public class JsonNodeRootItem : JsonNodeItem<string> { }
    public class JsonNodeStringItem : JsonNodeItem<string> { }
    public class JsonNodeDecimalItem : JsonNodeItem<decimal> { }
    public class JsonNodeBoolItem : JsonNodeItem<bool> { }
    public class JsonNodeArrayItem : JsonNodeItem<Array> { }
}
