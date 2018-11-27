namespace Thrones.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Thrones.Core.Abstract;
    using Thrones.Core.Entities;

    public class Southeros : Universe
    {
        private static readonly int MINIMUMALLIESTOCLAIMTHETHRONE = 3;

        public Southeros(string name)
            : base(name)
        {
            this.AddKingdom(KingdomFactory.CreateKingdom(name: "Land", emblem: "panda"));
            this.AddKingdom(KingdomFactory.CreateKingdom(name: "Water", emblem: "octopus"));
            this.AddKingdom(KingdomFactory.CreateKingdom(name: "Ice", emblem: "mammoth"));
            this.AddKingdom(KingdomFactory.CreateKingdom(name: "Air", emblem: "owl"));
            this.AddKingdom(KingdomFactory.CreateKingdom(name: "Fire", emblem: "dragon"));
            this.AddKingdom(KingdomFactory.CreateKingdom(name: "Space", emblem: "gorilla"));
        }

        public void ClaimTheThrone(IKingdom contendingKingdom, HashSet<IMessage> messages)
        {
            try
            {
                Ballot ballot = new Ballot(contendingKingdom, messages);
                Dictionary<IKingdom, HashSet<IKingdom>> winners = ballot.GetWinners();

                KeyValuePair<IKingdom, HashSet<IKingdom>> winner = winners.FirstOrDefault();
                if (winners.Count > 0 && winner.Value.Count >= MINIMUMALLIESTOCLAIMTHETHRONE)
                {
                    this.Ruler = winner.Key;
                    foreach (IKingdom ally in winner.Value)
                    {
                        this.Ruler.AddAlly(ally);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
