/* using Website.Models;
using System.Collections.Generic;
namespace Website.StaticData
{
    public class UserData
    {
        public static List<UserModel> People 
        {
            get
            {
                return listOfUsers;
            }
        }

        private static List<UserModel> listOfUsers = new List<UserModel>()
        {
            new UserModel() { UserId = 1, Email = "email@what.creativity", Password = SecurePasswordHasherHelper.Hash("Hashed"), FullName = "Tom Scott", Role = "Admin" },
            new UserModel() { UserId = 2, Email = "email@example", Password = SecurePasswordHasherHelper.Hash("exe"), FullName = "Minion", Role = "dev" },
            new UserModel() { UserId = 3, Email = "email@test.default", Password = SecurePasswordHasherHelper.Hash("test"), FullName = "Slave", Role = "dev" },
        };
    }
} */