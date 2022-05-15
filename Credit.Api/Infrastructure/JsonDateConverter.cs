using Newtonsoft.Json.Converters;


namespace Credit.Api.Infrastructure
{
    /// <summary>
    /// Default JSON date converter setting
    /// </summary>
    
    public class JsonDateConverter:IsoDateTimeConverter
    {
        public const string DefaultDateTimeFormat = "dd.MM.yyyy";

        public JsonDateConverter(string format){
            DateTimeFormat = format;
        }
    }

}
