// This is the main DLL file.
using System;

//we need to use specific attributes so that ProtoBuf knows what references what, so we add a using statement for the ProtoBuf namespace.
using ProtoBuf;

namespace SaveData
{
    class FormationData
    {

    }

    [ProtoContract]
    public enum EFormation
    {
        E442,
        E442Dia,
        E433,
        E433Att,
        E433Cen,
        E424,
        E451,
        E451Def,
        E343,
        E361,
        COUNT,
    }

    [ProtoContract]
    public enum EPosition
    {
        SUB = -1,
        GK,
        EDEF,
        LB,
        LCB,
        CB,
        RCB,
        RB,
        EMID,
        LM,
        DM,
        DCM,
        LWM,
        LCM,
        CM,
        RCM,
        RWM,
        ACM,
        AM,
        RM,
        EATT,
        LST,
        CST,
        RST,
        LW,
        RW,
        COUNT,
    }
}
