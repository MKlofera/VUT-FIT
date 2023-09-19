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
    public partial class PopServerMessageDTO : IEquatable<PopServerMessageDTO>
    {
        public PopServerMessageDTO(Guid? sessionId, ReplyEnum? reply, string description, string payload, ImfEnvelopeDTO envelope, ImfMessageDTO email, string mailPath, List<ImfAttachmentDTO> attachments, long? timestamp)
        {
            SessionId = sessionId;
            Reply = reply;
            Description = description;
            Payload = payload;
            Envelope = envelope;
            Email = email;
            MailPath = mailPath;
            Attachments = attachments;
            Timestamp = timestamp;
        }

        public PopServerMessageDTO()
        {
            SessionId = Guid.NewGuid();
            Reply = PopServerMessageDTO.ReplyEnum.OKEnum;
            Description = String.Empty;
            Payload = String.Empty;
            Envelope = new ImfEnvelopeDTO();
            Email = new ImfMessageDTO();
            MailPath = String.Empty;
            Attachments = new List<ImfAttachmentDTO>();
            Timestamp = 0;
        }

        /// <summary>
        /// Gets or Sets SessionId
        /// </summary>
        [Required]

        [DataMember(Name="sessionId")]
        public Guid? SessionId { get; set; }

        /// <summary>
        /// Gets or Sets Reply
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum ReplyEnum
        {
            /// <summary>
            /// Enum OKEnum for OK
            /// </summary>
            [EnumMember(Value = "OK")]
            OKEnum = 0,
            /// <summary>
            /// Enum ERREnum for ERR
            /// </summary>
            [EnumMember(Value = "ERR")]
            ERREnum = 1        }

        /// <summary>
        /// Gets or Sets Reply
        /// </summary>
        [Required]

        [DataMember(Name="reply")]
        public ReplyEnum? Reply { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>

        [DataMember(Name="description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Payload
        /// </summary>

        [DataMember(Name="payload")]
        public string Payload { get; set; }

        /// <summary>
        /// Gets or Sets Envelope
        /// </summary>

        [DataMember(Name="envelope")]
        public ImfEnvelopeDTO Envelope { get; set; }

        /// <summary>
        /// Gets or Sets Email
        /// </summary>

        [DataMember(Name="email")]
        public ImfMessageDTO Email { get; set; }

        /// <summary>
        /// Gets or Sets MailPath
        /// </summary>

        [DataMember(Name="mailPath")]
        public string MailPath { get; set; }

        /// <summary>
        /// Gets or Sets Attachments
        /// </summary>

        [DataMember(Name="attachments")]
        public List<ImfAttachmentDTO> Attachments { get; set; }

        /// <summary>
        /// Gets or Sets Timestamp
        /// </summary>
        [Required]

        [DataMember(Name="timestamp")]
        public long? Timestamp { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PopServerMessageDTO {\n");
            sb.Append("  SessionId: ").Append(SessionId).Append("\n");
            sb.Append("  Reply: ").Append(Reply).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Payload: ").Append(Payload).Append("\n");
            sb.Append("  Envelope: ").Append(Envelope).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  MailPath: ").Append(MailPath).Append("\n");
            sb.Append("  Attachments: ").Append(Attachments).Append("\n");
            sb.Append("  Timestamp: ").Append(Timestamp).Append("\n");
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
            return obj.GetType() == GetType() && Equals((PopServerMessageDTO)obj);
        }

        /// <summary>
        /// Returns true if PopServerMessageDTO instances are equal
        /// </summary>
        /// <param name="other">Instance of PopServerMessageDTO to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PopServerMessageDTO other)
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
                    Reply == other.Reply ||
                    Reply != null &&
                    Reply.Equals(other.Reply)
                ) && 
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.Equals(other.Description)
                ) && 
                (
                    Payload == other.Payload ||
                    Payload != null &&
                    Payload.Equals(other.Payload)
                ) && 
                (
                    Envelope == other.Envelope ||
                    Envelope != null &&
                    Envelope.Equals(other.Envelope)
                ) && 
                (
                    Email == other.Email ||
                    Email != null &&
                    Email.Equals(other.Email)
                ) && 
                (
                    MailPath == other.MailPath ||
                    MailPath != null &&
                    MailPath.Equals(other.MailPath)
                ) && 
                (
                    Attachments == other.Attachments ||
                    Attachments != null &&
                    Attachments.SequenceEqual(other.Attachments)
                ) && 
                (
                    Timestamp == other.Timestamp ||
                    Timestamp != null &&
                    Timestamp.Equals(other.Timestamp)
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
                    if (Reply != null)
                    hashCode = hashCode * 59 + Reply.GetHashCode();
                    if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                    if (Payload != null)
                    hashCode = hashCode * 59 + Payload.GetHashCode();
                    if (Envelope != null)
                    hashCode = hashCode * 59 + Envelope.GetHashCode();
                    if (Email != null)
                    hashCode = hashCode * 59 + Email.GetHashCode();
                    if (MailPath != null)
                    hashCode = hashCode * 59 + MailPath.GetHashCode();
                    if (Attachments != null)
                    hashCode = hashCode * 59 + Attachments.GetHashCode();
                    if (Timestamp != null)
                    hashCode = hashCode * 59 + Timestamp.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(PopServerMessageDTO left, PopServerMessageDTO right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PopServerMessageDTO left, PopServerMessageDTO right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
