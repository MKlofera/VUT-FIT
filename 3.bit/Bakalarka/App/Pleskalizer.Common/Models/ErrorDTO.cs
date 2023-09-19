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
    public partial class ErrorDTO : IEquatable<ErrorDTO>
    { 
        /// <summary>
        /// Gets or Sets Message
        /// </summary>
        [Required]

        [DataMember(Name="message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets ExceptionString
        /// </summary>

        [DataMember(Name="exceptionString")]
        public string ExceptionString { get; set; }

        /// <summary>
        /// Gets or Sets InnerException
        /// </summary>

        [DataMember(Name="innerException")]
        public string InnerException { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ErrorDTO {\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("  ExceptionString: ").Append(ExceptionString).Append("\n");
            sb.Append("  InnerException: ").Append(InnerException).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ErrorDTO)obj);
        }

        /// <summary>
        /// Returns true if ErrorDTO instances are equal
        /// </summary>
        /// <param name="other">Instance of ErrorDTO to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ErrorDTO other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Message == other.Message ||
                    Message != null &&
                    Message.Equals(other.Message)
                ) && 
                (
                    ExceptionString == other.ExceptionString ||
                    ExceptionString != null &&
                    ExceptionString.Equals(other.ExceptionString)
                ) && 
                (
                    InnerException == other.InnerException ||
                    InnerException != null &&
                    InnerException.Equals(other.InnerException)
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
                    if (Message != null)
                    hashCode = hashCode * 59 + Message.GetHashCode();
                    if (ExceptionString != null)
                    hashCode = hashCode * 59 + ExceptionString.GetHashCode();
                    if (InnerException != null)
                    hashCode = hashCode * 59 + InnerException.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(ErrorDTO left, ErrorDTO right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ErrorDTO left, ErrorDTO right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
