﻿using System;
using System.Xml;

namespace OfxSharp
{
    public static class OFXHelperMethods
    {
        /// <summary>
        ///     Converts string representation of AccountInfo to enum AccountInfo
        /// </summary>
        /// <param name="bankAccountType">representation of AccountInfo</param>
        /// <returns>AccountInfo</returns>
        public static BankAccountType GetBankAccountType(this string bankAccountType)
        {
            try
            {
                return (BankAccountType)Enum.Parse(typeof(BankAccountType), bankAccountType, true);
            }
            catch (Exception)
            {
                return BankAccountType.NA;
            }
        }

        /// <summary>
        ///     Flips date from YYYYMMDD to DDMMYYYY
        /// </summary>
        /// <param name="date">Date in YYYYMMDD format</param>
        /// <returns>Date in format DDMMYYYY</returns>
        public static DateTime? ToDate(this string date)
        {
            try
            {
                if (date.Length < 8)
                    return null;

                int dd;
                int.TryParse(date.Substring(6, 2), out dd);
                int mm;
                int.TryParse(date.Substring(4, 2), out mm);
                int yyyy;
                int.TryParse(date.Substring(0, 4), out yyyy);

                if (yyyy == 0 || mm == 0 || dd == 0)
                    return null;

                return new DateTime(yyyy, mm, dd);
            }
            catch
            {
                throw new OFXParseException("Unable to parse date");
            }
        }

        /// <summary>
        ///     Returns value of specified node
        /// </summary>
        /// <param name="node">Node to look for specified node</param>
        /// <param name="xpath">XPath for node you want</param>
        /// <returns></returns>
        public static string GetValue(this XmlNode node, string xpath)
        {
            var tempNode = node.SelectSingleNode(xpath);

            if (tempNode != null && tempNode.FirstChild != null)
                return tempNode.FirstChild.Value;
            return string.Empty;
        }
    }
}