using ESBtest.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESBtest.Model
{
    /// <summary>
    /// 样品数据模型
    /// </summary>
    public class SampleModel:NotifyBase
    {
        #region property
        private string sampleID;

        public string SampleID
        {
            get { return sampleID; }
            set
            {
                sampleID = value;
                RaisePropertyChanged();
            }
        }

        private string sampleName;

        public string SampleName
        {
            get { return sampleName; }
            set
            {
                sampleName = value;
                RaisePropertyChanged();
            }
        }

        private string category;

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                RaisePropertyChanged();
            }
        }

        private int categoryIndex;

        public int CategoryIndex
        {
            get { return categoryIndex; }
            set
            {
                categoryIndex = value;
                RaisePropertyChanged();
            }
        }



        private string samplingTime;

        public string SamplingTime
        {
            get { return samplingTime; }
            set
            {
                samplingTime = value;
                RaisePropertyChanged();
            }
        }

        private DateTime samplingDateTime;

        public DateTime SamplingDateTime
        {
            get { return samplingDateTime; }
            set
            {
                samplingDateTime = value;
                RaisePropertyChanged();
            }
        }


        private string longtitude;

        public string Longtitude
        {
            get { return longtitude; }
            set
            {
                longtitude = value;
                RaisePropertyChanged();
            }
        }

        private string latitude;

        public string Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                RaisePropertyChanged();
            }
        }


        #endregion
    }
}
