using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Pleskalizer.Common.Enums;

/// <summary>
/// Gets or Sets ProtocolL3
/// </summary>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum ProtocolL3Enum
        {
            /// <summary>
            /// Enum NoneEnum for None
            /// </summary>
            [EnumMember(Value = "None")]
            NoneEnum = 0,
            /// <summary>
            /// Enum LoopEnum for Loop
            /// </summary>
            [EnumMember(Value = "Loop")]
            LoopEnum = 1,
            /// <summary>
            /// Enum EchoEnum for Echo
            /// </summary>
            [EnumMember(Value = "Echo")]
            EchoEnum = 2,
            /// <summary>
            /// Enum IPv4Enum for IPv4
            /// </summary>
            [EnumMember(Value = "IPv4")]
            IPv4Enum = 3,
            /// <summary>
            /// Enum ArpEnum for Arp
            /// </summary>
            [EnumMember(Value = "Arp")]
            ArpEnum = 4,
            /// <summary>
            /// Enum WakeOnLanEnum for WakeOnLan
            /// </summary>
            [EnumMember(Value = "WakeOnLan")]
            WakeOnLanEnum = 5,
            /// <summary>
            /// Enum ReverseArpEnum for ReverseArp
            /// </summary>
            [EnumMember(Value = "ReverseArp")]
            ReverseArpEnum = 6,
            /// <summary>
            /// Enum AppleTalkEnum for AppleTalk
            /// </summary>
            [EnumMember(Value = "AppleTalk")]
            AppleTalkEnum = 7,
            /// <summary>
            /// Enum AppleTalkArpEnum for AppleTalkArp
            /// </summary>
            [EnumMember(Value = "AppleTalkArp")]
            AppleTalkArpEnum = 8,
            /// <summary>
            /// Enum VLanTaggedFrameEnum for VLanTaggedFrame
            /// </summary>
            [EnumMember(Value = "VLanTaggedFrame")]
            VLanTaggedFrameEnum = 9,
            /// <summary>
            /// Enum NovellInterNetworkPacketExchangeEnum for NovellInterNetworkPacketExchange
            /// </summary>
            [EnumMember(Value = "NovellInterNetworkPacketExchange")]
            NovellInterNetworkPacketExchangeEnum = 10,
            /// <summary>
            /// Enum NovellEnum for Novell
            /// </summary>
            [EnumMember(Value = "Novell")]
            NovellEnum = 11,
            /// <summary>
            /// Enum IPv6Enum for IPv6
            /// </summary>
            [EnumMember(Value = "IPv6")]
            IPv6Enum = 12,
            /// <summary>
            /// Enum MacControlEnum for MacControl
            /// </summary>
            [EnumMember(Value = "MacControl")]
            MacControlEnum = 13,
            /// <summary>
            /// Enum CobraNetEnum for CobraNet
            /// </summary>
            [EnumMember(Value = "CobraNet")]
            CobraNetEnum = 14,
            /// <summary>
            /// Enum MultiProtocolLabelSwitchingUnicastEnum for MultiProtocolLabelSwitchingUnicast
            /// </summary>
            [EnumMember(Value = "MultiProtocolLabelSwitchingUnicast")]
            MultiProtocolLabelSwitchingUnicastEnum = 15,
            /// <summary>
            /// Enum MultiProtocolLabelSwitchingMulticastEnum for MultiProtocolLabelSwitchingMulticast
            /// </summary>
            [EnumMember(Value = "MultiProtocolLabelSwitchingMulticast")]
            MultiProtocolLabelSwitchingMulticastEnum = 16,
            /// <summary>
            /// Enum PointToPointProtocolOverEthernetDiscoveryStageEnum for PointToPointProtocolOverEthernetDiscoveryStage
            /// </summary>
            [EnumMember(Value = "PointToPointProtocolOverEthernetDiscoveryStage")]
            PointToPointProtocolOverEthernetDiscoveryStageEnum = 17,
            /// <summary>
            /// Enum PointToPointProtocolOverEthernetSessionStageEnum for PointToPointProtocolOverEthernetSessionStage
            /// </summary>
            [EnumMember(Value = "PointToPointProtocolOverEthernetSessionStage")]
            PointToPointProtocolOverEthernetSessionStageEnum = 18,
            /// <summary>
            /// Enum ExtensibleAuthenticationProtocolOverLanEnum for ExtensibleAuthenticationProtocolOverLan
            /// </summary>
            [EnumMember(Value = "ExtensibleAuthenticationProtocolOverLan")]
            ExtensibleAuthenticationProtocolOverLanEnum = 19,
            /// <summary>
            /// Enum ProfinetEnum for Profinet
            /// </summary>
            [EnumMember(Value = "Profinet")]
            ProfinetEnum = 20,
            /// <summary>
            /// Enum HyperScsiEnum for HyperScsi
            /// </summary>
            [EnumMember(Value = "HyperScsi")]
            HyperScsiEnum = 21,
            /// <summary>
            /// Enum AtaOverEthernetEnum for AtaOverEthernet
            /// </summary>
            [EnumMember(Value = "AtaOverEthernet")]
            AtaOverEthernetEnum = 22,
            /// <summary>
            /// Enum EtherCatProtocolEnum for EtherCatProtocol
            /// </summary>
            [EnumMember(Value = "EtherCatProtocol")]
            EtherCatProtocolEnum = 23,
            /// <summary>
            /// Enum ProviderBridgingEnum for ProviderBridging
            /// </summary>
            [EnumMember(Value = "ProviderBridging")]
            ProviderBridgingEnum = 24,
            /// <summary>
            /// Enum AvbTransportProtocolEnum for AvbTransportProtocol
            /// </summary>
            [EnumMember(Value = "AvbTransportProtocol")]
            AvbTransportProtocolEnum = 25,
            /// <summary>
            /// Enum LLDPEnum for LLDP
            /// </summary>
            [EnumMember(Value = "LLDP")]
            LLDPEnum = 26,
            /// <summary>
            /// Enum SerialRealTimeCommunicationSystemIiiEnum for SerialRealTimeCommunicationSystemIii
            /// </summary>
            [EnumMember(Value = "SerialRealTimeCommunicationSystemIii")]
            SerialRealTimeCommunicationSystemIiiEnum = 27,
            /// <summary>
            /// Enum CircuitEmulationServicesOverEthernetEnum for CircuitEmulationServicesOverEthernet
            /// </summary>
            [EnumMember(Value = "CircuitEmulationServicesOverEthernet")]
            CircuitEmulationServicesOverEthernetEnum = 28,
            /// <summary>
            /// Enum HomePlugEnum for HomePlug
            /// </summary>
            [EnumMember(Value = "HomePlug")]
            HomePlugEnum = 29,
            /// <summary>
            /// Enum MacSecurityEnum for MacSecurity
            /// </summary>
            [EnumMember(Value = "MacSecurity")]
            MacSecurityEnum = 30,
            /// <summary>
            /// Enum PrecisionTimeProtocolEnum for PrecisionTimeProtocol
            /// </summary>
            [EnumMember(Value = "PrecisionTimeProtocol")]
            PrecisionTimeProtocolEnum = 31,
            /// <summary>
            /// Enum ConnectivityFaultManagementOrOperationsAdministrationManagementEnum for ConnectivityFaultManagementOrOperationsAdministrationManagement
            /// </summary>
            [EnumMember(Value = "ConnectivityFaultManagementOrOperationsAdministrationManagement")]
            ConnectivityFaultManagementOrOperationsAdministrationManagementEnum = 32,
            /// <summary>
            /// Enum FibreChannelOverEthernetEnum for FibreChannelOverEthernet
            /// </summary>
            [EnumMember(Value = "FibreChannelOverEthernet")]
            FibreChannelOverEthernetEnum = 33,
            /// <summary>
            /// Enum FibreChannelOverEthernetInitializationProtocolEnum for FibreChannelOverEthernetInitializationProtocol
            /// </summary>
            [EnumMember(Value = "FibreChannelOverEthernetInitializationProtocol")]
            FibreChannelOverEthernetInitializationProtocolEnum = 34,
            /// <summary>
            /// Enum QInQEnum for QInQ
            /// </summary>
            [EnumMember(Value = "QInQ")]
            QInQEnum = 35,
            /// <summary>
            /// Enum VeritasLowLatencyTransportEnum for VeritasLowLatencyTransport
            /// </summary>
            [EnumMember(Value = "VeritasLowLatencyTransport")]
            VeritasLowLatencyTransportEnum = 36,
            /// <summary>
            /// Enum UnknownEnum for Unknown
            /// </summary>
            [EnumMember(Value = "Unknown")]
            UnknownEnum = 37        }