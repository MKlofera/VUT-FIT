using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Pleskalizer.Common.Enums;

        /// <summary>
        /// Gets or Sets ProtocolL4
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum ProtocolL4Enum
        {
            /// <summary>
            /// Enum HOPOPTEnum for HOPOPT
            /// </summary>
            [EnumMember(Value = "HOPOPT")]
            HOPOPTEnum = 0,
            /// <summary>
            /// Enum ICMPEnum for ICMP
            /// </summary>
            [EnumMember(Value = "ICMP")]
            ICMPEnum = 1,
            /// <summary>
            /// Enum IGMPEnum for IGMP
            /// </summary>
            [EnumMember(Value = "IGMP")]
            IGMPEnum = 2,
            /// <summary>
            /// Enum GGPEnum for GGP
            /// </summary>
            [EnumMember(Value = "GGP")]
            GGPEnum = 3,
            /// <summary>
            /// Enum IPv4Enum for IPv4
            /// </summary>
            [EnumMember(Value = "IPv4")]
            IPv4Enum = 4,
            /// <summary>
            /// Enum STEnum for ST
            /// </summary>
            [EnumMember(Value = "ST")]
            STEnum = 5,
            /// <summary>
            /// Enum TCPEnum for TCP
            /// </summary>
            [EnumMember(Value = "TCP")]
            TCPEnum = 6,
            /// <summary>
            /// Enum CBTEnum for CBT
            /// </summary>
            [EnumMember(Value = "CBT")]
            CBTEnum = 7,
            /// <summary>
            /// Enum EGPEnum for EGP
            /// </summary>
            [EnumMember(Value = "EGP")]
            EGPEnum = 8,
            /// <summary>
            /// Enum IGPEnum for IGP
            /// </summary>
            [EnumMember(Value = "IGP")]
            IGPEnum = 9,
            /// <summary>
            /// Enum BBNRCCMONEnum for BBN_RCC_MON
            /// </summary>
            [EnumMember(Value = "BBN_RCC_MON")]
            BBNRCCMONEnum = 10,
            /// <summary>
            /// Enum NVPIIEnum for NVP_II
            /// </summary>
            [EnumMember(Value = "NVP_II")]
            NVPIIEnum = 11,
            /// <summary>
            /// Enum PUPEnum for PUP
            /// </summary>
            [EnumMember(Value = "PUP")]
            PUPEnum = 12,
            /// <summary>
            /// Enum ARGUSEnum for ARGUS
            /// </summary>
            [EnumMember(Value = "ARGUS")]
            ARGUSEnum = 13,
            /// <summary>
            /// Enum EMCONEnum for EMCON
            /// </summary>
            [EnumMember(Value = "EMCON")]
            EMCONEnum = 14,
            /// <summary>
            /// Enum XNETEnum for XNET
            /// </summary>
            [EnumMember(Value = "XNET")]
            XNETEnum = 15,
            /// <summary>
            /// Enum CHAOSEnum for CHAOS
            /// </summary>
            [EnumMember(Value = "CHAOS")]
            CHAOSEnum = 16,
            /// <summary>
            /// Enum UDPEnum for UDP
            /// </summary>
            [EnumMember(Value = "UDP")]
            UDPEnum = 17,
            /// <summary>
            /// Enum MUXEnum for MUX
            /// </summary>
            [EnumMember(Value = "MUX")]
            MUXEnum = 18,
            /// <summary>
            /// Enum DCNMEASEnum for DCN_MEAS
            /// </summary>
            [EnumMember(Value = "DCN_MEAS")]
            DCNMEASEnum = 19,
            /// <summary>
            /// Enum HMPEnum for HMP
            /// </summary>
            [EnumMember(Value = "HMP")]
            HMPEnum = 20,
            /// <summary>
            /// Enum PRMEnum for PRM
            /// </summary>
            [EnumMember(Value = "PRM")]
            PRMEnum = 21,
            /// <summary>
            /// Enum XNSIDPEnum for XNS_IDP
            /// </summary>
            [EnumMember(Value = "XNS_IDP")]
            XNSIDPEnum = 22,
            /// <summary>
            /// Enum TRUNK1Enum for TRUNK_1
            /// </summary>
            [EnumMember(Value = "TRUNK_1")]
            TRUNK1Enum = 23,
            /// <summary>
            /// Enum TRUNK2Enum for TRUNK_2
            /// </summary>
            [EnumMember(Value = "TRUNK_2")]
            TRUNK2Enum = 24,
            /// <summary>
            /// Enum LEAF1Enum for LEAF_1
            /// </summary>
            [EnumMember(Value = "LEAF_1")]
            LEAF1Enum = 25,
            /// <summary>
            /// Enum LEAF2Enum for LEAF_2
            /// </summary>
            [EnumMember(Value = "LEAF_2")]
            LEAF2Enum = 26,
            /// <summary>
            /// Enum RDPEnum for RDP
            /// </summary>
            [EnumMember(Value = "RDP")]
            RDPEnum = 27,
            /// <summary>
            /// Enum IRTPEnum for IRTP
            /// </summary>
            [EnumMember(Value = "IRTP")]
            IRTPEnum = 28,
            /// <summary>
            /// Enum ISOTP4Enum for ISO_TP4
            /// </summary>
            [EnumMember(Value = "ISO_TP4")]
            ISOTP4Enum = 29,
            /// <summary>
            /// Enum NETBLTEnum for NETBLT
            /// </summary>
            [EnumMember(Value = "NETBLT")]
            NETBLTEnum = 30,
            /// <summary>
            /// Enum MFENSPEnum for MFE_NSP
            /// </summary>
            [EnumMember(Value = "MFE_NSP")]
            MFENSPEnum = 31,
            /// <summary>
            /// Enum MERITINPEnum for MERIT_INP
            /// </summary>
            [EnumMember(Value = "MERIT_INP")]
            MERITINPEnum = 32,
            /// <summary>
            /// Enum DCCPEnum for DCCP
            /// </summary>
            [EnumMember(Value = "DCCP")]
            DCCPEnum = 33,
            /// <summary>
            /// Enum ThreePCEnum for ThreePC
            /// </summary>
            [EnumMember(Value = "ThreePC")]
            ThreePCEnum = 34,
            /// <summary>
            /// Enum IDPREnum for IDPR
            /// </summary>
            [EnumMember(Value = "IDPR")]
            IDPREnum = 35,
            /// <summary>
            /// Enum XTPEnum for XTP
            /// </summary>
            [EnumMember(Value = "XTP")]
            XTPEnum = 36,
            /// <summary>
            /// Enum DDPEnum for DDP
            /// </summary>
            [EnumMember(Value = "DDP")]
            DDPEnum = 37,
            /// <summary>
            /// Enum IDPRCMTPEnum for IDPR_CMTP
            /// </summary>
            [EnumMember(Value = "IDPR_CMTP")]
            IDPRCMTPEnum = 38,
            /// <summary>
            /// Enum TPEnum for TP
            /// </summary>
            [EnumMember(Value = "TP")]
            TPEnum = 39,
            /// <summary>
            /// Enum ILEnum for IL
            /// </summary>
            [EnumMember(Value = "IL")]
            ILEnum = 40,
            /// <summary>
            /// Enum IPv6Enum for IPv6
            /// </summary>
            [EnumMember(Value = "IPv6")]
            IPv6Enum = 41,
            /// <summary>
            /// Enum SDRPEnum for SDRP
            /// </summary>
            [EnumMember(Value = "SDRP")]
            SDRPEnum = 42,
            /// <summary>
            /// Enum IPv6RouteEnum for IPv6_Route
            /// </summary>
            [EnumMember(Value = "IPv6_Route")]
            IPv6RouteEnum = 43,
            /// <summary>
            /// Enum IPv6FragEnum for IPv6_Frag
            /// </summary>
            [EnumMember(Value = "IPv6_Frag")]
            IPv6FragEnum = 44,
            /// <summary>
            /// Enum IDRPEnum for IDRP
            /// </summary>
            [EnumMember(Value = "IDRP")]
            IDRPEnum = 45,
            /// <summary>
            /// Enum RSVPEnum for RSVP
            /// </summary>
            [EnumMember(Value = "RSVP")]
            RSVPEnum = 46,
            /// <summary>
            /// Enum GREEnum for GRE
            /// </summary>
            [EnumMember(Value = "GRE")]
            GREEnum = 47,
            /// <summary>
            /// Enum DSREnum for DSR
            /// </summary>
            [EnumMember(Value = "DSR")]
            DSREnum = 48,
            /// <summary>
            /// Enum BNAEnum for BNA
            /// </summary>
            [EnumMember(Value = "BNA")]
            BNAEnum = 49,
            /// <summary>
            /// Enum ESPEnum for ESP
            /// </summary>
            [EnumMember(Value = "ESP")]
            ESPEnum = 50,
            /// <summary>
            /// Enum AHEnum for AH
            /// </summary>
            [EnumMember(Value = "AH")]
            AHEnum = 51,
            /// <summary>
            /// Enum INLSPEnum for I_NLSP
            /// </summary>
            [EnumMember(Value = "I_NLSP")]
            INLSPEnum = 52,
            /// <summary>
            /// Enum SWIPEEnum for SWIPE
            /// </summary>
            [EnumMember(Value = "SWIPE")]
            SWIPEEnum = 53,
            /// <summary>
            /// Enum NARPEnum for NARP
            /// </summary>
            [EnumMember(Value = "NARP")]
            NARPEnum = 54,
            /// <summary>
            /// Enum MOBILEEnum for MOBILE
            /// </summary>
            [EnumMember(Value = "MOBILE")]
            MOBILEEnum = 55,
            /// <summary>
            /// Enum TLSPEnum for TLSP
            /// </summary>
            [EnumMember(Value = "TLSP")]
            TLSPEnum = 56,
            /// <summary>
            /// Enum SKIPEnum for SKIP
            /// </summary>
            [EnumMember(Value = "SKIP")]
            SKIPEnum = 57,
            /// <summary>
            /// Enum IPv6ICMPEnum for IPv6_ICMP
            /// </summary>
            [EnumMember(Value = "IPv6_ICMP")]
            IPv6ICMPEnum = 58,
            /// <summary>
            /// Enum IPv6NoNxtEnum for IPv6_NoNxt
            /// </summary>
            [EnumMember(Value = "IPv6_NoNxt")]
            IPv6NoNxtEnum = 59,
            /// <summary>
            /// Enum IPv6OptsEnum for IPv6_Opts
            /// </summary>
            [EnumMember(Value = "IPv6_Opts")]
            IPv6OptsEnum = 60,
            /// <summary>
            /// Enum AnyHostInternalEnum for AnyHostInternal
            /// </summary>
            [EnumMember(Value = "AnyHostInternal")]
            AnyHostInternalEnum = 61,
            /// <summary>
            /// Enum CFTPEnum for CFTP
            /// </summary>
            [EnumMember(Value = "CFTP")]
            CFTPEnum = 62,
            /// <summary>
            /// Enum AnyLocalNetworkEnum for AnyLocalNetwork
            /// </summary>
            [EnumMember(Value = "AnyLocalNetwork")]
            AnyLocalNetworkEnum = 63,
            /// <summary>
            /// Enum SATEXPAKEnum for SAT_EXPAK
            /// </summary>
            [EnumMember(Value = "SAT_EXPAK")]
            SATEXPAKEnum = 64,
            /// <summary>
            /// Enum KRYPTOLANEnum for KRYPTOLAN
            /// </summary>
            [EnumMember(Value = "KRYPTOLAN")]
            KRYPTOLANEnum = 65,
            /// <summary>
            /// Enum RVDEnum for RVD
            /// </summary>
            [EnumMember(Value = "RVD")]
            RVDEnum = 66,
            /// <summary>
            /// Enum IPPCEnum for IPPC
            /// </summary>
            [EnumMember(Value = "IPPC")]
            IPPCEnum = 67,
            /// <summary>
            /// Enum AnyDistributedFileSystemEnum for AnyDistributedFileSystem
            /// </summary>
            [EnumMember(Value = "AnyDistributedFileSystem")]
            AnyDistributedFileSystemEnum = 68,
            /// <summary>
            /// Enum SATMONEnum for SAT_MON
            /// </summary>
            [EnumMember(Value = "SAT_MON")]
            SATMONEnum = 69,
            /// <summary>
            /// Enum VISAEnum for VISA
            /// </summary>
            [EnumMember(Value = "VISA")]
            VISAEnum = 70,
            /// <summary>
            /// Enum IPCVEnum for IPCV
            /// </summary>
            [EnumMember(Value = "IPCV")]
            IPCVEnum = 71,
            /// <summary>
            /// Enum CPNXEnum for CPNX
            /// </summary>
            [EnumMember(Value = "CPNX")]
            CPNXEnum = 72,
            /// <summary>
            /// Enum CPHBEnum for CPHB
            /// </summary>
            [EnumMember(Value = "CPHB")]
            CPHBEnum = 73,
            /// <summary>
            /// Enum WSNEnum for WSN
            /// </summary>
            [EnumMember(Value = "WSN")]
            WSNEnum = 74,
            /// <summary>
            /// Enum PVPEnum for PVP
            /// </summary>
            [EnumMember(Value = "PVP")]
            PVPEnum = 75,
            /// <summary>
            /// Enum BRSATMONEnum for BR_SAT_MON
            /// </summary>
            [EnumMember(Value = "BR_SAT_MON")]
            BRSATMONEnum = 76,
            /// <summary>
            /// Enum SUNNDEnum for SUN_ND
            /// </summary>
            [EnumMember(Value = "SUN_ND")]
            SUNNDEnum = 77,
            /// <summary>
            /// Enum WBMONEnum for WB_MON
            /// </summary>
            [EnumMember(Value = "WB_MON")]
            WBMONEnum = 78,
            /// <summary>
            /// Enum WBEXPAKEnum for WB_EXPAK
            /// </summary>
            [EnumMember(Value = "WB_EXPAK")]
            WBEXPAKEnum = 79,
            /// <summary>
            /// Enum ISOIPEnum for ISO_IP
            /// </summary>
            [EnumMember(Value = "ISO_IP")]
            ISOIPEnum = 80,
            /// <summary>
            /// Enum VMTPEnum for VMTP
            /// </summary>
            [EnumMember(Value = "VMTP")]
            VMTPEnum = 81,
            /// <summary>
            /// Enum SECUREVMTPEnum for SECURE_VMTP
            /// </summary>
            [EnumMember(Value = "SECURE_VMTP")]
            SECUREVMTPEnum = 82,
            /// <summary>
            /// Enum VINESEnum for VINES
            /// </summary>
            [EnumMember(Value = "VINES")]
            VINESEnum = 83,
            /// <summary>
            /// Enum TTPORIPTMEnum for TTP_OR_IPTM
            /// </summary>
            [EnumMember(Value = "TTP_OR_IPTM")]
            TTPORIPTMEnum = 84,
            /// <summary>
            /// Enum NSFNETIGPEnum for NSFNET_IGP
            /// </summary>
            [EnumMember(Value = "NSFNET_IGP")]
            NSFNETIGPEnum = 85,
            /// <summary>
            /// Enum DGPEnum for DGP
            /// </summary>
            [EnumMember(Value = "DGP")]
            DGPEnum = 86,
            /// <summary>
            /// Enum TCFEnum for TCF
            /// </summary>
            [EnumMember(Value = "TCF")]
            TCFEnum = 87,
            /// <summary>
            /// Enum EIGRPEnum for EIGRP
            /// </summary>
            [EnumMember(Value = "EIGRP")]
            EIGRPEnum = 88,
            /// <summary>
            /// Enum OSPFIGPEnum for OSPFIGP
            /// </summary>
            [EnumMember(Value = "OSPFIGP")]
            OSPFIGPEnum = 89,
            /// <summary>
            /// Enum SpriteRPCEnum for Sprite_RPC
            /// </summary>
            [EnumMember(Value = "Sprite_RPC")]
            SpriteRPCEnum = 90,
            /// <summary>
            /// Enum LARPEnum for LARP
            /// </summary>
            [EnumMember(Value = "LARP")]
            LARPEnum = 91,
            /// <summary>
            /// Enum MTPEnum for MTP
            /// </summary>
            [EnumMember(Value = "MTP")]
            MTPEnum = 92,
            /// <summary>
            /// Enum AX25Enum for AX25
            /// </summary>
            [EnumMember(Value = "AX25")]
            AX25Enum = 93,
            /// <summary>
            /// Enum IPIPEnum for IPIP
            /// </summary>
            [EnumMember(Value = "IPIP")]
            IPIPEnum = 94,
            /// <summary>
            /// Enum MICPEnum for MICP
            /// </summary>
            [EnumMember(Value = "MICP")]
            MICPEnum = 95,
            /// <summary>
            /// Enum SCCSPEnum for SCC_SP
            /// </summary>
            [EnumMember(Value = "SCC_SP")]
            SCCSPEnum = 96,
            /// <summary>
            /// Enum ETHERIPEnum for ETHERIP
            /// </summary>
            [EnumMember(Value = "ETHERIP")]
            ETHERIPEnum = 97,
            /// <summary>
            /// Enum ENCAPEnum for ENCAP
            /// </summary>
            [EnumMember(Value = "ENCAP")]
            ENCAPEnum = 98,
            /// <summary>
            /// Enum AnyPrivateEncryptionSchemeEnum for AnyPrivateEncryptionScheme
            /// </summary>
            [EnumMember(Value = "AnyPrivateEncryptionScheme")]
            AnyPrivateEncryptionSchemeEnum = 99,
            /// <summary>
            /// Enum GMTPEnum for GMTP
            /// </summary>
            [EnumMember(Value = "GMTP")]
            GMTPEnum = 100,
            /// <summary>
            /// Enum IFMPEnum for IFMP
            /// </summary>
            [EnumMember(Value = "IFMP")]
            IFMPEnum = 101,
            /// <summary>
            /// Enum PNNIEnum for PNNI
            /// </summary>
            [EnumMember(Value = "PNNI")]
            PNNIEnum = 102,
            /// <summary>
            /// Enum PIMEnum for PIM
            /// </summary>
            [EnumMember(Value = "PIM")]
            PIMEnum = 103,
            /// <summary>
            /// Enum ARISEnum for ARIS
            /// </summary>
            [EnumMember(Value = "ARIS")]
            ARISEnum = 104,
            /// <summary>
            /// Enum SCPSEnum for SCPS
            /// </summary>
            [EnumMember(Value = "SCPS")]
            SCPSEnum = 105,
            /// <summary>
            /// Enum QNXEnum for QNX
            /// </summary>
            [EnumMember(Value = "QNX")]
            QNXEnum = 106,
            /// <summary>
            /// Enum ANEnum for AN
            /// </summary>
            [EnumMember(Value = "AN")]
            ANEnum = 107,
            /// <summary>
            /// Enum IPCompEnum for IPComp
            /// </summary>
            [EnumMember(Value = "IPComp")]
            IPCompEnum = 108,
            /// <summary>
            /// Enum SNPEnum for SNP
            /// </summary>
            [EnumMember(Value = "SNP")]
            SNPEnum = 109,
            /// <summary>
            /// Enum CompaqPeerEnum for Compaq_Peer
            /// </summary>
            [EnumMember(Value = "Compaq_Peer")]
            CompaqPeerEnum = 110,
            /// <summary>
            /// Enum IPXInIPEnum for IPX_in_IP
            /// </summary>
            [EnumMember(Value = "IPX_in_IP")]
            IPXInIPEnum = 111,
            /// <summary>
            /// Enum VRRPEnum for VRRP
            /// </summary>
            [EnumMember(Value = "VRRP")]
            VRRPEnum = 112,
            /// <summary>
            /// Enum PGMEnum for PGM
            /// </summary>
            [EnumMember(Value = "PGM")]
            PGMEnum = 113,
            /// <summary>
            /// Enum AnyZeroHopEnum for AnyZeroHop
            /// </summary>
            [EnumMember(Value = "AnyZeroHop")]
            AnyZeroHopEnum = 114,
            /// <summary>
            /// Enum L2TPEnum for L2TP
            /// </summary>
            [EnumMember(Value = "L2TP")]
            L2TPEnum = 115,
            /// <summary>
            /// Enum DDXEnum for DDX
            /// </summary>
            [EnumMember(Value = "DDX")]
            DDXEnum = 116,
            /// <summary>
            /// Enum IATPEnum for IATP
            /// </summary>
            [EnumMember(Value = "IATP")]
            IATPEnum = 117,
            /// <summary>
            /// Enum STPEnum for STP
            /// </summary>
            [EnumMember(Value = "STP")]
            STPEnum = 118,
            /// <summary>
            /// Enum SRPEnum for SRP
            /// </summary>
            [EnumMember(Value = "SRP")]
            SRPEnum = 119,
            /// <summary>
            /// Enum UTIEnum for UTI
            /// </summary>
            [EnumMember(Value = "UTI")]
            UTIEnum = 120,
            /// <summary>
            /// Enum SMPEnum for SMP
            /// </summary>
            [EnumMember(Value = "SMP")]
            SMPEnum = 121,
            /// <summary>
            /// Enum SMEnum for SM
            /// </summary>
            [EnumMember(Value = "SM")]
            SMEnum = 122,
            /// <summary>
            /// Enum PTPEnum for PTP
            /// </summary>
            [EnumMember(Value = "PTP")]
            PTPEnum = 123,
            /// <summary>
            /// Enum ISISoverIPV4Enum for ISISoverIPV4
            /// </summary>
            [EnumMember(Value = "ISISoverIPV4")]
            ISISoverIPV4Enum = 124,
            /// <summary>
            /// Enum FIREEnum for FIRE
            /// </summary>
            [EnumMember(Value = "FIRE")]
            FIREEnum = 125,
            /// <summary>
            /// Enum CRTPEnum for CRTP
            /// </summary>
            [EnumMember(Value = "CRTP")]
            CRTPEnum = 126,
            /// <summary>
            /// Enum CRUDPEnum for CRUDP
            /// </summary>
            [EnumMember(Value = "CRUDP")]
            CRUDPEnum = 127,
            /// <summary>
            /// Enum SSCOPMCEEnum for SSCOPMCE
            /// </summary>
            [EnumMember(Value = "SSCOPMCE")]
            SSCOPMCEEnum = 128,
            /// <summary>
            /// Enum IPLTEnum for IPLT
            /// </summary>
            [EnumMember(Value = "IPLT")]
            IPLTEnum = 129,
            /// <summary>
            /// Enum SPSEnum for SPS
            /// </summary>
            [EnumMember(Value = "SPS")]
            SPSEnum = 130,
            /// <summary>
            /// Enum PIPEEnum for PIPE
            /// </summary>
            [EnumMember(Value = "PIPE")]
            PIPEEnum = 131,
            /// <summary>
            /// Enum SCTPEnum for SCTP
            /// </summary>
            [EnumMember(Value = "SCTP")]
            SCTPEnum = 132,
            /// <summary>
            /// Enum FCEnum for FC
            /// </summary>
            [EnumMember(Value = "FC")]
            FCEnum = 133,
            /// <summary>
            /// Enum RSVPE2EIGNOREEnum for RSVP_E2E_IGNORE
            /// </summary>
            [EnumMember(Value = "RSVP_E2E_IGNORE")]
            RSVPE2EIGNOREEnum = 134,
            /// <summary>
            /// Enum MobilityHeaderEnum for MobilityHeader
            /// </summary>
            [EnumMember(Value = "MobilityHeader")]
            MobilityHeaderEnum = 135,
            /// <summary>
            /// Enum UDPLiteEnum for UDPLite
            /// </summary>
            [EnumMember(Value = "UDPLite")]
            UDPLiteEnum = 136,
            /// <summary>
            /// Enum MPLSInIPEnum for MPLS_in_IP
            /// </summary>
            [EnumMember(Value = "MPLS_in_IP")]
            MPLSInIPEnum = 137,
            /// <summary>
            /// Enum ManetEnum for manet
            /// </summary>
            [EnumMember(Value = "manet")]
            ManetEnum = 138,
            /// <summary>
            /// Enum HIPEnum for HIP
            /// </summary>
            [EnumMember(Value = "HIP")]
            HIPEnum = 139,
            /// <summary>
            /// Enum Shim6Enum for Shim6
            /// </summary>
            [EnumMember(Value = "Shim6")]
            Shim6Enum = 140,
            /// <summary>
            /// Enum WESPEnum for WESP
            /// </summary>
            [EnumMember(Value = "WESP")]
            WESPEnum = 141,
            /// <summary>
            /// Enum ROHCEnum for ROHC
            /// </summary>
            [EnumMember(Value = "ROHC")]
            ROHCEnum = 142,
            /// <summary>
            /// Enum EthernetEnum for Ethernet
            /// </summary>
            [EnumMember(Value = "Ethernet")]
            EthernetEnum = 143,
            /// <summary>
            /// Enum UnassignedEnum for Unassigned
            /// </summary>
            [EnumMember(Value = "Unassigned")]
            UnassignedEnum = 144,
            /// <summary>
            /// Enum Experimental1Enum for Experimental1
            /// </summary>
            [EnumMember(Value = "Experimental1")]
            Experimental1Enum = 145,
            /// <summary>
            /// Enum Experimental2Enum for Experimental2
            /// </summary>
            [EnumMember(Value = "Experimental2")]
            Experimental2Enum = 146,
            /// <summary>
            /// Enum UnknownEnum for Unknown
            /// </summary>
            [EnumMember(Value = "Unknown")]
            UnknownEnum = 147        }