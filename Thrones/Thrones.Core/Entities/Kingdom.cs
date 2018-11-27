namespace Thrones.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Thrones.Core.Abstract;

    public class Kingdom : IEquatable<Kingdom>, IKingdom
    {
        private List<IKingdom> allies;

        public Kingdom(string name, string emblem)
        {
            this.allies = new List<IKingdom>();
            this.Name = name;
            this.Emblem = emblem;
        }

        public string Name { get; }

        public string Emblem { get; }

        public void AddAlly(IKingdom ally)
        {
            IKingdom allyToAdd = this.allies.FirstOrDefault(x => x.Name == ally.Name);
            if (allyToAdd is null)
            {
                this.allies.Add(ally);
            }
        }

        public void RemoveAlly(IKingdom ally)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetAllies()
        {
            return this.allies.Select(x => x.Name).ToList();
        }

        public bool IsNull()
        {
            return false;
        }

        public bool WillSupport(string message)
        {
            return true;
        }

        public bool Equals(Kingdom other)
        {
            return this.Name == other.Name && this.Emblem == other.Emblem;
        }
    }
}
