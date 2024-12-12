//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

#nullable enable

#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 612 // Disable "CS0612 '...' is obsolete"
#pragma warning disable 649 // Disable "CS0649 Field is never assigned to, and will always have its default value null"
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."
#pragma warning disable 8073 // Disable "CS8073 The result of the expression is always 'false' since a value of type 'T' is never equal to 'null' of type 'T?'"
#pragma warning disable 3016 // Disable "CS3016 Arrays as attribute arguments is not CLS-compliant"
#pragma warning disable 8603 // Disable "CS8603 Possible null reference return"
#pragma warning disable 8604 // Disable "CS8604 Possible null reference argument for parameter"
#pragma warning disable 8625 // Disable "CS8625 Cannot convert null literal to non-nullable reference type"
#pragma warning disable 8765 // Disable "CS8765 Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes)."

namespace Dfe.Complete.Client.Contracts
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial interface ICreateProjectClient
    {
        /// <summary>
        /// Creates a new Project
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Project created successfully.</returns>
        /// <exception cref="PersonsApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<ProjectId> Projects_CreateProject_Async(CreateConversionProjectCommand request);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Creates a new Project
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Project created successfully.</returns>
        /// <exception cref="PersonsApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<ProjectId> Projects_CreateProject_Async(CreateConversionProjectCommand request, System.Threading.CancellationToken cancellationToken);

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ProjectId
    {
        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Guid? Value { get; set; } = default!;

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static ProjectId FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectId>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class CreateConversionProjectCommand
    {
        [Newtonsoft.Json.JsonProperty("urn", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Urn? Urn { get; set; } = default!;

        [Newtonsoft.Json.JsonProperty("significantDate", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(DateFormatConverter))]
        public System.DateTime? SignificantDate { get; set; } = default!;

        [Newtonsoft.Json.JsonProperty("isSignificantDateProvisional", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? IsSignificantDateProvisional { get; set; } = default!;

        [Newtonsoft.Json.JsonProperty("incomingTrustUkprn", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Ukprn? IncomingTrustUkprn { get; set; } = default!;

        [Newtonsoft.Json.JsonProperty("region", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public Region? Region { get; set; } = default!;

        [Newtonsoft.Json.JsonProperty("isDueTo2Ri", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? IsDueTo2Ri { get; set; } = default!;

        [Newtonsoft.Json.JsonProperty("hasAcademyOrderBeenIssued", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? HasAcademyOrderBeenIssued { get; set; } = default!;

        [Newtonsoft.Json.JsonProperty("advisoryBoardDate", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(DateFormatConverter))]
        public System.DateTime? AdvisoryBoardDate { get; set; } = default!;

        [Newtonsoft.Json.JsonProperty("advisoryBoardConditions", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string? AdvisoryBoardConditions { get; set; } = default!;

        [Newtonsoft.Json.JsonProperty("establishmentSharepointLink", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string? EstablishmentSharepointLink { get; set; } = default!;

        [Newtonsoft.Json.JsonProperty("incomingTrustSharepointLink", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string? IncomingTrustSharepointLink { get; set; } = default!;

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static CreateConversionProjectCommand FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<CreateConversionProjectCommand>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class Urn
    {
        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Value { get; set; } = default!;

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static Urn FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Urn>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class Ukprn
    {
        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Value { get; set; } = default!;

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static Ukprn FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Ukprn>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public enum Region
    {

        [System.Runtime.Serialization.EnumMember(Value = @"London")]
        London = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"SouthEast")]
        SouthEast = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"YorkshireAndTheHumber")]
        YorkshireAndTheHumber = 2,

        [System.Runtime.Serialization.EnumMember(Value = @"NorthWest")]
        NorthWest = 3,

        [System.Runtime.Serialization.EnumMember(Value = @"EastOfEngland")]
        EastOfEngland = 4,

        [System.Runtime.Serialization.EnumMember(Value = @"WestMidlands")]
        WestMidlands = 5,

        [System.Runtime.Serialization.EnumMember(Value = @"NorthEast")]
        NorthEast = 6,

        [System.Runtime.Serialization.EnumMember(Value = @"SouthWest")]
        SouthWest = 7,

        [System.Runtime.Serialization.EnumMember(Value = @"EastMidlands")]
        EastMidlands = 8,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    internal class DateFormatConverter : Newtonsoft.Json.Converters.IsoDateTimeConverter
    {
        public DateFormatConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }



    [System.CodeDom.Compiler.GeneratedCode("NSwag", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class PersonsApiException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string? Response { get; private set; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public PersonsApiException(string message, int statusCode, string? response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception? innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class PersonsApiException<TResult> : PersonsApiException
    {
        public TResult Result { get; private set; }

        public PersonsApiException(string message, int statusCode, string? response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception? innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }

}

#pragma warning restore  108
#pragma warning restore  114
#pragma warning restore  472
#pragma warning restore  612
#pragma warning restore 1573
#pragma warning restore 1591
#pragma warning restore 8073
#pragma warning restore 3016
#pragma warning restore 8603
#pragma warning restore 8604
#pragma warning restore 8625