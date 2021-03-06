using System;
using System.Globalization;
using System.Xml;

namespace OfxSharp
{
    public class Balance
    {
        public Balance(XmlNode ledgerNode, XmlNode avaliableNode)
        {
            var tempLedgerBalance = ledgerNode.GetValue("//BALAMT");

            if (!string.IsNullOrEmpty(tempLedgerBalance))
                LedgerBalance = Convert.ToDecimal(tempLedgerBalance, CultureInfo.InvariantCulture);
            else
                throw new OFXParseException("Ledger balance has not been set");

            // ***** OFX files from my bank don't have the 'avaliableNode' node, so i manage a null situation
            if (avaliableNode == null)
            {
                AvaliableBalance = 0;

                // ***** this member veriable should be a nullable DateTime, declared as:
                // public DateTime? LedgerBalanceDate { get; set; }
                // and next line could be:
                // AvaliableBalanceDate = null;
                AvaliableBalanceDate = new DateTime();
            }
            else
            {
                var tempAvaliableBalance = avaliableNode.GetValue("//BALAMT");

                if (!string.IsNullOrEmpty(tempAvaliableBalance))
                    AvaliableBalance = Convert.ToDecimal(tempAvaliableBalance, CultureInfo.InvariantCulture);
                else
                    throw new OFXParseException("Avaliable balance has not been set");
                AvaliableBalanceDate = avaliableNode.GetValue("//DTASOF").ToDate();
            }

            LedgerBalanceDate = ledgerNode.GetValue("//DTASOF").ToDate();
        }

        public decimal LedgerBalance { get; set; }

        public DateTime? LedgerBalanceDate { get; set; }

        public decimal AvaliableBalance { get; set; }

        public DateTime? AvaliableBalanceDate { get; set; }
    }
}