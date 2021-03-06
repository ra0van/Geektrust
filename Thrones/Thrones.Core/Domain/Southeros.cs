﻿namespace Thrones.Core.Domain
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

        public void ClaimTheThrone(IBallot ballot)
        {
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

        public List<KeyValuePair<IKingdom, int>> ElectARuler(HashSet<IKingdom> competingKingdoms)
        {
            Election election = new Election(competingKingdoms, this.GenerateElectorate(competingKingdoms));
            KeyValuePair<IKingdom, HashSet<IKingdom>> rulerAndAllies = election.GetWinnerAndAllies().OrderByDescending(x => x.Value).First();
            this.Ruler = rulerAndAllies.Key;
            foreach (IKingdom ally in rulerAndAllies.Value)
            {
                this.Ruler.AddAlly(ally);
            }

            return election.GetElectionResults();
        }

        private HashSet<IKingdom> GenerateElectorate(HashSet<IKingdom> competingKingdoms)
        {
            HashSet<IKingdom> electorate = new HashSet<IKingdom>(this.Kingdoms);
            electorate.RemoveWhere(x => competingKingdoms.Contains(x));
            return electorate;
        }
    }
}
