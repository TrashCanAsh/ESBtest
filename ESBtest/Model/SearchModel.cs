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

        private int category;

        public int Category
        {
            get { return category; }
            set
            {
                category = value;
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
        #endregion

        public SearchModel()
        {
            this.KeyWord = "";
            this.Category = 0;
            this.StartDate = DateTime.Now.Date;
            this.EndDate = DateTime.Now.Date;
            this.NW = new Location();
            this.SE = new Location();
        }
    }
}
