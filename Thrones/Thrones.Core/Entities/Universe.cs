namespace Thrones.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using Thrones.Core.Abstract;
    using Thrones.Core.Entities;

    public class Universe : IUniverse
    {
        private HashSet<IKingdom> kingdoms;
        private IKingdom ruler;

        public Universe(string name)
        {
            this.Name = name;
            this.kingdoms = new HashSet<IKingdom>();
            this.ruler = KingdomFactory.CreateKingdom(null, null);
        }

        public string Name { get; }

        public List<IKingdom> Kingdoms
        {
            get { return this.kingdoms.ToList(); }
        }

        public IKingdom Ruler
        {
            get => this.ruler;
            set => this.ruler = value;
        }

        public IKingdom this[string kingdomName]
        {
            get
            {
                return this.kingdoms.FirstOrDefault(x => x.Name.ToUpper() == kingdomName.ToUpper());
            }
        }

        public void AddKingdom(IKingdom kingdom)
        {
            this.kingdoms.Add(kingdom);
        }

        public void RemoveKingdom(IKingdom kingdom)
        {
            IKingdom kingdomToRemove = this[kingdom.Name];
            if (kingdomToRemove != null)
            {
                this.kingdoms.Remove(kingdomToRemove);
            }
        }

        public void RemoveKingdom(string kingdom)
        {
            throw new System.NotImplementedException();
        }
    }
}
