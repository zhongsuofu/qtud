using System;
using System.Collections.Generic;
using System.Text;
using Qtud.DBManage.Manager;
using Qtud.DBManage.Model;

namespace Qtud.Qtud
{
    /// <summary>
    /// ��ǰ�û���Ϣ
    /// </summary>
    public class CurrentUser
    {
        #region  ȫ�ֱ���
        //public static PublicConst.CurUserType _CurUserType = PublicConst.CurUserType.CommonUser;  //��ǰ�û����ͣ�Ĭ��Ϊ��ͨ�û�
        public static UserModel _CurUserModel = null;       //��ǰ��¼�û�����
        #endregion

        #region ���캯��
        public CurrentUser()
        {
        }
        #endregion

        #region Ч�鵱ǰ�û��Ƿ����
        /// <summary>
        /// Ч�鵱ǰ�û��Ƿ����
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
