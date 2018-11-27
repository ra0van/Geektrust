namespace Thrones.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Thrones.Core.Abstract;
    using Thrones.Core.Entities;

    public class Ballot
    {
        private readonly HashSet<IKingdom> competingKingdoms;
        private readonly HashSet<IKingdom> electorate;
        private readonly HashSet<IMessage> ballotBox;
        private readonly Dictionary<IKingdom, HashSet<IKingdom>> winners;
        private readonly Dictionary<IKingdom, int> result;
        private bool electionCompleted;

        public Ballot(IKingdom competingKingdom, HashSet<IMessage> ballotBox)
        {
            this.electorate = new HashSet<IKingdom>();
            this.competingKingdoms = new HashSet<IKingdom>();
            this.competingKingdoms.Add(competingKingdom);
            this.winners = new Dictionary<IKingdom, HashSet<IKingdom>>();
            this.result = new Dictionary<IKingdom, int>();

            this.ballotBox = new HashSet<IMessage>();
            this.ballotBox.UnionWith(ballotBox);
            foreach (IMessage message in ballotBox)
            {
                if (message.Sender.Equals(competingKingdom))
                {
                    this.electorate.Add(message.Reciever);
                }
            }
        }

        public Dictionary<IKingdom, HashSet<IKingdom>> GetWinners()
        {
            if (this.electionCompleted is false)
            {
                this.RunBallot();
            }

            return this.winners;
        }

        private void RunBallot()
        {
            Dictionary<IKingdom, HashSet<IKingdom>> results;
            if (this.ballotBox.Count is 0)
            {
                // TODO : generate for prob 2
            }

            results = this.GetResults(this.ballotBox);
            this.CalculateWinners(results);
            this.electionCompleted = true;
        }

        private Dictionary<IKingdom, HashSet<IKingdom>> GetResults(HashSet<IMessage> messages)
        {
            Dictionary<IKingdom, HashSet<IKingdom>> results = new Dictionary<IKingdom, HashSet<IKingdom>>();

            foreach (IKingdom kingdom in this.competingKingdoms)
            {
                results.Add(kingdom, new HashSet<IKingdom>());
            }

            HashSet<IKingdom> votedKingdoms = new HashSet<IKingdom>();
            foreach (IMessage message in messages)
            {
                IKingdom sender = message.Sender, reciever = message.Reciever;
                if (votedKingdoms.Contains(reciever))
                {
                    continue;
                }

                if (this.competingKingdoms.Contains(sender) && reciever.WillSupport(message.Content))
                {
                    votedKingdoms.Add(reciever);
                    results[sender].Add(reciever);
                }
            }

            return results;
        }

        private void CalculateWinners(Dictionary<IKingdom, HashSet<IKingdom>> result)
        {
            int maxVotes = 0;
            foreach (IKingdom kingdom in result.Keys)
            {
                int votes = result[kingdom].Count;
                this.result.Add(kingdom, votes);
                maxVotes = Math.Max(maxVotes, votes);
            }

            foreach (var winner in this.result.Where(x => x.Value == maxVotes))
            {
                this.winners.Add(winner.Key, result[winner.Key]);
            }
        }
    }
}
