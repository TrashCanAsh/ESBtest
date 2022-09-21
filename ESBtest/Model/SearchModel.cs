using ESBtest.Common;
using ESBtest.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESBtest.Model
{
    public class SearchModel : NotifyBase
    {
        #region property
        private string keyWord;

        public string KeyWord
        {
            get { return keyWord; }
            set
            {
                keyWord = value;
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

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                RaisePropertyChanged();
            }
        }

        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                RaisePropertyChanged();
            }
        }

        private Location topleft;

        public Location NW
        {
            get { return topleft; }
            set
            {
                topleft = value;
                RaisePropertyChanged();
            }
        }

        private Location bottomright;

        public Location SE
        {
            get { return bottomright; }
            set
            {
                bottomright = value;
                RaisePropertyChanged();
            }
        }

        private bool isCategoryChecked;

        public bool IsCategoryChecked
        {
            get { return isCategoryChecked; }
            set
            {
                isCategoryChecked = value;
                RaisePropertyChanged();
            }
        }

        private bool isSamplingTimeChecked;

        public bool IsSamplingTimeChecked
        {
            get { return isSamplingTimeChecked; }
            set
            {
                isSamplingTimeChecked = value;
                RaisePropertyChanged();
            }
        }

        private bool isSamplingLocationChecked;

        public bool IsSamplingLocationChecked
        {
            get { return isSamplingLocationChecked; }
            set
            {
                isSamplingLocationChecked = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        public SearchModel()
        {
            this.KeyWord = "";
            this.CategoryIndex = 0;
            this.StartDate = DateTime.Now.Date;
            this.EndDate = DateTime.Now.Date;
            this.NW = new Location();
            this.SE = new Location();
            this.IsCategoryChecked = false;
            this.IsSamplingLocationChecked = false;
            this.IsSamplingTimeChecked = false;
        }
    }
}
