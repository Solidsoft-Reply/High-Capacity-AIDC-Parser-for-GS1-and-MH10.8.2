using Newtonsoft.Json;

namespace Solidsoft.Reply.Parsers.HighCapacityAidc.PreProcessors;

/// <summary>
///   Represents a DOM 3 event code.
/// </summary>
public class EventCode {

    /// <summary>
    /// The event code.
    /// </summary>
    [JsonProperty("code", Order = 0)]
    public string Code { get; set; }

    /// <summary>
    /// Key modifiers.
    /// </summary>
    [JsonProperty("modifiers", Order = 1)]
    public int Modifiers { get; set; }
}