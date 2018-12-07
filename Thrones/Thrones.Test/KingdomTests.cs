namespace Thrones.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NUnit.Framework;
    using Thrones.Core.Abstract;
    using Thrones.Core.Entities;

    [TestClass]
    public class KingdomTests
    {

        public KingdomTests()
        {
        }

        [TestCase("Ice", "Mammoth", "Do not waster paper", false)]
        public void WillGiveSupportTests(string name, string emblem, string message, bool expectedResult)
        {
            IKingdom iceKingdom = KingdomFactory.CreateKingdom(name, emblem);
        }
    }
}
