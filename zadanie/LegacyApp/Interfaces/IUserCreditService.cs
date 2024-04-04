using System;
using System.Collections.Generic;

namespace LegacyApp;

public interface IUserCreditService
{
    public void Dispose();
    public int GetCreditLimit(string lastName, DateTime dateOfBirth);
}