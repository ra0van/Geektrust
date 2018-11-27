namespace Thrones.Core.Abstract
{
    using System.Collections.Generic;

    public interface IKingdom
    {
        string Name { get; }

        string Emblem { get; }

        void AddAlly(IKingdom ally);

        void RemoveAlly(IKingdom ally);

        List<string> GetAllies();

        bool IsNull();

        bool WillSupport(string message);
    }
}
