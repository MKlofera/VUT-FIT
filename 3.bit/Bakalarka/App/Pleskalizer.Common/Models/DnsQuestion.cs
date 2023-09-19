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
    public partial class DnsQuestion : IEquatable<DnsQuestion>
    {
        public DnsQuestion(TypeEnum? type, string name)
        {
            Type = type;
            Name = name;
        }

        public DnsQuestion()
        {
            Type = null;
            Name = String.Empty;
        }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum TypeEnum
        {
            /// <summary>
            /// Enum NotSupportedEnum for NotSupported
            /// </summary>
            [EnumMember(Value = "NotSupported")]
            NotSupportedEnum = 0,
            /// <summary>
            /// Enum AEnum for A
            /// </summary>
            [EnumMember(Value = "A")]
            AEnum = 1,
            /// <summary>
            /// Enum NsEnum for Ns
            /// </summary>
            [EnumMember(Value = "Ns")]
            NsEnum = 2,
            /// <summary>
            /// Enum MdEnum for Md
            /// </summary>
            [EnumMember(Value = "Md")]
            MdEnum = 3,
            /// <summary>
            /// Enum MfEnum for Mf
            /// </summary>
            [EnumMember(Value = "Mf")]
            MfEnum = 4,
            /// <summary>
            /// Enum CnameEnum for Cname
            /// </summary>
            [EnumMember(Value = "Cname")]
            CnameEnum = 5,
            /// <summary>
            /// Enum SoaEnum for Soa
            /// </summary>
            [EnumMember(Value = "Soa")]
            SoaEnum = 6,
            /// <summary>
            /// Enum MbEnum for Mb
            /// </summary>
            [EnumMember(Value = "Mb")]
            MbEnum = 7,
            /// <summary>
            /// Enum MgEnum for Mg
            /// </summary>
            [EnumMember(Value = "Mg")]
            MgEnum = 8,
            /// <summary>
            /// Enum MrEnum for Mr
            /// </summary>
            [EnumMember(Value = "Mr")]
            MrEnum = 9,
            /// <summary>
            /// Enum NullEnum for Null
            /// </summary>
            [EnumMember(Value = "Null")]
            NullEnum = 10,
            /// <summary>
            /// Enum WksEnum for Wks
            /// </summary>
            [EnumMember(Value = "Wks")]
            WksEnum = 11,
            /// <summary>
            /// Enum PtrEnum for Ptr
            /// </summary>
            [EnumMember(Value = "Ptr")]
            PtrEnum = 12,
            /// <summary>
            /// Enum HinfoEnum for Hinfo
            /// </summary>
            [EnumMember(Value = "Hinfo")]
            HinfoEnum = 13,
            /// <summary>
            /// Enum MinfoEnum for Minfo
            /// </summary>
            [EnumMember(Value = "Minfo")]
            MinfoEnum = 14,
            /// <summary>
            /// Enum MxEnum for Mx
            /// </summary>
            [EnumMember(Value = "Mx")]
            MxEnum = 15,
            /// <summary>
            /// Enum TxtEnum for Txt
            /// </summary>
            [EnumMember(Value = "Txt")]
            TxtEnum = 16,
            /// <summary>
            /// Enum RpEnum for Rp
            /// </summary>
            [EnumMember(Value = "Rp")]
            RpEnum = 17,
            /// <summary>
            /// Enum AfsdbEnum for Afsdb
            /// </summary>
            [EnumMember(Value = "Afsdb")]
            AfsdbEnum = 18,
            /// <summary>
            /// Enum X25Enum for X25
            /// </summary>
            [EnumMember(Value = "X25")]
            X25Enum = 19,
            /// <summary>
            /// Enum IsdnEnum for Isdn
            /// </summary>
            [EnumMember(Value = "Isdn")]
            IsdnEnum = 20,
            /// <summary>
            /// Enum RtEnum for Rt
            /// </summary>
            [EnumMember(Value = "Rt")]
            RtEnum = 21,
            /// <summary>
            /// Enum NsapEnum for Nsap
            /// </summary>
            [EnumMember(Value = "Nsap")]
            NsapEnum = 22,
            /// <summary>
            /// Enum NsapPtrEnum for NsapPtr
            /// </summary>
            [EnumMember(Value = "NsapPtr")]
            NsapPtrEnum = 23,
            /// <summary>
            /// Enum SigEnum for Sig
            /// </summary>
            [EnumMember(Value = "Sig")]
            SigEnum = 24,
            /// <summary>
            /// Enum KeyEnum for Key
            /// </summary>
            [EnumMember(Value = "Key")]
            KeyEnum = 25,
            /// <summary>
            /// Enum PxEnum for Px
            /// </summary>
            [EnumMember(Value = "Px")]
            PxEnum = 26,
            /// <summary>
            /// Enum GposEnum for Gpos
            /// </summary>
            [EnumMember(Value = "Gpos")]
            GposEnum = 27,
            /// <summary>
            /// Enum AaaaEnum for Aaaa
            /// </summary>
            [EnumMember(Value = "Aaaa")]
            AaaaEnum = 28,
            /// <summary>
            /// Enum LocEnum for Loc
            /// </summary>
            [EnumMember(Value = "Loc")]
            LocEnum = 29,
            /// <summary>
            /// Enum NxtEnum for Nxt
            /// </summary>
            [EnumMember(Value = "Nxt")]
            NxtEnum = 30,
            /// <summary>
            /// Enum EidEnum for Eid
            /// </summary>
            [EnumMember(Value = "Eid")]
            EidEnum = 31,
            /// <summary>
            /// Enum NimlocEnum for Nimloc
            /// </summary>
            [EnumMember(Value = "Nimloc")]
            NimlocEnum = 32,
            /// <summary>
            /// Enum SrvEnum for Srv
            /// </summary>
            [EnumMember(Value = "Srv")]
            SrvEnum = 33,
            /// <summary>
            /// Enum AtmaEnum for Atma
            /// </summary>
            [EnumMember(Value = "Atma")]
            AtmaEnum = 34,
            /// <summary>
            /// Enum NaptrEnum for Naptr
            /// </summary>
            [EnumMember(Value = "Naptr")]
            NaptrEnum = 35,
            /// <summary>
            /// Enum KxEnum for Kx
            /// </summary>
            [EnumMember(Value = "Kx")]
            KxEnum = 36,
            /// <summary>
            /// Enum CertEnum for Cert
            /// </summary>
            [EnumMember(Value = "Cert")]
            CertEnum = 37,
            /// <summary>
            /// Enum A6Enum for A6
            /// </summary>
            [EnumMember(Value = "A6")]
            A6Enum = 38,
            /// <summary>
            /// Enum DnameEnum for Dname
            /// </summary>
            [EnumMember(Value = "Dname")]
            DnameEnum = 39,
            /// <summary>
            /// Enum SinkEnum for Sink
            /// </summary>
            [EnumMember(Value = "Sink")]
            SinkEnum = 40,
            /// <summary>
            /// Enum OptEnum for Opt
            /// </summary>
            [EnumMember(Value = "Opt")]
            OptEnum = 41,
            /// <summary>
            /// Enum AplEnum for Apl
            /// </summary>
            [EnumMember(Value = "Apl")]
            AplEnum = 42,
            /// <summary>
            /// Enum DsEnum for Ds
            /// </summary>
            [EnumMember(Value = "Ds")]
            DsEnum = 43,
            /// <summary>
            /// Enum SshfpEnum for Sshfp
            /// </summary>
            [EnumMember(Value = "Sshfp")]
            SshfpEnum = 44,
            /// <summary>
            /// Enum IpseckeyEnum for Ipseckey
            /// </summary>
            [EnumMember(Value = "Ipseckey")]
            IpseckeyEnum = 45,
            /// <summary>
            /// Enum RrsigEnum for Rrsig
            /// </summary>
            [EnumMember(Value = "Rrsig")]
            RrsigEnum = 46,
            /// <summary>
            /// Enum NsecEnum for Nsec
            /// </summary>
            [EnumMember(Value = "Nsec")]
            NsecEnum = 47,
            /// <summary>
            /// Enum DnskeyEnum for Dnskey
            /// </summary>
            [EnumMember(Value = "Dnskey")]
            DnskeyEnum = 48,
            /// <summary>
            /// Enum DhcidEnum for Dhcid
            /// </summary>
            [EnumMember(Value = "Dhcid")]
            DhcidEnum = 49,
            /// <summary>
            /// Enum Nsec3Enum for Nsec3
            /// </summary>
            [EnumMember(Value = "Nsec3")]
            Nsec3Enum = 50,
            /// <summary>
            /// Enum Nsec3paramEnum for Nsec3param
            /// </summary>
            [EnumMember(Value = "Nsec3param")]
            Nsec3paramEnum = 51,
            /// <summary>
            /// Enum TlsaEnum for Tlsa
            /// </summary>
            [EnumMember(Value = "Tlsa")]
            TlsaEnum = 52,
            /// <summary>
            /// Enum SmimeaEnum for Smimea
            /// </summary>
            [EnumMember(Value = "Smimea")]
            SmimeaEnum = 53,
            /// <summary>
            /// Enum HipEnum for Hip
            /// </summary>
            [EnumMember(Value = "Hip")]
            HipEnum = 54,
            /// <summary>
            /// Enum NinfoEnum for Ninfo
            /// </summary>
            [EnumMember(Value = "Ninfo")]
            NinfoEnum = 55,
            /// <summary>
            /// Enum RkeyEnum for Rkey
            /// </summary>
            [EnumMember(Value = "Rkey")]
            RkeyEnum = 56,
            /// <summary>
            /// Enum TalinkEnum for Talink
            /// </summary>
            [EnumMember(Value = "Talink")]
            TalinkEnum = 57,
            /// <summary>
            /// Enum CdsEnum for Cds
            /// </summary>
            [EnumMember(Value = "Cds")]
            CdsEnum = 58,
            /// <summary>
            /// Enum CdnskeyEnum for Cdnskey
            /// </summary>
            [EnumMember(Value = "Cdnskey")]
            CdnskeyEnum = 59,
            /// <summary>
            /// Enum OpenpgpkeyEnum for Openpgpkey
            /// </summary>
            [EnumMember(Value = "Openpgpkey")]
            OpenpgpkeyEnum = 60,
            /// <summary>
            /// Enum CsyncEnum for Csync
            /// </summary>
            [EnumMember(Value = "Csync")]
            CsyncEnum = 61,
            /// <summary>
            /// Enum ZonemdEnum for Zonemd
            /// </summary>
            [EnumMember(Value = "Zonemd")]
            ZonemdEnum = 62,
            /// <summary>
            /// Enum SpfEnum for Spf
            /// </summary>
            [EnumMember(Value = "Spf")]
            SpfEnum = 63,
            /// <summary>
            /// Enum UinfoEnum for Uinfo
            /// </summary>
            [EnumMember(Value = "Uinfo")]
            UinfoEnum = 64,
            /// <summary>
            /// Enum UidEnum for Uid
            /// </summary>
            [EnumMember(Value = "Uid")]
            UidEnum = 65,
            /// <summary>
            /// Enum GidEnum for Gid
            /// </summary>
            [EnumMember(Value = "Gid")]
            GidEnum = 66,
            /// <summary>
            /// Enum UnspecEnum for Unspec
            /// </summary>
            [EnumMember(Value = "Unspec")]
            UnspecEnum = 67,
            /// <summary>
            /// Enum NidEnum for Nid
            /// </summary>
            [EnumMember(Value = "Nid")]
            NidEnum = 68,
            /// <summary>
            /// Enum L32Enum for L32
            /// </summary>
            [EnumMember(Value = "L32")]
            L32Enum = 69,
            /// <summary>
            /// Enum L64Enum for L64
            /// </summary>
            [EnumMember(Value = "L64")]
            L64Enum = 70,
            /// <summary>
            /// Enum LpEnum for Lp
            /// </summary>
            [EnumMember(Value = "Lp")]
            LpEnum = 71,
            /// <summary>
            /// Enum Eui48Enum for Eui48
            /// </summary>
            [EnumMember(Value = "Eui48")]
            Eui48Enum = 72,
            /// <summary>
            /// Enum Eui64Enum for Eui64
            /// </summary>
            [EnumMember(Value = "Eui64")]
            Eui64Enum = 73,
            /// <summary>
            /// Enum TkeyEnum for Tkey
            /// </summary>
            [EnumMember(Value = "Tkey")]
            TkeyEnum = 74,
            /// <summary>
            /// Enum TsigEnum for Tsig
            /// </summary>
            [EnumMember(Value = "Tsig")]
            TsigEnum = 75,
            /// <summary>
            /// Enum IxfrEnum for Ixfr
            /// </summary>
            [EnumMember(Value = "Ixfr")]
            IxfrEnum = 76,
            /// <summary>
            /// Enum AxfrEnum for Axfr
            /// </summary>
            [EnumMember(Value = "Axfr")]
            AxfrEnum = 77,
            /// <summary>
            /// Enum MailbEnum for Mailb
            /// </summary>
            [EnumMember(Value = "Mailb")]
            MailbEnum = 78,
            /// <summary>
            /// Enum MailaEnum for Maila
            /// </summary>
            [EnumMember(Value = "Maila")]
            MailaEnum = 79,
            /// <summary>
            /// Enum AnyEnum for Any
            /// </summary>
            [EnumMember(Value = "Any")]
            AnyEnum = 80,
            /// <summary>
            /// Enum UriEnum for Uri
            /// </summary>
            [EnumMember(Value = "Uri")]
            UriEnum = 81,
            /// <summary>
            /// Enum CaaEnum for Caa
            /// </summary>
            [EnumMember(Value = "Caa")]
            CaaEnum = 82,
            /// <summary>
            /// Enum AvcEnum for Avc
            /// </summary>
            [EnumMember(Value = "Avc")]
            AvcEnum = 83,
            /// <summary>
            /// Enum DoaEnum for Doa
            /// </summary>
            [EnumMember(Value = "Doa")]
            DoaEnum = 84,
            /// <summary>
            /// Enum AmtrelayEnum for Amtrelay
            /// </summary>
            [EnumMember(Value = "Amtrelay")]
            AmtrelayEnum = 85,
            /// <summary>
            /// Enum TaEnum for Ta
            /// </summary>
            [EnumMember(Value = "Ta")]
            TaEnum = 86,
            /// <summary>
            /// Enum DlvEnum for Dlv
            /// </summary>
            [EnumMember(Value = "Dlv")]
            DlvEnum = 87        }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [Required]

        [DataMember(Name="type")]
        public TypeEnum? Type { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [Required]

        [DataMember(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DnsQuestion {\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
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
            return obj.GetType() == GetType() && Equals((DnsQuestion)obj);
        }

        /// <summary>
        /// Returns true if DnsQuestion instances are equal
        /// </summary>
        /// <param name="other">Instance of DnsQuestion to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DnsQuestion other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Type == other.Type ||
                    Type != null &&
                    Type.Equals(other.Type)
                ) && 
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
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
                    if (Type != null)
                    hashCode = hashCode * 59 + Type.GetHashCode();
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(DnsQuestion left, DnsQuestion right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DnsQuestion left, DnsQuestion right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
