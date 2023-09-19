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
    public partial class SmtpClientMessageDTO : IEquatable<SmtpClientMessageDTO>
    { 
        /// <summary>
        /// Gets or Sets SessionId
        /// </summary>
        [Required]

        [DataMember(Name="sessionId")]
        public Guid? SessionId { get; set; }

        /// <summary>
        /// Gets or Sets Command
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum CommandEnum
        {
            /// <summary>
            /// Enum NOCOMMANDEnum for NO_COMMAND
            /// </summary>
            [EnumMember(Value = "NO_COMMAND")]
            NOCOMMANDEnum = 0,
            /// <summary>
            /// Enum EHLOEnum for EHLO
            /// </summary>
            [EnumMember(Value = "EHLO")]
            EHLOEnum = 1,
            /// <summary>
            /// Enum HELOEnum for HELO
            /// </summary>
            [EnumMember(Value = "HELO")]
            HELOEnum = 2,
            /// <summary>
            /// Enum MAILEnum for MAIL
            /// </summary>
            [EnumMember(Value = "MAIL")]
            MAILEnum = 3,
            /// <summary>
            /// Enum RCPTEnum for RCPT
            /// </summary>
            [EnumMember(Value = "RCPT")]
            RCPTEnum = 4,
            /// <summary>
            /// Enum DATAEnum for DATA
            /// </summary>
            [EnumMember(Value = "DATA")]
            DATAEnum = 5,
            /// <summary>
            /// Enum STARTTLSEnum for STARTTLS
            /// </summary>
            [EnumMember(Value = "STARTTLS")]
            STARTTLSEnum = 6,
            /// <summary>
            /// Enum AUTHEnum for AUTH
            /// </summary>
            [EnumMember(Value = "AUTH")]
            AUTHEnum = 7,
            /// <summary>
            /// Enum QUITEnum for QUIT
            /// </summary>
            [EnumMember(Value = "QUIT")]
            QUITEnum = 8,
            /// <summary>
            /// Enum RSETEnum for RSET
            /// </summary>
            [EnumMember(Value = "RSET")]
            RSETEnum = 9,
            /// <summary>
            /// Enum VRFYEnum for VRFY
            /// </summary>
            [EnumMember(Value = "VRFY")]
            VRFYEnum = 10,
            /// <summary>
            /// Enum EXPNEnum for EXPN
            /// </summary>
            [EnumMember(Value = "EXPN")]
            EXPNEnum = 11,
            /// <summary>
            /// Enum HELPEnum for HELP
            /// </summary>
            [EnumMember(Value = "HELP")]
            HELPEnum = 12,
            /// <summary>
            /// Enum NOOPEnum for NOOP
            /// </summary>
            [EnumMember(Value = "NOOP")]
            NOOPEnum = 13        }

        /// <summary>
        /// Gets or Sets Command
        /// </summary>
        [Required]

        [DataMember(Name="command")]
        public CommandEnum? Command { get; set; }

        /// <summary>
        /// Gets or Sets Parameters
        /// </summary>

        [DataMember(Name="parameters")]
        public List<string> Parameters { get; set; }

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
            sb.Append("class SmtpClientMessageDTO {\n");
            sb.Append("  SessionId: ").Append(SessionId).Append("\n");
            sb.Append("  Command: ").Append(Command).Append("\n");
            sb.Append("  Parameters: ").Append(Parameters).Append("\n");
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
            return obj.GetType() == GetType() && Equals((SmtpClientMessageDTO)obj);
        }

        /// <summary>
        /// Returns true if SmtpClientMessageDTO instances are equal
        /// </summary>
        /// <param name="other">Instance of SmtpClientMessageDTO to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SmtpClientMessageDTO other)
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
                    Command == other.Command ||
                    Command != null &&
                    Command.Equals(other.Command)
                ) && 
                (
                    Parameters == other.Parameters ||
                    Parameters != null &&
                    Parameters.SequenceEqual(other.Parameters)
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
                    if (Command != null)
                    hashCode = hashCode * 59 + Command.GetHashCode();
                    if (Parameters != null)
                    hashCode = hashCode * 59 + Parameters.GetHashCode();
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

        public static bool operator ==(SmtpClientMessageDTO left, SmtpClientMessageDTO right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SmtpClientMessageDTO left, SmtpClientMessageDTO right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
