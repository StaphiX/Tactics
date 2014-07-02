using System;

//we need to use specific attributes so that ProtoBuf knows what references what, so we add a using statement for the ProtoBuf namespace.
using ProtoBuf;

namespace SaveData
{
    [ProtoContract]
    public class BoardGame
    {
        [ProtoMember(1)]
        public int iID;
        [ProtoMember(2)]
        public int iCollectionID;
        [ProtoMember(3)]
        public string sName;
        [ProtoMember(4)]
        public string[] sAltNames;
        [ProtoMember(5)]
        public string sDescription;
        [ProtoMember(6)]
        public string[] sCatagory;
        [ProtoMember(7)]
        public string[] sMechanic;
        [ProtoMember(8)]
        public string[] sFamily;
        [ProtoMember(9)]
        public string[] sDesigner;
        [ProtoMember(10)]
        public string[] sArtist;
        [ProtoMember(11)]
        public string[] sPublisher;
        [ProtoMember(12)]
        public string sImage;
        [ProtoMember(13)]
        public int iMinPlayers;
        [ProtoMember(14)]
        public int iMaxPlayers;
        [ProtoMember(15)]
        public int iIdealPlayers;
        [ProtoMember(16)]
        public int iYear;
        [ProtoMember(17)]
        public int iMinAge;
        [ProtoMember(18)]
        public int iSuggestedAge;
        [ProtoMember(19)]
        public int iMinPlayTime;
        [ProtoMember(20)]
        public int iMaxPlayTime;
    }
}
