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
        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChanged();
            }
        }


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

        private string samplingLocation;

        public string SamplingLocation
        {
            get { return samplingLocation; }
            set
            {
                samplingLocation = value;
                RaisePropertyChanged();
            }
        }



        private string longitude;

        public string Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
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

        private bool isFavorited;

        public bool IsFavorited
        {
            get { return isFavorited; }
            set
            {
                isFavorited = value;
                RaisePropertyChanged();
            }
        }

        private bool isInCart;

        public bool IsInCart
        {
            get { return isInCart; }
            set
            {
                isInCart = value;
                RaisePropertyChanged();
            }
        }


        public SampleModel()
        {
            this.SamplingDateTime = DateTime.Now;
            this.IsSelected = false;
            this.IsFavorited = false;
            this.IsInCart = false;
        }


        #endregion
    }
}
