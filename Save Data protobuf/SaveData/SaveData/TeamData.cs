// This is the main DLL file.
using System;

//we need to use specific attributes so that ProtoBuf knows what references what, so we add a using statement for the ProtoBuf namespace.
using ProtoBuf;

namespace SaveData
{
    //when creating a class use the ProtoContract attribute. While not completely necessary if you look through the documentation you can see that these attributes can control what goes on when things are serialized and deserialized
    [ProtoContract]
    public class TeamData
    {
        //Protobuf also needs to associate data with keys so that it can identify data on serialization and deserialization. We do this with the ProtoMember attribute, and in that attribute be assign an integer value
        [ProtoMember(1)]
        public int iID;
        [ProtoMember(2)]
        public string sTeamName;
        [ProtoMember(3)]
        public string[] sNickName;
        [ProtoMember(4)]
        public string sMedName;
        [ProtoMember(5)]
        public string sShortName;
        [ProtoMember(6)]
        public string sManagerName;
        [ProtoMember(7)]
        public string sLeague;
        [ProtoMember(8)]
        public string sStadium;
        [ProtoMember(9)]
        public int iStadiumCapacity;
        [ProtoMember(10)]
        public string sLocation;
        [ProtoMember(11)]
        public string sCountry;
        [ProtoMember(12)]
        public string sHomeCol;
        [ProtoMember(13)]
        public string sAwayCol;
        [ProtoMember(14)]
        public ushort sFoundedYear;
        [ProtoMember(15)]
        public TeamHistory[] tTeamHistory;
        [ProtoMember(16)]
        public TeamStaff[] tTeamStaff;
        [ProtoMember(17)]
        public string[] sRivals;
        [ProtoMember(18)]
        public PlayerLink[] tPlayerLinks;
    }


    [ProtoContract]
    public class TeamHistory
    {
        [ProtoMember(1)]
        public string sCompetition;
        [ProtoMember(2)]
        public ushort uYear;
        [ProtoMember(3)]
        public bool bCup;
    }

    [ProtoContract]
    public class TeamStaff
    {
        [ProtoMember(1)]
        public string sTitle;
        [ProtoMember(2)]
        public string sName;
    }

    [ProtoContract]
    public class PlayerLink
    {
        [ProtoMember(1)]
        public int iID;
        [ProtoMember(2)]
        public int iPlayerNumber;
        [ProtoMember(3)]
        public bool bIsFirstEleven;
        [ProtoMember(4)]
        public EPosition eTeamPos;
    }

}
