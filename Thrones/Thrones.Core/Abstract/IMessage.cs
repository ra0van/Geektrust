namespace Thrones.Core.Abstract
{
    using System.Collections.ObjectModel;

    public interface IMessage
    {
        IKingdom Sender { get; }

        IKingdom Reciever { get; }

        string Content { get; }
    }
}
