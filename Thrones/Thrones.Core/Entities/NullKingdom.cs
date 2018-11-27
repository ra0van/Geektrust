namespace Thrones.Core.Entities
{
    using System.Collections.Generic;
    using Thrones.Core.Abstract;

    public class NullKingdom : IKingdom
    {
        private const string None = "None";

        public string Name => None;

        public string Emblem => None;

        public void AddAllie(IKingdom friend) { }

        public void AddAlly(IKingdom ally)
        {
        }

        public void RemoveAlly(IKingdom ally)
        {
        }

        public List<string> GetAllies()
        {
            return new List<string>() { None };
        }

        public bool IsNull()
        {
            return true;
        }

        public bool WillSupport(string message)
        {
            return false;
        }
    }
}
