using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace DamageParserServer
{
    public class BattleAggregator : WebSocketBehavior
    {
        private Dictionary<string, Dictionary<string, ServerCombatantFightInfo>> Raids;
        public BattleAggregator()
        {
            Raids = new Dictionary<string, Dictionary<string, ServerCombatantFightInfo>>();
            AddRaid("123"); //test
        }

        public void AddRaid(string raidId)
        {
            Raids.Add(raidId, new Dictionary<string, ServerCombatantFightInfo>());
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                ServerCombatantFightInfo fightInfo = JsonConvert.DeserializeObject<ServerCombatantFightInfo>(e.Data);
                if (Raids.ContainsKey(fightInfo.RaidId))
                {
                    Dictionary<string, ServerCombatantFightInfo> raid = Raids[fightInfo.RaidId];

                    if (!raid.ContainsKey(fightInfo.CombatantName))
                    {
                        raid.Add(fightInfo.CombatantName, fightInfo);
                    }
                    raid[fightInfo.CombatantName] = fightInfo;

                    string msg = JsonConvert.SerializeObject(raid.Values.ToList());
                    Send(msg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception: ", ex.Message);
            }
        }
    }
}
