namespace Thrones.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Thrones.Core.Abstract;
    using Thrones.Core.Entities;

    public class Election
    {
        private readonly HashSet<IKingdom> competingKingdoms;
        private readonly HashSet<IKingdom> electorate;
        private readonly List<Dictionary<IKingdom, int>> electionResults = new List<Dictionary<IKingdom, int>>();
        private Dictionary<IKingdom, HashSet<IKingdom>> winnerAndAllies;
        private bool electionCompleted;

        public Election(HashSet<IKingdom> competingKingdoms, HashSet<IKingdom> electorate)
        {
            if (competingKingdoms.Count == 0)
            {
                throw new Exception("Election cannot be created with no candidates");
            }

            if (electorate.Count == 0)
            {
                throw new Exception("No Kingdom left to Vote");
            }

            this.competingKingdoms = competingKingdoms;

            this.electorate.RemoveWhere(x => competingKingdoms.Contains(x));
            this.electorate = electorate;
        }

        public int ElectorateStrength => this.electorate.Count;

        public List<Dictionary<IKingdom, int>> ElectionResults
        {
            get
            {
                if (!this.electionCompleted)
                {
                    // runElection();
                }

                return this.electionResults;
            }
        }

        public Dictionary<IKingdom, HashSet<IKingdom>> GetWinnerAndAllies()
        {
            if (this.electionCompleted is false)
            {
                this.RunElection();
            }

            return this.winnerAndAllies;
        }

        private void RunElection()
        {
            Dictionary<IKingdom, HashSet<IKingdom>> winners = new Dictionary<IKingdom, HashSet<IKingdom>>();

            foreach (IKingdom kingdom in this.competingKingdoms)
            {
                winners.Add(kingdom, new HashSet<IKingdom>());
            }

            // do
            // {
            //    winners = runBallot(winners.Keys);
            // } while (winners.Count > 1);

            // this.winnerAndAllies = winners.First();
            this.electionCompleted = true;
        }
    }
}
