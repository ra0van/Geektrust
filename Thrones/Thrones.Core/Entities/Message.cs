namespace Thrones.Core.Entities
{
    using Thrones.Core.Abstract;

    public class Message : IMessage
    {
        private readonly IKingdom sender;
        private readonly IKingdom reciever;
        private readonly string content;

        public Message(IKingdom sender, IKingdom reciever, string content)
        {
            this.sender = sender;
            this.reciever = reciever;
            this.content = content;
        }

        public IKingdom Sender
        {
            get => this.sender;
        }

        public IKingdom Reciever
        {
            get => this.reciever;
        }

        public string Content => this.content;
    }
}
