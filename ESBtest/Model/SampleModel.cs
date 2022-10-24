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
        /// <summary>
        /// 表格第一列中的checkbox是否为选中状态（表格）
        /// </summary>
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
        
        /// <summary>
        /// 样品ID信息
        /// </summary>
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
        
        /// <summary>
        /// 样品名称信息
        /// </summary>
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
        
        /// <summary>
        /// 样品种类信息
        /// </summary>
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

        /// <summary>
        /// 样品种类标签信息
        /// </summary>
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

        /// <summary>
        /// 采样时间信息（字符串类型）
        /// </summary>
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

        /// <summary>
        /// 采样时间信息（DataTime类型）
        /// </summary>
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

        /// <summary>
        /// 采样地点（字符串）
        /// </summary>
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

        /// <summary>
        /// 采样经度
        /// </summary>
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

        /// <summary>
        /// 采样纬度
        /// </summary>
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

        /// <summary>
        /// 样品状态
        /// 0-未知；1-在库；2-锁定；3-借出
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
        /// 样品状态（字符串类型）
        /// </summary>
        private string statestr;
        public string StateStr
        {
            get { return statestr; }
            set
            {
                statestr = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 样品备注
        /// </summary>
        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 是否为当前用户收藏夹中样品（表格）
        /// </summary>
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

        /// <summary>
        /// 是否为当前用户购物车中样品（表格）
        /// </summary>
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
