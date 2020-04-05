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
        private Dictionary<string, Dictionary<string, ServerCombatantFightInfo>> Fights;
        public BattleAggregator()
        {
            Fights = new Dictionary<string, Dictionary<string, ServerCombatantFightInfo>>();
            Fights.Add("123", new Dictionary<string, ServerCombatantFightInfo>());
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                ServerCombatantFightInfo fightInfo = JsonConvert.DeserializeObject<ServerCombatantFightInfo>(e.Data);
                if (Fights.ContainsKey(fightInfo.RaidId))
                {
                    Dictionary<string, ServerCombatantFightInfo> fight = Fights[fightInfo.RaidId];

                    if (!fight.ContainsKey(fightInfo.CombatantName))
                    {
                        fight.Add(fightInfo.CombatantName, fightInfo);
                    }
                    fight[fightInfo.CombatantName] = fightInfo;

                    string msg = JsonConvert.SerializeObject(fight.Values.ToList());
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
