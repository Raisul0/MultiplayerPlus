using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace MultiplayerPlusCommon.ViewModels
{
    public class MPShoutSlotVM : ViewModel
    {
        private string _shoutId;
        private string _shoutName;
        private string _shoutValue;
        private bool _isSelected;
        private readonly Action<MPShoutSlotVM> _onFocus;


        [DataSourceProperty]
        public string ShoutName
        {
            get
            {
                return _shoutName;
            }
            set
            {
                if (value != _shoutName)
                {
                    _shoutName = value;
                    OnPropertyChangedWithValue(value, "ShoutName");
                }
            }
        }

        [DataSourceProperty]
        public string ShoutId
        {
            get
            {
                return _shoutId;
            }
            set
            {
                if (value != _shoutId)
                {
                    _shoutId = value;
                    OnPropertyChangedWithValue(value, "ShoutId");
                }
            }
        }

        [DataSourceProperty]
        public string ShoutValue
        {
            get
            {
                return _shoutValue;
            }
            set
            {
                if (value != _shoutValue)
                {
                    _shoutValue = value;
                    OnPropertyChangedWithValue(value, "ShoutValue");
                }
            }
        }

        [DataSourceProperty]
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    OnPropertyChangedWithValue(value, "IsSelected");
                    if (value)
                    {
                        _onFocus(this);
                    }
                }
            }
        }



        public MPShoutSlotVM(string shoutId, string shoutName,string shoutValue,  Action<MPShoutSlotVM> onFocus)
        {
            ShoutId = shoutId;
            ShoutName = shoutName;
            ShoutValue = shoutValue;
            _onFocus = onFocus;
        }
    }
}
