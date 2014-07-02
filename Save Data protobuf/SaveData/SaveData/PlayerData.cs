// This is the main DLL file.
using System;

//we need to use specific attributes so that ProtoBuf knows what references what, so we add a using statement for the ProtoBuf namespace.
using ProtoBuf;

namespace SaveData
{
//when creating a class use the ProtoContract attribute. While not completely necessary if you look through the documentation you can see that these attributes can control what goes on when things are serialized and deserialized
    [ProtoContract]
    public class PlayerData
    {
        //Protobuf also needs to associate data with keys so that it can identify data on serialization and deserialization. We do this with the ProtoMember attribute, and in that attribute be assign an integer value
        [ProtoMember(1)]
        public int iID;
        [ProtoMember(2)]
        public string sFullName;
        [ProtoMember(3)]
        public string sFirstName;
        [ProtoMember(4)]
        public string sLastName;
        [ProtoMember(5)]
        public PlayerPosition[] ePositions;
        [ProtoMember(6)]
        public EFoot uPrefFoot;
        [ProtoMember(7)]
        public DateTime tDOB;
        [ProtoMember(8)]
        public string sNation;
        [ProtoMember(9)]
        public byte uHeightCM;
        [ProtoMember(10)]
        public byte uWeightKG;
        [ProtoMember(11)]
        public PlayerRatings tRatings;
        [ProtoMember(12)]
        public PlayerAbilities tAbilities;
        [ProtoMember(13)]
        public PlayerHistory[] tHistory;
    }

    [ProtoContract]
    public class PlayerRatings
    {
        [ProtoMember(1)] //ratings are stored as 0 - 10000 e.g. 0.00 - 99.99
        public ushort uSpeed;
        [ProtoMember(2)]
        public ushort uPassing;
        [ProtoMember(3)]
        public ushort uSetPeices;
        [ProtoMember(4)]
        public ushort uFocus;
        [ProtoMember(5)]
        public ushort uSkill;
        [ProtoMember(6)]
        public ushort uStrength;
        [ProtoMember(7)]
        public ushort uAerial;
        [ProtoMember(8)]
        public ushort uShooting;
        [ProtoMember(9)]
        public ushort uDefending;
        [ProtoMember(10)]
        public ushort uPositioning;
        [ProtoMember(11)]
        public ushort uGoalkeeping;
    }

    [ProtoContract]
    public class PlayerAbilities
    {
        [ProtoMember(1)] //Abilities are stored as -100 - 100 e.g. -0.99 - 0.99
        public sbyte uControl;
        [ProtoMember(2)] 
        public sbyte uCorners;
        [ProtoMember(3)]
        public sbyte uCrossing;
        [ProtoMember(4)] 
        public sbyte uDribbling;
        [ProtoMember(5)] 
        public sbyte uFinishing;
        [ProtoMember(6)] 
        public sbyte uFlair;
        [ProtoMember(7)] 
        public sbyte uFreeKicks;
        [ProtoMember(8)] 
        public sbyte uLongPassing;
        [ProtoMember(9)] 
        public sbyte uLongShots;
        [ProtoMember(10)] 
        public sbyte uMarking;
        [ProtoMember(11)]
        public sbyte uPassing;
        [ProtoMember(12)]
        public sbyte uPenalties;
        [ProtoMember(13)]
        public sbyte uTackling;
        [ProtoMember(14)]
        public sbyte uVolleying;
        [ProtoMember(15)]
        public sbyte uAggression;
        [ProtoMember(16)]
        public sbyte uComposure;
        [ProtoMember(17)]
        public sbyte uConcentration;
        [ProtoMember(18)]
        public sbyte uCreativity;
        [ProtoMember(19)]
        public sbyte uDetermination;
        [ProtoMember(20)]
        public sbyte uLeadership;
        [ProtoMember(21)]
        public sbyte uMovement;
        [ProtoMember(22)]
        public sbyte uPositioning;
        [ProtoMember(23)]
        public sbyte uWorkRate;
        [ProtoMember(24)]
        public sbyte uPace;
        [ProtoMember(25)]
        public sbyte uStamina;
        [ProtoMember(26)]
        public sbyte uStrength;
        [ProtoMember(27)]
        public sbyte uAerialAbility;
        [ProtoMember(28)]
        public sbyte uLongThrows;
        [ProtoMember(29)]
        public sbyte uCommandOfArea;
        [ProtoMember(30)]
        public sbyte uDistribution;
        [ProtoMember(31)]
        public sbyte uHandling;
        [ProtoMember(32)]
        public sbyte uOneOnOnes;
        [ProtoMember(33)]
        public sbyte uSavingPenalties;
        [ProtoMember(34)]
        public sbyte uShotStopping;

