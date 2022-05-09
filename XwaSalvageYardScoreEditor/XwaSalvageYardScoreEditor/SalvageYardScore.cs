using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XwaSalvageYardScoreEditor
{
    public sealed class SalvageYardScore : INotifyPropertyChanged
    {
        private const int MaxNameLength = 14;

        public event PropertyChangedEventHandler PropertyChanged;

        private int challengeIndex;

        private int scoreIndex;

        private string name;

        private int time;

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

        public int ScoreIndex
        {
            get
            {
                return this.scoreIndex;
            }

            set
            {
                if (this.scoreIndex == value)
                {
                    return;
                }

                this.scoreIndex = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScoreIndex)));
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                byte[] b = Encoding.ASCII.GetBytes(value);
                string s = Encoding.ASCII.GetString(b, 0, Math.Min(b.Length, MaxNameLength - 1));

                if (this.name == s)
                {
                    return;
                }

                this.name = s;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public int Time
        {
            get
            {
                return this.time;
            }

            set
            {
                if (this.time == value)
                {
                    return;
                }

                this.time = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Time)));
            }
        }
    }
}
