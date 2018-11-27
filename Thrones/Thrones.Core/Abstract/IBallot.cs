namespace Thrones.Core.Abstract
{
    using System.Collections.Generic;

    public interface IBallot
    {
        Dictionary<IKingdom, HashSet<IKingdom>> GetWinners();
    }
}
