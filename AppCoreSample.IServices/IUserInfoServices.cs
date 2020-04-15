using AppCoreSample.Model;
using System;
using System.Collections.Generic;

namespace AppCoreSample.IServices
{
    public interface IUserInfoServices
    {
        List<UserInfo> GetAll();

        UserInfo GetUserInfoById(int id);
    }
}
