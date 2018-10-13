using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonStats_WPF {
    public class DataViewModel : INotifyPropertyChanged {
        private Data d = null;

        public DataViewModel() {
            d = new Data();
        }

        public DataView PokedexDataView {
            get { return d.PokedexDataView; }
        }

        public DataSet PokedexDataSet {
            get { return d.PokedexDataSet; }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null) {
            var handler = PropertyChanged;
            if(handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
