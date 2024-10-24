﻿using System;
using System.IO;
using System.Xml;
using FSSepaLibraryCore.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSSepaLibraryCore.Test
{
    [TestClass]
    public class SepaDebitTransferTest
    {
        private static readonly SepaIbanData Creditor = new SepaIbanData
            {
                Bic = "SOGEFRPPXXX",
                Iban = "FR7030002005500000157845Z02",
                Name = "My Corp"
            };

        private readonly string FILENAME = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/sepa_debit_test_result.xml";

        private const string ONE_ROW_RESULT =
            "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"urn:iso:std:iso:20022:tech:xsd:pain.008.001.02\"><CstmrDrctDbtInitn><GrpHdr><MsgId>transferID</MsgId><CreDtTm>2013-02-17T22:38:12</CreDtTm><NbOfTxs>1</NbOfTxs><CtrlSum>23.45</CtrlSum><InitgPty><Nm>Me</Nm><Id><OrgId><Othr><Id>MyId</Id></Othr></OrgId></Id></InitgPty></GrpHdr><PmtInf><PmtInfId>paymentInfo</PmtInfId><PmtMtd>DD</PmtMtd><NbOfTxs>1</NbOfTxs><CtrlSum>23.45</CtrlSum><PmtTpInf><SvcLvl><Cd>SEPA</Cd></SvcLvl><LclInstrm><Cd>CORE</Cd></LclInstrm><SeqTp>OOFF</SeqTp></PmtTpInf><ReqdColltnDt>2013-02-17</ReqdColltnDt><Cdtr><Nm>My Corp</Nm></Cdtr><CdtrAcct><Id><IBAN>FR7030002005500000157845Z02</IBAN></Id><Ccy>EUR</Ccy></CdtrAcct><CdtrAgt><FinInstnId><BIC>SOGEFRPPXXX</BIC></FinInstnId></CdtrAgt><ChrgBr>SLEV</ChrgBr><CdtrSchmeId><Id><PrvtId><Othr><Id /><SchmeNm><Prtry>SEPA</Prtry></SchmeNm></Othr></PrvtId></Id></CdtrSchmeId><DrctDbtTxInf><PmtId><InstrId>Transaction Id 1</InstrId><EndToEndId>paymentInfo/1</EndToEndId></PmtId><InstdAmt Ccy=\"EUR\">23.45</InstdAmt><DrctDbtTx><MndtRltdInf><MndtId>First mandate</MndtId><DtOfSgntr>2012-12-07</DtOfSgntr></MndtRltdInf></DrctDbtTx><DbtrAgt><FinInstnId><BIC>AGRIFRPPXXX</BIC></FinInstnId></DbtrAgt><Dbtr><Nm>THEIR_NAME</Nm></Dbtr><DbtrAcct><Id><IBAN>FR1420041010050500013M02606</IBAN></Id></DbtrAcct><RmtInf><Ustrd>Transaction description</Ustrd></RmtInf></DrctDbtTxInf></PmtInf></CstmrDrctDbtInitn></Document>";

        private const string MULTIPLE_ROW_RESULT =
            "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"urn:iso:std:iso:20022:tech:xsd:pain.008.001.02\"><CstmrDrctDbtInitn><GrpHdr><MsgId>transferID</MsgId><CreDtTm>2013-02-17T22:38:12</CreDtTm><NbOfTxs>3</NbOfTxs><CtrlSum>63.36</CtrlSum><InitgPty><Nm>Me</Nm></InitgPty></GrpHdr><PmtInf><PmtInfId>paymentInfo</PmtInfId><PmtMtd>DD</PmtMtd><NbOfTxs>3</NbOfTxs><CtrlSum>63.36</CtrlSum><PmtTpInf><SvcLvl><Cd>SEPA</Cd></SvcLvl><LclInstrm><Cd>CORE</Cd></LclInstrm><SeqTp>OOFF</SeqTp></PmtTpInf><ReqdColltnDt>2013-02-18</ReqdColltnDt><Cdtr><Nm>My Corp</Nm></Cdtr><CdtrAcct><Id><IBAN>FR7030002005500000157845Z02</IBAN></Id><Ccy>EUR</Ccy></CdtrAcct><CdtrAgt><FinInstnId><BIC>SOGEFRPPXXX</BIC></FinInstnId></CdtrAgt><ChrgBr>SLEV</ChrgBr><CdtrSchmeId><Id><PrvtId><Othr><Id /><SchmeNm><Prtry>SEPA</Prtry></SchmeNm></Othr></PrvtId></Id></CdtrSchmeId><DrctDbtTxInf><PmtId><InstrId>Transaction Id 1</InstrId><EndToEndId>multiple1</EndToEndId></PmtId><InstdAmt Ccy=\"EUR\">23.45</InstdAmt><DrctDbtTx><MndtRltdInf><MndtId>First mandate</MndtId><DtOfSgntr>2012-12-07</DtOfSgntr></MndtRltdInf></DrctDbtTx><DbtrAgt><FinInstnId><BIC>AGRIFRPPXXX</BIC></FinInstnId></DbtrAgt><Dbtr><Nm>THEIR_NAME</Nm></Dbtr><DbtrAcct><Id><IBAN>FR1420041010050500013M02606</IBAN></Id></DbtrAcct><RmtInf><Ustrd>Transaction description</Ustrd></RmtInf></DrctDbtTxInf><DrctDbtTxInf><PmtId><InstrId>Transaction Id 2</InstrId><EndToEndId>paymentInfo/2</EndToEndId></PmtId><InstdAmt Ccy=\"EUR\">12.56</InstdAmt><DrctDbtTx><MndtRltdInf><MndtId>First mandate</MndtId><DtOfSgntr>2012-12-07</DtOfSgntr></MndtRltdInf></DrctDbtTx><DbtrAgt><FinInstnId><BIC>AGRIFRPPXXX</BIC></FinInstnId></DbtrAgt><Dbtr><Nm>THEIR_NAME</Nm></Dbtr><DbtrAcct><Id><IBAN>FR1420041010050500013M02606</IBAN></Id></DbtrAcct><RmtInf><Ustrd>Transaction description 2</Ustrd></RmtInf></DrctDbtTxInf><DrctDbtTxInf><PmtId><InstrId>Transaction Id 3</InstrId><EndToEndId>paymentInfo/3</EndToEndId></PmtId><InstdAmt Ccy=\"EUR\">27.35</InstdAmt><DrctDbtTx><MndtRltdInf><MndtId /><DtOfSgntr>0001-01-01</DtOfSgntr></MndtRltdInf></DrctDbtTx><DbtrAgt><FinInstnId><BIC>BANK_BIC</BIC></FinInstnId></DbtrAgt><Dbtr><Nm>NAME</Nm></Dbtr><DbtrAcct><Id><IBAN>ACCOUNT_IBAN_SAMPLE</IBAN></Id></DbtrAcct><RmtInf><Ustrd>Transaction description 3</Ustrd></RmtInf></DrctDbtTxInf></PmtInf></CstmrDrctDbtInitn></Document>";
        
        private static SepaDebitTransferTransaction CreateTransaction(string id, decimal amount, string information)
        {
            return new SepaDebitTransferTransaction
            {
                Id = id,
                Debtor = new SepaIbanData
                {
                    Bic = "AGRIFRPPXXX",
                    Iban = "FR1420041010050500013M02606",
                    Name = "THEIR_NAME"
                },
                MandateIdentification = "First mandate",
                DateOfSignature = new DateTime(2012, 12, 7),
                Amount = amount,
                RemittanceInformation = information
            };
        }

        private static SepaDebitTransfer GetEmptyDebitTransfert()
        {
            return new SepaDebitTransfer
            {
                CreationDate = new DateTime(2013, 02, 17, 22, 38, 12),
                RequestedExecutionDate = new DateTime(2013, 02, 17),
                MessageIdentification = "transferID",
                PaymentInfoId = "paymentInfo",
                InitiatingPartyName = "Me",
                Creditor = Creditor
            };
        }

        private static SepaDebitTransfer GetOneTransactionDebitTransfert(decimal amount)
        {
            var transfert = GetEmptyDebitTransfert();
            transfert.InitiatingPartyId = "MyId";
            transfert.AddDebitTransfer(CreateTransaction("Transaction Id 1",amount,"Transaction description"));
            return transfert;
        }

        //[OneTimeTearDown]
        //public void Cleanup()
        //{
        //    if (File.Exists(FILENAME))
        //        File.Delete(FILENAME);
        //}

        [TestMethod]
        public void ShouldAllowMultipleNullIdTransations()
        {
            const decimal amount = 23.45m;

            try {
                SepaDebitTransfer transfert = GetOneTransactionDebitTransfert(amount);

                transfert.AddDebitTransfer(new SepaDebitTransferTransaction
                    {
                        Id = null,
                        Debtor = new SepaIbanData
                            {
                                Bic = "AGRIFRPPXXX",
                                Iban = "FR1420041010050500013M02606",
                                Name = "THEIR_NAME"
                            },
                        Amount = amount,
                        RemittanceInformation = "Transaction description 1",
                        MandateIdentification = "mandate 1",
                        DateOfSignature = new DateTime(2010, 12, 7),
                    });

                transfert.AddDebitTransfer(new SepaDebitTransferTransaction
                    {
                        Id = null,
                        Debtor = new SepaIbanData
                            {
                                Bic = "AGRIFRPPXXX",
                                Iban = "FR1420041010050500013M02606",
                                Name = "THEIR_NAME"
                            },
                        Amount = amount,
                        RemittanceInformation = "Transaction description 2",
                        MandateIdentification = "mandate 2",
                        DateOfSignature = new DateTime(2011, 12, 7),
                    });
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void ShouldKeepEndToEndIdIfSet()
        {
            const decimal amount = 23.45m;

            SepaDebitTransfer transfert = GetOneTransactionDebitTransfert(amount);

            var trans = CreateTransaction(null, amount, "Transaction description 2");
            trans.EndToEndId = "endToendId1";
            transfert.AddDebitTransfer(trans);

            trans = CreateTransaction(null, amount, "Transaction description 3");
            trans.EndToEndId = "endToendId2";
            transfert.AddDebitTransfer(trans);

            string result = transfert.AsXmlString();

            Assert.IsTrue(result.Contains("<EndToEndId>endToendId1</EndToEndId>"));
            Assert.IsTrue(result.Contains("<EndToEndId>endToendId2</EndToEndId>"));
        }

        [TestMethod]
        public void ShouldManageMultipleTransactionsTransfer()
        {
            var transfert = new SepaDebitTransfer
                {
                    CreationDate = new DateTime(2013, 02, 17, 22, 38, 12),
                    RequestedExecutionDate = new DateTime(2013, 02, 18),
                    MessageIdentification = "transferID",
                    PaymentInfoId = "paymentInfo",
                    InitiatingPartyName = "Me",
                    Creditor = Creditor
                };

            const decimal amount = 23.45m;
            var trans = CreateTransaction("Transaction Id 1", amount, "Transaction description");
            trans.EndToEndId = "multiple1";
            transfert.AddDebitTransfer(trans);

            const decimal amount2 = 12.56m;
            transfert.AddDebitTransfer(CreateTransaction("Transaction Id 2", amount2, "Transaction description 2"));


            const decimal amount3 = 27.35m;
            transfert.AddDebitTransfer(new SepaDebitTransferTransaction
                {
                    Id = "Transaction Id 3",
                    Debtor = new SepaIbanData
                        {
                            Bic = "BANK_BIC",
                            Iban = "ACCOUNT_IBAN_SAMPLE",
                            Name = "NAME"
                        },
                    Amount = amount3,
                    RemittanceInformation = "Transaction description 3"
                });

            const decimal total = (amount + amount2 + amount3)*100;

            Assert.AreEqual(total, transfert.HeaderControlSumInCents);
            Assert.AreEqual(total, transfert.PaymentControlSumInCents);

            Assert.AreEqual(MULTIPLE_ROW_RESULT, transfert.AsXmlString());
            
        }

        [TestMethod]
        public void ShouldValidateThePain00800102XmlSchema()
        {
            var transfert = new SepaDebitTransfer
            {
                CreationDate = new DateTime(2013, 02, 17, 22, 38, 12),
                RequestedExecutionDate = new DateTime(2013, 02, 18),
                MessageIdentification = "transferID",
                PaymentInfoId = "paymentInfo",
                InitiatingPartyName = "Me",
                Creditor = Creditor
            };

            const decimal amount = 23.45m;
            var trans = CreateTransaction("Transaction Id 1", amount, "Transaction description");
            trans.EndToEndId = "multiple1";
            transfert.AddDebitTransfer(trans);

            const decimal amount2 = 12.56m;
            transfert.AddDebitTransfer(CreateTransaction("Transaction Id 2", amount2, "Transaction description 2"));


            const decimal amount3 = 27.35m;
            transfert.AddDebitTransfer(new SepaDebitTransferTransaction
            {
                Id = "Transaction Id 3",
                Debtor = new SepaIbanData
                {
                    Bic = "BANK_BIC",
                    Iban = "ACCOUNT_IBAN_SAMPLE",
                    Name = "NAME"
                },
                Amount = amount3,
                RemittanceInformation = "Transaction description 3"
            });

            var validator = XmlValidator.GetValidator(transfert.Schema);
            validator.Validate(transfert.AsXmlString());
        }

        [TestMethod]
        public void ShouldValidateThePain00800103XmlSchema()
        {
            var transfert = new SepaDebitTransfer
            {
                CreationDate = new DateTime(2013, 02, 17, 22, 38, 12),
                RequestedExecutionDate = new DateTime(2013, 02, 18),
                MessageIdentification = "transferID",
                PaymentInfoId = "paymentInfo",
                InitiatingPartyName = "Me",
                Creditor = Creditor,
                Schema = SepaSchema.Pain00800103
            };

            const decimal amount = 23.45m;
            var trans = CreateTransaction("Transaction Id 1", amount, "Transaction description");
            trans.EndToEndId = "multiple1";
            transfert.AddDebitTransfer(trans);

            const decimal amount2 = 12.56m;
            transfert.AddDebitTransfer(CreateTransaction("Transaction Id 2", amount2, "Transaction description 2"));


            const decimal amount3 = 27.35m;
            transfert.AddDebitTransfer(new SepaDebitTransferTransaction
            {
                Id = "Transaction Id 3",
                Debtor = new SepaIbanData
                {
                    Bic = "BANK_BIC",
                    Iban = "ACCOUNT_IBAN_SAMPLE",
                    Name = "NAME"
                },
                Amount = amount3,
                RemittanceInformation = "Transaction description 3"
            });

            var validator = XmlValidator.GetValidator(transfert.Schema);
            validator.Validate(transfert.AsXmlString());
        }

        [TestMethod]
        public void ShouldRejectNotAllowedXmlSchema()
        {
            try { new SepaDebitTransfer { Schema = SepaSchema.Pain00100103 }; }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("schema is not allowed!"));
            }
        }

        [TestMethod]
        public void ShouldManageOneTransactionTransfer()
        {
            const decimal amount = 23.45m;
            SepaDebitTransfer transfert = GetOneTransactionDebitTransfert(amount);

            const decimal total = amount*100;
            Assert.AreEqual(total, transfert.HeaderControlSumInCents);
            Assert.AreEqual(total, transfert.PaymentControlSumInCents);

            Assert.AreEqual(ONE_ROW_RESULT, transfert.AsXmlString());
        }

        [TestMethod]
        public void ShouldAllowTransactionWithoutRemittanceInformation()
        {
            var transfert = GetEmptyDebitTransfert();
            transfert.AddDebitTransfer(CreateTransaction(null, 12m, null));
            transfert.AddDebitTransfer(CreateTransaction(null, 13m, null));

            string result = transfert.AsXmlString();
            Assert.IsFalse(result.Contains("<RmtInf>"));
        }

        [TestMethod]
        public void ShouldRejectIfNoCreditor()
        {
            var transfert = new SepaDebitTransfer
                {
                    MessageIdentification = "transferID",
                    PaymentInfoId = "paymentInfo",
                    InitiatingPartyName = "Me"
                };
            transfert.AddDebitTransfer(CreateTransaction("Transaction Id 1", 100m, "Transaction description"));
            try { transfert.AsXmlString(); }
            catch (SepaRuleException e)
            {
                Assert.AreEqual(e.Message, "The creditor is mandatory.");
            }
            
        }

        [TestMethod]
        public void ShouldAcceptNoInitiatingPartyName()
        {
            SepaDebitTransfer transfert = GetOneTransactionDebitTransfert(100m);
            transfert.InitiatingPartyName = null;
            try { transfert.AsXmlString(); }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void ShouldRejectIfNoMessageIdentification()
        {
            SepaDebitTransfer transfert = GetOneTransactionDebitTransfert(100m);
            transfert.MessageIdentification = null;
            try { transfert.AsXmlString(); }
            catch (SepaRuleException e)
            {
                Assert.Equals(e.Message, "The message identification is mandatory.");
            }
        }

        [TestMethod]
        public void ShouldUseMessageIdentificationAsPaymentInfoIdIfNotDefined()
        {
            SepaDebitTransfer transfert = GetOneTransactionDebitTransfert(100m);
            transfert.PaymentInfoId = null;

            string result = transfert.AsXmlString();

            Assert.IsTrue(result.Contains("<PmtInfId>"+ transfert.MessageIdentification + "</PmtInfId>"));
        }

        [TestMethod]
        public void ShouldRejectIfNoTransaction()
        {
            var transfert = new SepaDebitTransfer
                {
                    MessageIdentification = "transferID",
                    PaymentInfoId = "paymentInfo",
                    InitiatingPartyName = "Me",
                    Creditor = Creditor
                };

            try { transfert.AsXmlString(); }
            catch (SepaRuleException e)
            {
                Assert.AreEqual(e.Message, "At least one transaction is needed in a transfer.");
            }
        }

        [TestMethod]
        public void ShouldRejectInvalidCreditor()
        {
            try { new SepaDebitTransfer { Creditor = new SepaIbanData() }; }
            catch (SepaRuleException e)
            {
                Assert.AreEqual(e.Message, "Creditor IBAN data are invalid.");
            }
        }

        [TestMethod]
        public void ShouldRejectCreditorWithoutBic()
        {
            var iban = (SepaIbanData)Creditor.Clone();
            iban.UnknownBic = true;
            try { new SepaDebitTransfer { Creditor = iban }; }
            catch (SepaRuleException e)
            {
                Assert.Equals(e.Message, "Creditor IBAN data are invalid.");
            }
        }

        [TestMethod]
        public void ShouldRejectNullTransactionTransfer()
        {
            var transfert = new SepaDebitTransfer();


            try { transfert.AddDebitTransfer(null); }
            catch (ArgumentNullException e)
            {
                Assert.IsTrue(e.Message.Contains("transfer"));
            }
        }

        [TestMethod]
        public void ShouldRejectTwoTransationsWithSameId()
        {
            SepaDebitTransfer transfert = GetOneTransactionDebitTransfert(100m);
            var debit = CreateTransaction("Transaction Id 1", 23.45m, "Transaction description 2");

            try { transfert.AddDebitTransfer(debit); }
            catch (SepaRuleException e)
            {
                Assert.IsTrue(e.Message.Contains("must be unique in a transfer"));
            }
        }
        
        [TestMethod]
        public void ShouldUseADefaultCurrency()
        {
            var transfert = new SepaDebitTransfer();
            Assert.AreEqual("EUR", transfert.CreditorAccountCurrency);
        }

        [TestMethod]
        public void ShouldUseADefaultLocalInstrumentCode()
        {
            var transfert = new SepaDebitTransfer();
            Assert.AreEqual("CORE", transfert.LocalInstrumentCode);
        }


        [TestMethod]
        public void ShouldSaveDebitInXmlFile()
        {
            const decimal amount = 23.45m;

            SepaDebitTransfer transfert = GetOneTransactionDebitTransfert(amount);
            transfert.Save(FILENAME);

            var doc = new XmlDocument();
            doc.Load(FILENAME);

            Assert.AreEqual(ONE_ROW_RESULT, doc.OuterXml);
        }
    }
}