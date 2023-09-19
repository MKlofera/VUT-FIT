/*
 * REST API Decoder
 *
 * REST API Versions
 *
 * OpenAPI spec version: 1.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class HttpRequestDTO : IEquatable<HttpRequestDTO>
    { 
        /// <summary>
        /// Gets or Sets SessionId
        /// </summary>
        [Required]

        [DataMember(Name="sessionId")]
        public Guid? SessionId { get; set; }

        /// <summary>
        /// Gets or Sets Method
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum MethodEnum
        {
            /// <summary>
            /// Enum OPTIONSEnum for OPTIONS
            /// </summary>
            [EnumMember(Value = "OPTIONS")]
            OPTIONSEnum = 0,
            /// <summary>
            /// Enum GETEnum for GET
            /// </summary>
            [EnumMember(Value = "GET")]
            GETEnum = 1,
            /// <summary>
            /// Enum HEADEnum for HEAD
            /// </summary>
            [EnumMember(Value = "HEAD")]
            HEADEnum = 2,
            /// <summary>
            /// Enum POSTEnum for POST
            /// </summary>
            [EnumMember(Value = "POST")]
            POSTEnum = 3,
            /// <summary>
            /// Enum PUTEnum for PUT
            /// </summary>
            [EnumMember(Value = "PUT")]
            PUTEnum = 4,
            /// <summary>
            /// Enum DELETEEnum for DELETE
            /// </summary>
            [EnumMember(Value = "DELETE")]
            DELETEEnum = 5,
            /// <summary>
            /// Enum TRACEEnum for TRACE
            /// </summary>
            [EnumMember(Value = "TRACE")]
            TRACEEnum = 6,
            /// <summary>
            /// Enum CONNECTEnum for CONNECT
            /// </summary>
            [EnumMember(Value = "CONNECT")]
            CONNECTEnum = 7,
            /// <summary>
            /// Enum PATCHEnum for PATCH
            /// </summary>
            [EnumMember(Value = "PATCH")]
            PATCHEnum = 8        }

        /// <summary>
        /// Gets or Sets Method
        /// </summary>
        [Required]

        [DataMember(Name="method")]
        public MethodEnum? Method { get; set; }

        /// <summary>
        /// Gets or Sets Uri
        /// </summary>
        [Required]

        [DataMember(Name="uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Gets or Sets Timestamp
        /// </summary>
        [Required]

        [DataMember(Name="timestamp")]
        public long? Timestamp { get; set; }

        /// <summary>
        /// Gets or Sets Headers
        /// </summary>

        [DataMember(Name="headers")]
        public List<HttpHeaderDTO> Headers { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class HttpRequestDTO {\n");
            sb.Append("  SessionId: ").Append(SessionId).Append("\n");
            sb.Append("  Method: ").Append(Method).Append("\n");
            sb.Append("  Uri: ").Append(Uri).Append("\n");
            sb.Append("  Timestamp: ").Append(Timestamp).Append("\n");
            sb.Append("  Headers: ").Append(Headers).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((HttpRequestDTO)obj);
        }

        /// <summary>
        /// Returns true if HttpRequestDTO instances are equal
        /// </summary>
        /// <param name="other">Instance of HttpRequestDTO to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(HttpRequestDTO other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    SessionId == other.SessionId ||
                    SessionId != null &&
                    SessionId.Equals(other.SessionId)
                ) && 
                (
                    Method == other.Method ||
                    Method != null &&
                    Method.Equals(other.Method)
                ) && 
                (
                    Uri == other.Uri ||
                    Uri != null &&
                    Uri.Equals(other.Uri)
                ) && 
                (
                    Timestamp == other.Timestamp ||
                    Timestamp != null &&
                    Timestamp.Equals(other.Timestamp)
                ) && 
                (
                    Headers == other.Headers ||
                    Headers != null &&
                    Headers.SequenceEqual(other.Headers)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (SessionId != null)
                    hashCode = hashCode * 59 + SessionId.GetHashCode();
                    if (Method != null)
                    hashCode = hashCode * 59 + Method.GetHashCode();
                    if (Uri != null)
                    hashCode = hashCode * 59 + Uri.GetHashCode();
                    if (Timestamp != null)
                    hashCode = hashCode * 59 + Timestamp.GetHashCode();
                    if (Headers != null)
                    hashCode = hashCode * 59 + Headers.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(HttpRequestDTO left, HttpRequestDTO right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HttpRequestDTO left, HttpRequestDTO right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
