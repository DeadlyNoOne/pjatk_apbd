using System;

namespace LegacyApp
{
    public class User
    {
        public object Client { get; internal set; }
        public DateTime DateOfBirth { get; internal set; }
        public string EmailAddress { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }


        public bool setCreditLimit()
        {
            switch (((Client)this.Client).Name)
            {
                case "VeryImportantClient":
                    this.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                    setCreditLimit(2);
                    break;
                default:
                    this.HasCreditLimit = true;
                    setCreditLimit(1);
                    break;

            }
            return true;
        }

        public bool setCreditLimit(int multipleLimit)
        {
            using (var userCreditService = new UserCreditService())
            {
                this.CreditLimit = userCreditService.GetCreditLimit(this.FirstName, this.LastName, this.DateOfBirth) * multipleLimit; ;
            }

            return true;
        }

        public bool isRich()
        {
            if (this.HasCreditLimit && this.CreditLimit < 500)
            {
                return false;
            }
            return true;
        }

    }


}