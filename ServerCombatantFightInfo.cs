using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamageParserServer
{
    [Serializable]
    public class ServerCombatantFightInfo
    {
        public string CombatantName { get; set; }
        public DateTime StartTime { get; set; }
        public int TotalDamage { get; set; }
        public int DPS { get; set; }
        public string RaidId { get; set; }
    }
}
