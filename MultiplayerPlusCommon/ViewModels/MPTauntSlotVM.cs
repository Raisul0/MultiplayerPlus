using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace MultiplayerPlusCommon.ViewModels
{
    public class MPTauntSlotVM : ViewModel
    {
        private string _tauntId;
        private string _tauntAction;
        private string _tauntName;
        private bool _isSelected;
        private readonly Action<MPTauntSlotVM> _onFocus;

        [DataSourceProperty]
        public string TauntAction
        {
            get
            {
                return _tauntAction;
            }
            set
            {
                if (value != _tauntAction)
                {
                    _tauntAction = value;
                    OnPropertyChangedWithValue(value, "TauntAction");
                }
            }
        }

        [DataSourceProperty]
        public string TauntName
        {
            get
            {
                return _tauntName;
            }
            set
            {
                if (value != _tauntName)
                {
                    _tauntName = value;
                    OnPropertyChangedWithValue(value, "TauntName");
                }
            }
        }

        [DataSourceProperty]
        public string TanutId
        {
            get
            {
                return _tauntId;
            }
            set
            {
                if (value != _tauntId)
                {
                    _tauntId = value;
                    OnPropertyChangedWithValue(value, "TanutId");
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



        public MPTauntSlotVM(string tauntId, string tauntName, string tauntAction, Action<MPTauntSlotVM> onFocus)
        {
            TanutId = tauntId;
            TauntName = tauntName;
            TauntAction = tauntAction;
            _onFocus = onFocus;
        }
    }
}
