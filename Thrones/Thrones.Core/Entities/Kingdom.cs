namespace Thrones.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Thrones.Core.Abstract;

    public class Kingdom : IEquatable<Kingdom>, IKingdom
    {
        private List<IKingdom> allies;
        private Dictionary<char, int> charCount;

        public Kingdom(string name, string emblem)
        {
            this.allies = new List<IKingdom>();
            this.Name = name;
            this.Emblem = emblem;
            this.charCount = this.GetCharCount(this.Emblem);
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
            Dictionary<char, int> messageCharCount = this.GetCharCount(message.ToLower());
            foreach (KeyValuePair<char, int> item in this.charCount)
            {
                if (!messageCharCount.ContainsKey(item.Key))
                {
                    return false;
                }

                if (messageCharCount[item.Key] < item.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Equals(Kingdom other)
        {
            return this.Name == other.Name && this.Emblem == other.Emblem;
        }

        private Dictionary<char, int> GetCharCount(string message)
        {
            Dictionary<char, int> charCount = new Dictionary<char, int>();
            foreach (char currentChar in message)
            {
                if (charCount.Keys.Contains(currentChar))
                {
                    charCount[currentChar] += 1;
                }
                else
                {
                    charCount.Add(currentChar, 1);
                }
            }

            return charCount;
        }
    }
}
