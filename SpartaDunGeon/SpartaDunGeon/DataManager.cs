using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spartadungeon;

namespace SpartaDunGeon
{
    internal class DataManager
    {
        public static DataManager _player = new DataManager();
        public Player PlayerData { get; set; }
        public List<Item> InventoryData { get; set; }
        public List<Item> StoreData { get; set; }
        public List<Item> PotionData { get; set; }
        public StageData StageData { get; set; }
        public List<QuestSaveData> QuestSaveData { get; set; }
        public static bool Data(string SoL)
        {
            string path = Path.GetFullPath("./SaveData.json");
            if (SoL == "Save")
            {
                _player = new DataManager
                {
                    PlayerData = GameManager.player,
                    InventoryData = Inventory.inventory,
                    StoreData = Store.storeInventory,
                    PotionData = Inventory.potionInventory,
                    StageData = Dungeon.stage,
                    QuestSaveData = QuestManager.questSaveList
                };
                string saveData = JsonConvert.SerializeObject(_player, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(path, saveData);
                return true;
            }
            else if (SoL == "Load")
            {
                if (!File.Exists(path))
                {
                    ConsoleUtility.PrintColoredText(ConsoleColor.Red, "저장된 데이터가 없습니다.");
                    Thread.Sleep(500);
                    return false;
                }
                string LoadData = File.ReadAllText(Path.GetFullPath("./SaveData.json"));
                _player = JsonConvert.DeserializeObject<DataManager>(LoadData);
                GameManager.player = _player.PlayerData;
                Inventory.inventory = _player.InventoryData;
                Store.storeInventory = _player.StoreData;
                Inventory.potionInventory = _player.PotionData;
                Dungeon.stage = _player.StageData;
                QuestManager.questSaveList = _player.QuestSaveData;
                return true;
            }
            return false;
        }
    }
}
