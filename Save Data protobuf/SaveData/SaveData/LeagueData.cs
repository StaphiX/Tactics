// This is the main DLL file.
using System;

//we need to use specific attributes so that ProtoBuf knows what references what, so we add a using statement for the ProtoBuf namespace.
using ProtoBuf;

namespace SaveData
{
    //when creating a class use the ProtoContract attribute. While not completely necessary if you look through the documentation you can see that these attributes can control what goes on when things are serialized and deserialized
    [ProtoContract]
    public class LeagueData
    {
        [ProtoMember(1)]
        int iLeagueID;
        [ProtoMember(2)]
        eCountry eCountry;
        [ProtoMember(3)]
        int iLeagueRank;
        [ProtoMember(4)]
        int iPromotionLeague;
    }

    [ProtoContract]
    enum eCountry
    {
        ENG, //England
    }
}
