using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XwaSalvageYardScoreEditor
{
    public sealed class SalvageYardChallengeScore : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int challengeIndex;

        public int ChallengeIndex
        {
            get
            {
                return this.challengeIndex;
            }

            set
            {
                if (this.challengeIndex == value)
                {
                    return;
                }

                this.challengeIndex = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChallengeIndex)));
            }
        }

        public ObservableCollection<SalvageYardScore> Scores { get; } = new ObservableCollection<SalvageYardScore>();
    }
}
