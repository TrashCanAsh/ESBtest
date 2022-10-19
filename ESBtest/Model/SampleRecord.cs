using ESBtest.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESBtest.Model
{
    class SampleRecord : NotifyBase
    {
        #region property
        private int num;
        public int Num
        {
            get { return num; }
            set
            {
                num = value;
                RaisePropertyChanged();
            }
        }

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

        private DateTime requestDate;
        public DateTime RequestTime
        {
            get { return requestDate; }
            set
            {
                requestDate = value;
                RaisePropertyChanged();
            }
        }

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




        #endregion
    }
}
