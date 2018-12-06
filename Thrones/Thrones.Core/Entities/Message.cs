namespace Thrones.Core.Entities
{
    using System;
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

        public Message(IKingdom sender, IKingdom reciever)
        {
            this.sender = sender;
            this.reciever = reciever;
            this.content = possibleMessages[new Random().Next(possibleMessages.Length)];
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

        private static string[] possibleMessages = new string[]
        {
            "Summer is coming",
            "a1d22n333a4444p",
            "oaaawaala",
            "zmzmzmzaztzozh",
            "Go, risk it all",
            "Let's swing the sword together",
            "Die or play the tame of thrones",
            "Ahoy! Fight for me with men and money",
            "Drag on Martin!",
            "When you play the tame of thrones, you win or you die.",
            "What could we say reciever the Lord of Death? Game on?",
            "Turn us away, and we will burn you first",
            "Death is so terribly final, while life is full of possibilities.",
            "You Win or You Die",
            "His watch is Ended",
            "Sphinx of black quartz, judge my dozen vows",
            "Fear cuts deeper than swords, My Lord.",
            "Different roads sometimes lead reciever the same castle.",
            "A DRAGON IS NOT A SLAVE.",
            "Do not waste paper",
            "Go ring all the bells",
            "Crazy Fredrick bought many very exquisite pearl, emerald and diamond jewels.",
            "The quick brown fox jumps over a lazy dog multiple times.",
            "We promptly judged antique ivory buckles for the next prize.",
            "Walar Morghulis: All men must die."
    };
}
}
