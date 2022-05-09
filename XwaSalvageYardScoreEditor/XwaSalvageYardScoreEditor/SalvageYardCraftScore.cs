using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XwaSalvageYardScoreEditor
{
    public sealed class SalvageYardCraftScore : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int modelIndex;

        public int ModelIndex
        {
            get
            {
                return this.modelIndex;
            }

            set
            {
                if (this.modelIndex == value)
                {
                    return;
                }

                this.modelIndex = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModelIndex)));
            }
        }

        public ObservableCollection<SalvageYardChallengeScore> Scores { get; } = new ObservableCollection<SalvageYardChallengeScore>();
    }
}
