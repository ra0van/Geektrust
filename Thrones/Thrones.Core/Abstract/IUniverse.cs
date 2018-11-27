namespace Thrones.Core.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUniverse
    {
        string Name { get; }

        IKingdom Ruler { get; }

        List<IKingdom> Kingdoms { get; }

        void AddKingdom(IKingdom kingdom);

        void RemoveKingdom(string kingdom);
    }
}
