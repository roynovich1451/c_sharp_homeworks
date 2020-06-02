using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MovieProjectClasses
{
    public class ObservableDictionary<Key, Value> : INotifyPropertyChanged
    {
        private Dictionary<Key, Value> dict = new Dictionary<Key, Value>();

        public Dictionary<Key, Value> Dict
        {
            get
            {
                return dict;
            }
            set
            {
                dict = value;
                if (PropertyChanged != null)
                {
                    OnPropertyChanged("Dict");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public void Add(Key key, Value val)
        {
            Dict.Add(key, val);
            var temp = Dict;
            Dict = null;
            Dict = temp;
        }

        public void Remove(Key key)
        {
            Dict.Remove(key);
            var temp = Dict;
            Dict = null;
            Dict = temp;
        }

        public void OrderDict()
        {
            var temp = Dict.OrderBy(dic => dic.Key).ToDictionary(dic => dic.Key, dic => dic.Value);
            Dict = null;
            Dict = temp;
        }
    }
}
