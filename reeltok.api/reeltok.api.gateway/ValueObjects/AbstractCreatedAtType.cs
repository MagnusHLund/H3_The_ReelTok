using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.ValueObjects
{
    public abstract class AbstractCreatedAtType<T>
    {
        [Required]
        [JsonProperty("CreatedAt")]
        public abstract T CreatedAt { get; }
    }
}