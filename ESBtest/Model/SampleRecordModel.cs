using ESBtest.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESBtest.Model
{
    public class SampleRecordModel : NotifyBase
    {
        #region property
        /// <summary>
        /// 申请计数
        /// </summary>
        private int idSamplesRecord;
        public int IdSamplesRecord
        {
            get { return idSamplesRecord; }
            set
            {
                idSamplesRecord = value;
                RaisePropertyChanged();
            }
        }
        
        /// <summary>
        /// 用户申请记录
        /// 同一次申请会放在一次记录中
        /// </summary>
        private int idRecord;
        public int IdRecord
        {
            get { return idRecord; }
            set
            {
                idRecord = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 申请用户ID
        /// 外键
        /// </summary>
        private int idUser;
        public int IdUser
        {
            get { return idUser; }
            set
            {
                idUser = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 申请用户名称
        /// </summary>
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// 申请样品ID
        /// 外键
        /// </summary>
        private int idSamples;
        public int IdSamples
        {
            get { return idSamples; }
            set
            {
                idSamples = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 申请日期
        /// </summary>
        private DateTime requestDate;
        public DateTime RequestDate
        {
            get { return requestDate; }
            set
            {
                requestDate = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 申请备注
        /// </summary>
        private string requestComment;
        public string RequestComment
        {
            get { return requestComment; }
            set
            {
                requestComment = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 申请通过日期
        /// </summary>
        private DateTime approvalDate;
        public DateTime ApprovalDate
        {
            get { return approvalDate; }
            set
            {
                approvalDate = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 审批人管理员ID
        /// </summary>
        private int idAdmin;
        public int IdAdmin
        {
            get { return idAdmin; }
            set
            {
                idAdmin = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 审批备注
        /// </summary>
        private string approvalComment;
        public string ApprovalComment
        {
            get { return approvalComment; }
            set
            {
                approvalComment = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 申请状态(int)
        /// 0-未知 1-正在审批 2-审批通过 3-已借出 4-已归还 9-审批未通过
        /// 10-用户取消 
        /// </summary>
        private int state;
        public int State
        {
            get { return state; }
            set
            {
                state = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 申请状态（字符串）
        /// </summary>
        private string stateStr;
        public string StateStr
        {
            get { return stateStr; }
            set
            {
                stateStr = value;
                RaisePropertyChanged();
            }
        }


        public SampleRecordModel()
        {
            this.RequestDate = DateTime.Now;
            this.ApprovalDate = DateTime.Now;
        }


        #endregion
    }
}
