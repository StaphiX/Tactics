using System;
using SaveData;
using ProtoBuf;
using ProtoBuf.Meta;

namespace SaveData_Serializer
{
    class Program
    { 
        static void Main(string[] args)
        {
            var model = TypeModel.Create();
            model.Add(typeof(SaveData.BoardGame), true);
            model.Add(typeof(SaveData.LeagueData), true);
            model.Add(typeof(SaveData.PlayerAbilities), true);
            model.Add(typeof(SaveData.PlayerData), true);
            model.Add(typeof(SaveData.PlayerHistory), true);
            model.Add(typeof(SaveData.PlayerPosition), true);
            model.Add(typeof(SaveData.PlayerRatings), true);
            model.Add(typeof(SaveData.TeamData), true);
            model.Add(typeof(SaveData.TeamHistory), true);
            model.Add(typeof(SaveData.TeamStaff), true);
            model.Add(typeof(SaveData.EAbility), true);
            model.Add(typeof(SaveData.EFoot), true);
            model.Add(typeof(SaveData.EFormation), true);
            model.Add(typeof(SaveData.EPosition), true);

            model.AllowParseableTypes = true;
            model.AutoAddMissingTypes = true;

            model.Compile("SaveDataSerializer", "SaveDataSerializer.dll");
        }
    }
}
