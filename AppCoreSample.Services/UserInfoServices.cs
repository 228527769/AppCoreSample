using AppCoreSample.IServices;
using AppCoreSample.Model;
using System;
using System.Collections.Generic;

namespace AppCoreSample.Services
{
    public class UserInfoServices : IUserInfoServices
    {
        #region 模拟数据
        private List<UserInfo> userInfos = new List<UserInfo>
        {
            new UserInfo(){ 
                id=1,
                LoginName="zhangsan",
                LoginPWD="zhangsan123456",
                Sex=1,
                Status=1,
                Remark="test"
            },

            new UserInfo(){
                id=2,
                LoginName="lisi",
                LoginPWD="lisi23456",
                Sex=1,
                Status=1,
                Remark="test"
            },

            new UserInfo(){
                id=3,
                LoginName="wangwu",
                LoginPWD="wangwu123456",
                Sex=1,
                Status=1,
                Remark="test"
            },
        };
        #endregion

        public List<UserInfo> GetAll() => this.userInfos;

        public UserInfo GetUserInfoById(int id) => this.userInfos.Find(x=>x.id==id);
    }
}
