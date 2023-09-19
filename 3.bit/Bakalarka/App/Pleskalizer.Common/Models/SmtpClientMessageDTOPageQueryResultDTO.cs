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
    public partial class SmtpClientMessageDTOPageQueryResultDTO : IEquatable<SmtpClientMessageDTOPageQueryResultDTO>
    {
        public SmtpClientMessageDTOPageQueryResultDTO(byte[] pagingState, List<SmtpClientMessageDTO> items)
        {
            PagingState = pagingState;
            Items = items;
        }

        public SmtpClientMessageDTOPageQueryResultDTO()
        {
            PagingState = new byte[]{1};
            Items = new List<SmtpClientMessageDTO>();
        }

        /// <summary>
        /// Gets or Sets PagingState
        /// </summary>

        [DataMember(Name="pagingState")]
        public byte[] PagingState { get; set; }

        /// <summary>
        /// Gets or Sets Items
        /// </summary>

        [DataMember(Name="items")]
        public List<SmtpClientMessageDTO> Items { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SmtpClientMessageDTOPageQueryResultDTO {\n");
            sb.Append("  PagingState: ").Append(PagingState).Append("\n");
            sb.Append("  Items: ").Append(Items).Append("\n");
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
            return obj.GetType() == GetType() && Equals((SmtpClientMessageDTOPageQueryResultDTO)obj);
        }

        /// <summary>
        /// Returns true if SmtpClientMessageDTOPageQueryResultDTO instances are equal
        /// </summary>
        /// <param name="other">Instance of SmtpClientMessageDTOPageQueryResultDTO to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SmtpClientMessageDTOPageQueryResultDTO other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    PagingState == other.PagingState ||
                    PagingState != null &&
                    PagingState.Equals(other.PagingState)
                ) && 
                (
                    Items == other.Items ||
                    Items != null &&
                    Items.SequenceEqual(other.Items)
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
                    if (PagingState != null)
                    hashCode = hashCode * 59 + PagingState.GetHashCode();
                    if (Items != null)
                    hashCode = hashCode * 59 + Items.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(SmtpClientMessageDTOPageQueryResultDTO left, SmtpClientMessageDTOPageQueryResultDTO right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SmtpClientMessageDTOPageQueryResultDTO left, SmtpClientMessageDTOPageQueryResultDTO right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}