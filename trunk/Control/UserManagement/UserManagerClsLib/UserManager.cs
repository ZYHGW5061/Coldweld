using GlobalDataDefineClsLib;
using GWUserManager.Entity;
using GWUserManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GWUserManager.Entity.User;

namespace UserManagerClsLib
{
    public class UserManager
    {
        UserService userService = new UserService();
        private static volatile UserManager _instance;
        private static readonly object _lockObj = new object();
        public static UserManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new UserManager();
                        }
                    }
                }
                return _instance;
            }
        }
        public int CurrentUserId { get; set; }
        public string CurrentUserName { get; set; }
        public int CurrentUserType { get; set; }
        private UserManager()
        {
            GetUserByName("admin");
        }
        public EnumLoginResult Login(string username, string password, ref int userType)
        {
            var ret = EnumLoginResult.UserNotExist;
            try
            {
                User user = userService.login(username, password);
                //User user = new User();
                //user.UserName = "admin";
                //user.Password = "123";
                //user.Level = User.LevelType.admin;

                //int aaa = userService.AddUser(user);
                if (user != null)
                {
                    CurrentUserId = user.Id;
                    userType = (int)user.Level;
                    ret = EnumLoginResult.Success;
                }
            }
            catch (Exception ex)
            {

            }
            return ret;
        }
        public void Logout(string username, string password)
        {
        }
        public bool AddUser(string name, string password, int level, string description)
        {
            User user = new User();
            user.UserName = name;
            user.Password = password;
            user.Level = (EnumUserType)level;
            user.Desc = description;
            int ret = userService.AddUser(user);
            return ret > 0;
        }
        public bool DeleteUser(string name)
        {
            var ret = 0;
            User user = userService.GetUserByName(name);
            if (user != null)
            {
                ret = userService.DeleteUser(user);
            }
            return ret > 0;
        }
        public bool ChangeUserInfos(string oldName, string newName, string password, int level, string description)
        {
            var ret = 0;
            var changePasswordRet = false;
            User user = userService.GetUserByName(oldName);
            if (user != null)
            {
                user.UserName = newName;
                user.Level = (User.EnumUserType)level;
                user.Desc = description;
                ret = userService.ModifyUser(user);
                changePasswordRet = userService.ChgPassword(user, password);
            }
            return (ret > 0) & changePasswordRet;
        }
        public List<RightsInfo> GetAllLevel()
        {
            var temp = new List<RightsInfo>();
            foreach (var item in Enum.GetValues(typeof(User.EnumUserType)))
            {
                temp.Add(new RightsInfo { RightsID = (int)item, RightsType = item.ToString() });
            }

            return temp;
        }
        public User GetUserByName(string name)
        {
            return userService.GetUserByName("admin");
        }
        public List<UserInfos> GetAllUsers()
        {
            var ret = new List<UserInfos>();
            foreach (var item in userService.GetAllUsers())
            {
                ret.Add(new UserInfos { id = item.Id, username = item.UserName, password = item.Password, usertype = item.Level.ToString(), description = item.Desc });
            }
            return ret;
        }
        public int GetUserTypeIndex(string userType)
        {
            var type = EnumUserType.Operator;
            Enum.TryParse<EnumUserType>(userType, out type);
            return (int)type;
        }
    }
}
