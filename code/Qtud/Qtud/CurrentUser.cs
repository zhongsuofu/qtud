using System;
using System.Collections.Generic;
using System.Text;
using Qtud.DBManage.Manager;
using Qtud.DBManage.Model;

namespace Qtud.Qtud
{
    /// <summary>
    /// 当前用户信息
    /// </summary>
    public class CurrentUser
    {
        #region  全局变量
        //public static PublicConst.CurUserType _CurUserType = PublicConst.CurUserType.CommonUser;  //当前用户类型，默认为普通用户
        public static UserModel _CurUserModel = null;       //当前登录用户对象。
        #endregion

        #region 构造函数
        public CurrentUser()
        {
        }
        #endregion

        #region 效验当前用户是否存在
        /// <summary>
        /// 效验当前用户是否存在
        /// </summary>
        /// <returns></returns>
        public static int IsCurUserModelExist()
        {
            try
            {
                if (_CurUserModel == null)
                {
                    return -1;
                }
                return 0;
            }
            catch (System.Exception e)
            {
                throw e; 
            }
        }
        #endregion
    }
}
