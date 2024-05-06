using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SpartaDunGeon
{
    public class QuestSaveData
    {
        public int SaveId;
        public int TmpKill {  get; set; }
        public bool IsCompleted { get; set; }
        public bool IsProceeding { get; set; }
        public bool CanCompleted { get; set; }
        public QuestSaveData(int saveId, int tmpKill = 0, bool isCompleted = false, bool isProceeding = false, bool canCompleted = false)
        {
            SaveId = saveId;
            TmpKill = tmpKill;
            IsCompleted = isCompleted;
            IsProceeding = isProceeding;
            CanCompleted = canCompleted;
        }
    }
}
