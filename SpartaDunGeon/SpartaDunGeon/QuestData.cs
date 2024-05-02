using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDunGeon
{
    public enum QuestType
    {
        // 전투 퀘스트 = 몬스터 처치
        // 성장 퀘스트 = 레벨 업
        // 생활 퀘스트 = 아이템 수집        
        전투 = 1,
        성장,
        생활,
        
    }
    public class QuestData
    {        
        public string QuestName { get; }
        public string Description { get; }
        public QuestType Type { get; }
        public int QuestId { get; }
        public bool IsCompleted { get; private set; }

        public QuestData(int id, string name, string description)
        {
            QuestId = id;
            QuestName = name;
            Description = description;
        }
    }
}
