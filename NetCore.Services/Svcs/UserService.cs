//using NetCore.Data.DataModels;
using NetCore.Data.Classes;
using NetCore.Data.ViewModels;
using NetCore.Services.Data;
using NetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.Services.Svcs
{
    public class UserService:IUser
    {
        private DBFirstDbContext _context;

        public UserService(DBFirstDbContext context)
        {
            _context = context;
        }

        // Data Model : Database 와 연동할 Model
        // View Model : View를 위한 Model
        #region private methods
        private IEnumerable<User> GetUserInfos()
        {
            return _context.Users.ToList();
            //return new List<User>()
            //{
            //    new User()
            //    {
            //        UserId = "user001",
            //        UserName = "김유저",
            //        UserEmail = "user001@gmail.com",
            //        Password = "123456"                    
            //    }
            //};
        }

        private bool checkTheUserInfo(string userId, string password)
        {
            return GetUserInfos().Where(user => user.UserId.Equals(userId) && user.Password.Equals(password)).Any();
        }

        #endregion

        // Interface를 상속 받은 후에 명시적으로 Interface 구현
        bool IUser.MatchTheUserInfo(LoginInfo loginInfo)
        {
            return checkTheUserInfo(loginInfo.UserId, loginInfo.Password);
        }


    }
}
