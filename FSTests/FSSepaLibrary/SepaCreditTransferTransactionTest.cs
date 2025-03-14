﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSSepaLibrary.Test
{
    [TestClass]
    public class SepaCreditTransferTransactionTest
    {
        private const string Bic = "SOGEFRPPXXX";
        private const string Iban = "FR7030002005500000157845Z02";
        private const string Name = "A_NAME";

        private readonly SepaIbanData _iBanData = new SepaIbanData
            {
                Bic = Bic,
                Iban = Iban,
                Name = Name
            };

        [TestMethod]
        public void ShouldHaveADefaultCurrency()
        {
            var data = new SepaCreditTransferTransaction();

            Assert.AreEqual("EUR", data.Currency);
        }

        [TestMethod]
        public void ShouldKeepProvidedData()
        {
            const decimal amount = 100m;
            const string currency = "USD";
            const string id = "Batch1";
            const string endToEndId = "Batch1/Row2";
            const string remittanceInformation = "Sample";

            var data = new SepaCreditTransferTransaction
                {
                    Creditor = _iBanData,
                    Amount = amount,
                    Currency = currency,
                    Id = id,
                    EndToEndId = endToEndId,
                    RemittanceInformation = remittanceInformation
                };

            Assert.AreEqual(currency, data.Currency);
            Assert.AreEqual(amount, data.Amount);
            Assert.AreEqual(id, data.Id);
            Assert.AreEqual(endToEndId, data.EndToEndId);
            Assert.AreEqual(remittanceInformation, data.RemittanceInformation);
            Assert.AreEqual(Bic, data.Creditor.Bic);
            Assert.AreEqual(Iban, data.Creditor.Iban);

            var data2 = data.Clone() as SepaCreditTransferTransaction;

            Assert.IsNotNull(data2);
            Assert.AreEqual(currency, data2.Currency);
            Assert.AreEqual(amount, data2.Amount);
            Assert.AreEqual(id, data2.Id);
            Assert.AreEqual(endToEndId, data2.EndToEndId);
            Assert.AreEqual(remittanceInformation, data2.RemittanceInformation);
            Assert.AreEqual(Bic, data2.Creditor.Bic);
            Assert.AreEqual(Iban, data2.Creditor.Iban);
        }

        [TestMethod]
        public void ShouldRejectInvalidCreditor()
        {
            try { new SepaCreditTransferTransaction { Creditor = new SepaIbanData() }; }
            catch (SepaRuleException e)
            {
                Assert.IsTrue(e.Message.Contains("Creditor IBAN data are invalid."));
            }
        }
    }
}