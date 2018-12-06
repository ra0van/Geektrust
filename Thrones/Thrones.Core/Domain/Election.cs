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
        private readonly List<KeyValuePair<IKingdom, int>> electionResults = new List<KeyValuePair<IKingdom, int>>();
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

            electorate.RemoveWhere(x => competingKingdoms.Contains(x));
            this.electorate = electorate;
        }

        public int ElectorateStrength => this.electorate.Count;

        public List<KeyValuePair<IKingdom, int>> ElectionResults
        {
            get
            {
                if (!this.electionCompleted)
                {
                    this.RunElection();
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

        public List<KeyValuePair<IKingdom, int>> GetElectionResults()
        {
            if (this.electionCompleted is false)
            {
                this.RunElection();
            }

            return this.electionResults;
        }

        private void RunElection()
        {
            Dictionary<IKingdom, HashSet<IKingdom>> winners = new Dictionary<IKingdom, HashSet<IKingdom>>();

            foreach (IKingdom kingdom in this.competingKingdoms)
            {
                winners.Add(kingdom, new HashSet<IKingdom>());
            }

            do
            {
                winners = this.RunBallot(winners.Keys.ToList());
            }
            while (winners.Count > 1 || !this.WinnerGotMinimumVote(winners));

            this.winnerAndAllies = winners;
            this.electionCompleted = true;
        }

        private bool WinnerGotMinimumVote(Dictionary<IKingdom, HashSet<IKingdom>> winners)
        {
            return winners.Last().Value.Count > 0;
        }

        private Dictionary<IKingdom, HashSet<IKingdom>> RunBallot(List<IKingdom> kingdoms)
        {
            Ballot ballot = new Ballot(this.competingKingdoms, this.electorate);
            Dictionary<IKingdom, HashSet<IKingdom>> results = ballot.GetWinners();
            foreach (KeyValuePair<IKingdom, HashSet<IKingdom>> item in results)
            {
                this.electionResults.Add(new KeyValuePair<IKingdom, int>(item.Key, item.Value.Count));
            }

            return results;
        }
    }
}
