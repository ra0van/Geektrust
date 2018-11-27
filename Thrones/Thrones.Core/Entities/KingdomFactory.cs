namespace Thrones.Core.Entities
{
    using Thrones.Core.Abstract;

    public class KingdomFactory
    {
        public static IKingdom CreateKingdom(string name, string emblem)
        {
            IKingdom kingdom = null;
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(emblem))
            {
                kingdom = new NullKingdom();
            }
            else
            {
                kingdom = new Kingdom(name, emblem);
            }

            return kingdom;
        }
    }
}
