using System;

namespace LegacyApp
{
    /*
     * UI - user interface (HTML, console)
     * BL - business logic
     * Infrastructure - I/O (SQL queries, mouse click, e-mail insertion)
     */
    public class UserService
    {
        public UserService()
        {
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            //BL
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            //BL
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            //BL
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            //BL
            if (age < 21)
            {
                return false;
            }

            //Infrastructure - communication with database 
            //coupling to ClientRepository class 
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            //coupling to User class
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            //BL + Infrastructure
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                //coupling to UserCreditService
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                //coupling to UserCreditService
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            //BL
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            //Infrastructure
            //Coupling to UserDataAccess
            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
