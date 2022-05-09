using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XwaSalvageYardScoreEditor
{
    public static class SalvageYardScoresTable
    {
        private const int ChallengeCount = 8;

        private const int ScoreCount = 10;

        private const int MaxNameLength = 14;

        public static ObservableCollection<SalvageYardCraftScore> Read(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(null, path);
            }

            var table = new ObservableCollection<SalvageYardCraftScore>();

            using (BinaryReader file = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read), Encoding.ASCII))
            {
                if (file.ReadInt32() != 0x13DE3C1F)
                {
                    throw new InvalidDataException();
                }

                if (file.ReadInt32() != 1)
                {
                    throw new InvalidDataException();
                }

                int craftCount = file.ReadInt32();

                for (int craftIndex = 0; craftIndex < craftCount; craftIndex++)
                {
                    var craft = new SalvageYardCraftScore
                    {
                        ModelIndex = file.ReadInt32()
                    };

                    for (int challengeIndex = 0; challengeIndex < ChallengeCount; challengeIndex++)
                    {
                        var challenge = new SalvageYardChallengeScore
                        {
                            ChallengeIndex = challengeIndex
                        };

                        for (int scoreIndex = 0; scoreIndex < ScoreCount; scoreIndex++)
                        {
                            byte[] nameBytes = file.ReadBytes(MaxNameLength);
                            string name = Encoding.ASCII.GetString(nameBytes);
                            name = name.Substring(0, name.IndexOf('\0'));

                            int time = file.ReadInt32();

                            var score = new SalvageYardScore
                            {
                                ChallengeIndex = challengeIndex,
                                ScoreIndex = scoreIndex,
                                Name = name,
                                Time = time
                            };

                            challenge.Scores.Add(score);
                        }

                        craft.Scores.Add(challenge);
                    }

                    table.Add(craft);
                }
            }

            return table;
        }

        public static void Write(ObservableCollection<SalvageYardCraftScore> table, string path)
        {
            using (BinaryWriter file = new BinaryWriter(new FileStream(path, FileMode.Create, FileAccess.Write), Encoding.ASCII))
            {
                file.Write(0x13DE3C1F);
                file.Write(1);
                file.Write(table.Count);

                foreach (SalvageYardCraftScore craft in table)
                {
                    file.Write(craft.ModelIndex);

                    int challengeCount = Math.Min(craft.Scores.Count, ChallengeCount);

                    for (int challengeIndex = 0; challengeIndex < challengeCount; challengeIndex++)
                    {
                        SalvageYardChallengeScore challenge = craft.Scores[challengeIndex];

                        int scoreCount = Math.Min(challenge.Scores.Count, ScoreCount);

                        for (int scoreIndex = 0; scoreIndex < scoreCount; scoreIndex++)
                        {
                            SalvageYardScore score = challenge.Scores[scoreIndex];

                            var nameBytes = Encoding.ASCII.GetBytes(score.Name);
                            file.Write(nameBytes);

                            for (int i = nameBytes.Length; i < MaxNameLength; i++)
                            {
                                file.Write((byte)0);
                            }

                            file.Write(score.Time);
                        }

                        for (int scoreIndex = scoreCount; scoreIndex < ScoreCount; scoreIndex++)
                        {
                            file.Write(new byte[MaxNameLength]);
                            file.Write(0);
                        }
                    }

                    for (int challengeIndex = challengeCount; challengeIndex < ChallengeCount; challengeIndex++)
                    {
                        for (int scoreIndex = 0; scoreIndex < ScoreCount; scoreIndex++)
                        {
                            file.Write(new byte[MaxNameLength]);
                            file.Write(0);
                        }
                    }
                }
            }
        }

        public static void AddNew(ObservableCollection<SalvageYardCraftScore> table)
        {
            var craft = new SalvageYardCraftScore
            {
                ModelIndex = 0
            };

            for (int challengeIndex = 0; challengeIndex < ChallengeCount; challengeIndex++)
            {
                var challenge = new SalvageYardChallengeScore
                {
                    ChallengeIndex = challengeIndex
                };

                for (int scoreIndex = 0; scoreIndex < ScoreCount; scoreIndex++)
                {
                    var score = new SalvageYardScore
                    {
                        ChallengeIndex = challengeIndex,
                        ScoreIndex = scoreIndex,
                        Name = string.Empty,
                        Time = 0
                    };

                    challenge.Scores.Add(score);
                }

                craft.Scores.Add(challenge);
            }

            table.Add(craft);
        }
    }
}
