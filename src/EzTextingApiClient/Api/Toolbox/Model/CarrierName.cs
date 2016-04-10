using System.ComponentModel;
using System.Runtime.Serialization;

namespace EzTextingApiClient.Api.Toolbox.Model
{
    public enum CarrierName
    {
        // ReSharper disable InconsistentNaming
        // Carrier Codes (United States)
        [Description("ACS Wireless")] [EnumMember(Value = "ACSUS")] ACSUS,
        [Description("Alltel")] [EnumMember(Value = "ALLTELUS")] ALLTELUS,
        [Description("RINA/All West Wireless")] [EnumMember(Value = "ALLWESTUS")] ALLWESTUS,
        [Description("EKN/Appalachian Wireless")] [EnumMember(Value = "APPALACHIANUS")] APPALACHIANUS,
        [Description("Arch Wireless")] [EnumMember(Value = "ARCHWIRELESSUS")] ARCHWIRELESSUS,
        [Description("AT&T Wireless")] [EnumMember(Value = "ATTUS")] ATTUS,
        [Description("Bluegrass Cellular")] [EnumMember(Value = "BLUEGRASSUS")] BLUEGRASSUS,
        [Description("Boost USA")] [EnumMember(Value = "BOOSTUS")] BOOSTUS,
        [Description("Cellcom")] [EnumMember(Value = "CELLCOMUS")] CELLCOMUS,
        [Description("Cellular South")] [EnumMember(Value = "CELLULARSOUTHUS")] CELLULARSOUTHUS,
        [Description("Centennial")] [EnumMember(Value = "CENTENNIALUS")] CENTENNIALUS,
        [Description("Central Wireless")] [EnumMember(Value = "CENTRALUS")] CENTRALUS,
        [Description("Amerilink")] [EnumMember(Value = "CHOICEUS")] CHOICEUS,
        [Description("Cincinnati Bell")] [EnumMember(Value = "CINBELLUS")] CINBELLUS,
        [Description("AT&T (Formerly Cingular Wireless)")] [EnumMember(Value = "CINGULARUS")] CINGULARUS,
        [Description("Cox Communications")] [EnumMember(Value = "COXUS")] COXUS,
        [Description("Cricket Communications")] [EnumMember(Value = "CRICKETUS")] CRICKETUS,
        [Description("RINA/CTC Telecom-Cambridge")] [EnumMember(Value = "CTCUS")] CTCUS,
        [Description("Dobson")] [EnumMember(Value = "DOBSONUS")] DOBSONUS,
        [Description("RINA/Snake River PCS")] [EnumMember(Value = "EAGLEUS")] EAGLEUS,
        [Description("ECIT - Cell One of East Central IL")] [EnumMember(Value = "ECITUS")] ECITUS,
        [Description("Edge Wireless")] [EnumMember(Value = "EDGEUS")] EDGEUS,
        [Description("Element Mobile")] [EnumMember(Value = "ELEMENTUS")] ELEMENTUS,
        [Description("RINA/FMTC-Farmers Mutual Telephone Co.")] [EnumMember(Value = "FARMERSMUTUALUS")] FARMERSMUTUALUS,
        [Description("GCI Communications")] [EnumMember(Value = "GENERALCOMUS")] GENERALCOMUS,
        [Description("RINA/Silverstar")] [EnumMember(Value = "GOLDSTARUS")] GOLDSTARUS,
        [Description("Immix Wireless/PC Management")] [EnumMember(Value = "IMMIXUS")] IMMIXUS,
        [Description("Inland Cellular")] [EnumMember(Value = "INLANDUS")] INLANDUS,
        [Description("Iowa Wireless")] [EnumMember(Value = "IOWAWIRELESSUS")] IOWAWIRELESSUS,
        [Description("Illinois Valley Cellular")] [EnumMember(Value = "IVCUS")] IVCUS,
        [Description("Metrocall Wireless")] [EnumMember(Value = "METROCALLUS")] METROCALLUS,
        [Description("Metro PCS")] [EnumMember(Value = "METROPCSUS")] METROPCSUS,
        [Description("Midwest Wireless")] [EnumMember(Value = "MIDWESTUS")] MIDWESTUS,
        [Description("Nex-Tech Wireless")] [EnumMember(Value = "NEXTECHUS")] NEXTECHUS,
        [Description("North Coast PCS")] [EnumMember(Value = "NORTHCOASTUS")] NORTHCOASTUS,
        [Description("nTelos")] [EnumMember(Value = "NTELOSUS")] NTELOSUS,
        [Description("RINA/Nucla-Naturita Telephone Co.")] [EnumMember(Value = "NUCLANATURITAUS")] NUCLANATURITAUS,
        [Description("Pacific Bell")] [EnumMember(Value = "PACBELLUS")] PACBELLUS,
        [Description("Plateau Telecom")] [EnumMember(Value = "PLATEAUUS")] PLATEAUUS,
        [Description("Pocket Wireless")] [EnumMember(Value = "POCKETUS")] POCKETUS,
        [Description("Revol")] [EnumMember(Value = "REVOLUS")] REVOLUS,
        [Description("RCC/Unicel")] [EnumMember(Value = "RURALCELUS")] RURALCELUS,
        [Description("RINA/South Central")] [EnumMember(Value = "SOUTHCENTRALUTAHUS")] SOUTHCENTRALUTAHUS,
        [Description("South Canaan Cell")] [EnumMember(Value = "SOUTHCANAANUS")] SOUTHCANAANUS,
        [Description("Southern Bell")] [EnumMember(Value = "SOUTHWESTBELLUS")] SOUTHWESTBELLUS,
        [Description("Sprint PCS")] [EnumMember(Value = "SPRINTUS")] SPRINTUS,
        [Description("Suncom")] [EnumMember(Value = "SUNCOMUS")] SUNCOMUS,
        [Description("RINA/Syringa Wireless")] [EnumMember(Value = "SYRINGAUS")] SYRINGAUS,
        [Description("Thumb Cellular")] [EnumMember(Value = "THUMBUS")] THUMBUS,
        [Description("T-Mobile USA")] [EnumMember(Value = "TMOBILEUS")] TMOBILEUS,
        [Description("Triton PCS")] [EnumMember(Value = "TRITONPCSUS")] TRITONPCSUS,
        [Description("RINA/UBET")] [EnumMember(Value = "UNITAHBASINUS")] UNITAHBASINUS,
        [Description("United Wireless")] [EnumMember(Value = "UNITEDWIRELESSUS")] UNITEDWIRELESSUS,
        [Description("US Cellular")] [EnumMember(Value = "USCELLULARUS")] USCELLULARUS,
        [Description("Verizon Wireless")] [EnumMember(Value = "VERIZONUS")] VERIZONUS,
        [Description("Viaero Wireless")] [EnumMember(Value = "VIAEROUS")] VIAEROUS,
        [Description("Virgin USA")] [EnumMember(Value = "VIRGINUS")] VIRGINUS,
        [Description("West Central Wireless")] [EnumMember(Value = "WCENTRALUS")] WCENTRALUS,
        [Description("Western Wireless")] [EnumMember(Value = "WESTERNWUS")] WESTERNWUS,

        // Carrier Codes (Canada)
        [Description("Fido")] [EnumMember(Value = "FIDOCA")] FIDOCA,
        [Description("Bell Mobility")] [EnumMember(Value = "BELLCA")] BELLCA,
        [Description("Wind Mobile")] [EnumMember(Value = "WINDCA")] WINDCA,
        [Description("Telus")] [EnumMember(Value = "TELUSCA")] TELUSCA,
        [Description("SaskTel")] [EnumMember(Value = "SASKTELCA")] SASKTELCA,
        [Description("Virgin Canada")] [EnumMember(Value = "VIRGINCA")] VIRGINCA,
        [Description("MTS Canada")] [EnumMember(Value = "MTSCA")] MTSCA,
        [Description("Rogers")] [EnumMember(Value = "ROGERSCA")] ROGERSCA,
        [Description("Videotron")] [EnumMember(Value = "VIDEOTRONCA")] VIDEOTRONCA,

        // System cannot obtain carrier information
        [Description("FAILURE")] [EnumMember(Value = "FAILURE")] FAILURE
    }
}