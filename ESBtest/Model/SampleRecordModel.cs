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
            get { return num; }
            set
            {
                num = value;
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
        /// 申请通过日期
        /// </summary>
        private DateTime approvalDate;
        public DateTime Approvaldate
        {
            get { return approvalDate; }
            set
            {
                approvalDate = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 借出日期
        /// </summary>
        private DateTime outDate;
        public DateTime OutDate
        {
            get { return outDate; }
            set
            {
                outDate = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 还入日期
        /// </summary>
        private DateTime inDate;
        public DateTime InDate
        {
            get { return inDate; }
            set
            {
                inDate = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 申请状态(int)
        /// 0-未知 1-正在审批 2-审批通过 3-已借出 4-已归还
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
            this.Approvaldate = DateTime.Now;
            this.OutDate = DateTime.Now;
            this.InDate = DateTime.Now;
        }


        #endregion
    }
}