        public sbyte GetAbility(EAbility eAbility)
        {
            switch (eAbility)
            {
                case EAbility.NONE: return  0;
                case EAbility.Control: return  uControl;
                case EAbility.Corners: return  uCorners;
                case EAbility.Crossing: return  uCrossing;
                case EAbility.Dribbling: return  uDribbling;
                case EAbility.Finishing: return  uFinishing;
                case EAbility.Flair: return  uFlair;
                case EAbility.FreeKicks: return  uFreeKicks;
                case EAbility.LongPassing: return  uLongPassing;
                case EAbility.LongShots: return  uLongShots;
                case EAbility.Marking: return  uMarking;
                case EAbility.Passing: return  uPassing;
                case EAbility.Penalties: return  uPenalties;
                case EAbility.Tackling: return  uTackling;
                case EAbility.Volleying: return  uVolleying;
                case EAbility.Aggression: return  uAggression;
                case EAbility.Composure: return  uComposure;
                case EAbility.Concentration: return  uConcentration;
                case EAbility.Creativity: return  uCreativity;
                case EAbility.Determination: return  uDetermination;
                case EAbility.Leadership: return  uLeadership;
                case EAbility.Movement: return  uMovement;
                case EAbility.Positioning: return  uPositioning;
                case EAbility.WorkRate: return  uWorkRate;
                case EAbility.Pace: return  uPace;
                case EAbility.Stamina: return  uStamina;
                case EAbility.Strength: return  uStrength;
                case EAbility.AerialAbility: return  uAerialAbility;
                case EAbility.LongThrows: return  uLongThrows;
                case EAbility.CommandOfArea: return  uCommandOfArea;
                case EAbility.Distribution: return  uDistribution;
                case EAbility.Handling: return uHandling;
                case EAbility.OneOnOnes: return  uOneOnOnes;
                case EAbility.SavingPenalties: return  uSavingPenalties;
                case EAbility.ShotStopping: return  uShotStopping;
                case EAbility.SpeedRat: return (sbyte)(uPace + uStamina + uMovement + uWorkRate);
                case EAbility.PassingRat: return (sbyte)(uPassing + uCrossing + uDistribution + uLongPassing);
                case EAbility.SetPeicesRat: return (sbyte)(uFreeKicks + uCorners + uPenalties + uLongThrows);
                case EAbility.FocusRat: return (sbyte)(uDetermination + uComposure + uLeadership + uConcentration);
                case EAbility.SkillRat: return (sbyte)(uFlair + uDribbling + uCreativity + uControl);
                case EAbility.StrengthRat: return (sbyte)(uStrength + uAggression);
                case EAbility.AerialRat: return (sbyte)(uAerialAbility);
                case EAbility.ShootingRat: return (sbyte)(uLongShots + uFinishing + uVolleying);
                case EAbility.DefendingRat: return (sbyte)(uTackling + uMarking + uPositioning);
                case EAbility.PositioningRat: return (sbyte)(uPositioning);
                case EAbility.GoalkeepingRat: return (sbyte)(uCommandOfArea + uDistribution + uHandling + uOneOnOnes + uSavingPenalties + uShotStopping);
                default:
                    return 0;
            }
        }
    }

    [ProtoContract]
    public enum EFoot
    {
        LEFT,
        RIGHT,
        BOTH,
    }

    [ProtoContract]
    public class PlayerHistory
    {
        [ProtoMember(1)]
        public DateTime tDate;
        [ProtoMember(2)]
        public string sClub;
        [ProtoMember(3)]
        public bool bLoan;
        [ProtoMember(4)]
        public sbyte iRating;
    }

    [ProtoContract]
    public class PlayerPosition
    {
        [ProtoMember(1)]
        public EPosition ePos;
        [ProtoMember(2)]
        public byte uProficiancy;

        public PlayerPosition()
        {
        }

        public PlayerPosition(EPosition _ePos, byte _uProficiancy)
        {
            ePos = _ePos;
            uProficiancy = _uProficiancy;
        }
    }

    [ProtoContract]
    public enum EAbility
    {
        NONE,
        Control,
        Corners,
        Crossing,
        Dribbling,
        Finishing,
        Flair,
        FreeKicks,
        LongPassing,
        LongShots,
        Marking,
        Passing,
        Penalties,
        Tackling,
        Volleying,
        Aggression,
        Composure,
        Concentration,
        Creativity,
        Determination,
        Leadership,
        Movement,
        Positioning,
        WorkRate,
        Pace,
        Stamina,
        Strength,
        AerialAbility,
        LongThrows,
        CommandOfArea,
        Distribution,
        Handling,
        OneOnOnes,
        SavingPenalties,
        ShotStopping,
        SpeedRat,
        PassingRat,
        SetPeicesRat,
        FocusRat,
        SkillRat,
        StrengthRat,
        AerialRat,
        ShootingRat,
        DefendingRat,
        PositioningRat,
        GoalkeepingRat,
    }

}