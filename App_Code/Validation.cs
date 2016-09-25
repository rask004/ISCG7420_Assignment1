using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI.WebControls;

namespace SecurityLayer
{
    /// <summary>
    ///     Provides constants and helpers for data validation.
    /// </summary>
    public static class Validation
    {
        public static readonly string ValidationAlphabetic = "abcdefghijklmnopqrstuvwxyzQAZXSWEDCVFRTGBNHYUJMKILOP";
        public static readonly string ValidationNameGeneral = "abcdefghijklmnopqrstuvwxyzQAZXSWEDCVFRTGBNHYUJMKILOP .,'";
        public static readonly string ValidationNumerals = "0123456789";

        public static readonly string ValidationEmailErrorMessageGeneral = "The User must have a valid Email.";
        public static readonly string ValidationEmailErrorMessageInUse = "The User Email is already in use.";

        public static readonly string ValidationLoginErrorMessageGeneral = "The User Login must only have alphanumeric characters.";
        public static readonly string ValidationLoginErrorMessageInUse = "The User Login is already in use.";

        /// <summary>
        ///     Validate that input is only alphabetic.
        /// </summary>
        /// <param name="args"></param>
        public static void ValidateAlphabeticInput(ref ServerValidateEventArgs args)
        {
            foreach (char c in args.Value)
            {
                if (!Validation.ValidationAlphabetic.Contains(c))
                {
                    args.IsValid = false;
                    return;
                }
            }

            args.IsValid = true;
        }

        /// <summary>
        ///     Validate that input is an email address
        /// </summary>
        /// <param name="args"></param>
        public static void ValidateEmailInput(ref ServerValidateEventArgs args)
        {
            try
            {
                new MailAddress(args.Value.ToString());
            }
            catch (FormatException ex)
            {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }

        /// <summary>
        ///     Validate that input is alphabetic or numeric
        /// </summary>
        /// <param name="args"></param>
        public static void ValidateAlphaNumericInput(ref ServerValidateEventArgs args)
        {
            foreach (char c in args.Value)
            {
                if (!Validation.ValidationAlphabetic.Contains(c)
                    && !Validation.ValidationNumerals.Contains(c))
                {
                    args.IsValid = false;
                    return;
                }
            }

            args.IsValid = true;
        }

        /// <summary>
        ///     Validate that a number can represent a landline number 
        /// </summary>
        /// <param name="args"></param>
        public static void ValidateLandlineNumber(ref ServerValidateEventArgs args)
        {
            if (args.Value.Length < 8 || args.Value.Length > 10 || args.Value[0] != '0')
            {
                args.IsValid = false;
                return;
            }

            foreach (char c in args.Value)
            {
                if (!Validation.ValidationNumerals.Contains(c))
                {
                    args.IsValid = false;
                    return;
                }
            }

            long contactNumber = Convert.ToInt64(args.Value.Substring(1));

            if (contactNumber < 1000001 || contactNumber > 999999999)
            {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }

        /// <summary>
        ///     Validate that a number can be a mobile number
        ///     Can be in international (+CCRRNNNNNN[NN]) or local (0RRNNNNNN[NN])
        /// </summary>
        /// <param name="args"></param>
        public static void ValidateMobileNumber(ref ServerValidateEventArgs args)
        {
            string numberDigits = String.Empty;
            try
            {

                // international format e.g. +64 22 55556666
                if (args.Value[0] == '+')
                {
                    if (!Validation.ValidationNumerals.Contains(args.Value[1])
                        || !Validation.ValidationNumerals.Contains(args.Value[2])
                        || !Validation.ValidationNumerals.Contains(args.Value[3])
                        || !Validation.ValidationNumerals.Contains(args.Value[4]))
                    {
                        args.IsValid = false;
                        return;
                    }

                    numberDigits = args.Value.Substring(5);
                }

                // national format e.g. 022 55556666
                else if (args.Value[0] == '0')
                {
                    if ((args.Value[1] != '2' ||
                         !Validation.ValidationNumerals.Contains(args.Value[2])))
                    {
                        args.IsValid = false;
                        return;
                    }

                    numberDigits = args.Value.Substring(3);
                }
                else
                {
                    args.IsValid = false;
                    return;
                }

                // parse remaining digits!
                foreach (char c in numberDigits)
                {
                    if (!Validation.ValidationNumerals.Contains(c))
                    {
                        args.IsValid = false;
                        return;
                    }
                }

                long contactNumber = Convert.ToInt64(numberDigits);

                if (contactNumber < 100001 || contactNumber > 99999999)
                {
                    args.IsValid = false;
                    return;
                }
            }
            catch (IndexOutOfRangeException)
            {
                // too short to be a valid number
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }

        /// <summary>
        ///     Validate input representing a password meets a minimum length.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="length"></param>
        public static void ValidatePasswordLength(ref ServerValidateEventArgs args, int length)
        {
            if (length < 1)
            {
                throw new ArgumentException("Minimum password length parameter must be 1 or greater.", "length");    
            }

            if (args.Value.Length < length)
            {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }

        /// <summary>
        ///     Validate input matches a street address (number[letter] name [suffix...])
        /// </summary>
        /// <param name="args"></param>
        public static void ValidateStreetAddress(ref ServerValidateEventArgs args)
        {
            string[] address = args.Value.Split(' ');
            string addressNumber = address[0];

            // address format: <numerals>[one letter] <letters> [letters...] e.g. 123 Samson St, 34a Happy Valley Rise Rd
            if (!Validation.ValidationAlphabetic.Contains(addressNumber[addressNumber.Length - 1])
                && !Validation.ValidationNumerals.Contains(addressNumber[addressNumber.Length - 1]))
            {
                // last char in address number must be letter or number e.g. 34a, 127
                args.IsValid = false;
                return;
            }

            foreach (char c in addressNumber.Substring(0, addressNumber.Length - 1))
            {
                // other chars in addressNumber must be numerals
                if (!Validation.ValidationNumerals.Contains(c))
                {
                    args.IsValid = false;
                    return;
                }
            }

            for (int i = 1; i < address.Length; i++)
            {
                // each other address part should only have alphabet letters
                foreach (char c in address[i])
                {
                    // other chars in addressNumber must be numerals
                    if (!Validation.ValidationAlphabetic.Contains(c))
                    {
                        args.IsValid = false;
                        return;
                    }
                }
            }

            args.IsValid = true;
        }

        /// <summary>
        ///     Validate input as alphanumeric, ignoting periods, commas and apostrophes.
        /// </summary>
        /// <param name="args"></param>
        public static void ValidateGenericName(ref ServerValidateEventArgs args)
        {
            foreach (char c in args.Value)
            {
                if (!Validation.ValidationNameGeneral.Contains(c))
                {
                    args.IsValid = false;
                    return;
                }
            }

            args.IsValid = true;
        }

        /// <summary>
        ///     Validate that input can represent a money value (number to two decimal places)
        /// </summary>
        /// <param name="args"></param>
        public static void ValidateMoneyInput(ref ServerValidateEventArgs args)
        {
            try
            {
                Convert.ToDouble(args.Value);
            }
            catch (FormatException ex)
            {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }
    }
}
