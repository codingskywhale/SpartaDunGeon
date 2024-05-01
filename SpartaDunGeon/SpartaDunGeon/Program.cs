using SpartaDunGeon;
using System.Xml.Linq;

public class GameManager
{
    public Player player;
    private List<Item> inventory;
    private List<Item> storeInventory;
    private List<Item> potionInventory;
    public GameManager()//생성자 없어서 추가했습니다.
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        inventory = new List<Item>();
        storeInventory = new List<Item>();
        storeInventory.Add(new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", ItemType.ARMOR, 0, 5, 0, 1000));
        storeInventory.Add(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", ItemType.ARMOR, 0, 9, 0, 2000));
        storeInventory.Add(new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", ItemType.ARMOR, 0, 15, 0, 3500));
        storeInventory.Add(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", ItemType.WEAPON, 2, 0, 0, 600));
        storeInventory.Add(new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", ItemType.WEAPON, 5, 0, 0, 1500));
        storeInventory.Add(new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", ItemType.WEAPON, 7, 0, 0, 3000));
        potionInventory = new List<Item>();
        potionInventory.Add(new Item("포션", "포션을 사용하면 체력을 30 회복 할 수 있습니다.", ItemType.POTION, 0, 0, 30, 300));
        potionInventory.Add(new Item("포션", "포션을 사용하면 체력을 30 회복 할 수 있습니다.", ItemType.POTION, 0, 0, 30, 300));
        potionInventory.Add(new Item("포션", "포션을 사용하면 체력을 30 회복 할 수 있습니다.", ItemType.POTION, 0, 0, 30, 300));
    }

    //게임 시작

    public void StartGame()
    {
        NameChoise();
    }

    //이름 입력
    private void NameChoise()
    {
        Console.Clear();

        Console.Write("이름을 입력해 주세요: ");
        string name = Console.ReadLine();

        Console.WriteLine($"입력하신 이름은 '{name}' 입니다.");
        Console.WriteLine("1. 예");
        Console.WriteLine("2. 아니오\n");

        int Choise = ConsoleUtility.ChoiceMenu(1, 2);

        switch (Choise)
        {
            case 1:
                JobChoise(name);
                break;
            case 2:
                NameChoise();
                break;
        }
    }

    //직업 선택
    private void JobChoise(string name)
    {
        Console.Clear ();

        Console.WriteLine("직업을 선택해 주세요.");
        Console.WriteLine("1. 전사");
        Console.WriteLine("2. 마법사");
        Console.Write("\n>>");

        while (true)
        {
            string jobChoise = Console.ReadLine();
            if (jobChoise == "1")
            {
                Console.WriteLine("전사를 선택하셨습니다.");
                player = new Player(name, "전사", 1, 10, 7, 70, 70, 700);
                Thread.Sleep(1000);
                MainMenu();
                break;
            }
            else if (jobChoise == "2")
            {
                Console.WriteLine("마법사를 선택하셨습니다.");
                player = new Player(name, "마법사", 1, 5, 5, 50, 50, 500);
                Thread.Sleep(1000);
                MainMenu();
                break;
            }
            else
            {
                Console.Write("선택지에서 골라주세요.\n>>");
            }
        }
    }

    //메인 메뉴
    private void MainMenu()
    {
        Console.Clear();
        //인트로
        Console.WriteLine("##############################");
        Console.WriteLine("스파르타 게임을 시작한다");
        Console.WriteLine("원하는 행동을 고르세요.");
        Console.WriteLine("##############################\n");

        //선택지
        Console.WriteLine("1. 상태창");
        Console.WriteLine("2. 장비창");
        Console.WriteLine("3. 상점");
        Console.WriteLine("4. 전투 시작");
        Console.WriteLine("5. 회복 아이템");

        //선택지 검증
        int Choise = ConsoleUtility.ChoiceMenu(1, 5);

        //메뉴 중에서 선택
        switch (Choise)
        {
            case 1:
                StateMenu();
                break;
            case 2:
                InventoryMenu();
                break;
            case 3:
                StoreMenu();
                break;
            case 4:
                Console.WriteLine("전투 시작");
                break;
            case 5:
                PotionMenu();
                break;
        }
    }

    //상태창
    private void StateMenu()
    {
        Console.Clear();

        ConsoleUtility.PrintColoredText(Color.Yellow,"# 상태 보기 #\n");
        Console.WriteLine("캐릭터의 정보가 표기됩니다.\n");

        Console.WriteLine($"{player.Name} ({player.Job})");
        Console.WriteLine($"공격력 : {player.Atk}");
        Console.WriteLine($"방어력 : {player.Def}");
        Console.WriteLine($"체  력 : {player.Hp}/{player.MaxHp}");
        Console.WriteLine($"Gold : {player.Gold}");

        Console.WriteLine("\n0. 나가기\n");

        int Choise = ConsoleUtility.ChoiceMenu(0, 0);

        switch (Choise)
        {
            case 0:
                MainMenu();
                break;
        }
    }

    //인벤토리
    private void InventoryMenu()
    {
        Console.Clear();
        Console.WriteLine("인벤토리");
        Console.WriteLine("");
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < inventory.Count; i++)
        {
            inventory[i].InventoryItemList();
        }
        Console.WriteLine("");
        Console.WriteLine("1. 장착관리");
        ConsoleUtility.PrintColoredText(Color.Red, "0. 나가기\n");
        Console.WriteLine("");
        switch (ConsoleUtility.ChoiceMenu(0, 1))
        {
            case 0:
                MainMenu();
                break;
            case 1:
                EquipMenu();
                break;
        }
    }

    //장비 장착
    private void EquipMenu()
    {
        Console.Clear();

        Console.WriteLine("인벤토리 - 장착관리");
        Console.WriteLine("");
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < inventory.Count; i++)
        {
            inventory[i].InventoryItemList(true, i + 1);
        }
        Console.WriteLine("");
        ConsoleUtility.PrintColoredText(Color.Red, "0. 나가기\n");

        int keyInput = ConsoleUtility.ChoiceMenu(0, inventory.Count);

        switch (keyInput)
        {
            case 0:
                InventoryMenu();
                break;
            default:
                if (inventory[keyInput - 1].IsEquipped == false)
                {
                    foreach (Item item in inventory)
                    {
                        if (item.IsEquipped == true && item.Type == inventory[keyInput - 1].Type)
                        {
                            item.toggleEquipStatus();
                            break;
                        }
                    }
                    inventory[keyInput - 1].toggleEquipStatus();
                }
                else
                {
                    inventory[keyInput - 1].toggleEquipStatus();
                }
                EquipMenu();
                break;
        }
    }

    //상점
    private void StoreMenu()
    {
        Console.Clear();

        Console.WriteLine("상점");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine("");
        Console.WriteLine("[보유골드]");
        Console.WriteLine($"{player.Gold} G");
        Console.WriteLine("");
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < storeInventory.Count; i++)
        {
            storeInventory[i].StoreItemList();
        }
        Console.WriteLine("");
        Console.WriteLine("1. 아이템 구매");
        Console.WriteLine("2. 아이템 판매");
        ConsoleUtility.PrintColoredText(Color.Red, "0. 나가기\n");
        Console.WriteLine("");
        switch (ConsoleUtility.ChoiceMenu(0, 2))
        {
            case 0:
                MainMenu();
                break;
            case 1:
                BuyMenu();
                break;
            case 2:
                SellMenu();
                break;
        }
    }

    //상점 - 구매
    private void BuyMenu()
    {
        Console.Clear();

        Console.WriteLine("상점 - 구매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine("");
        Console.WriteLine("[보유골드]");
        Console.WriteLine($"{player.Gold} G");
        Console.WriteLine("");
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < storeInventory.Count; i++)
        {
            storeInventory[i].StoreItemList(true, i + 1);
        }
        Console.WriteLine("");
        ConsoleUtility.PrintColoredText(Color.Red, "0. 나가기\n");
        Console.WriteLine("");

        int keyInput = ConsoleUtility.ChoiceMenu(0, storeInventory.Count);

        switch (keyInput)
        {
            case 0:
                StoreMenu();
                break;
            default:
                if (storeInventory[keyInput - 1].IsPurchased)
                {
                    ConsoleUtility.PrintColoredText(Color.Red, "이미 구매한 아이템입니다.");
                    Thread.Sleep(500);
                    BuyMenu();
                }
                else if (player.Gold >= storeInventory[keyInput - 1].Price)
                {
                    player.Gold -= storeInventory[keyInput - 1].Price;
                    storeInventory[keyInput - 1].Buy();
                    inventory.Add(storeInventory[keyInput - 1]);
                    BuyMenu();
                }
                else
                {
                    ConsoleUtility.PrintColoredText(Color.Red, "Gold가 부족합니다.");
                    Thread.Sleep(500);
                    BuyMenu();
                }
                break;
        }
    }

    //상점 - 판매
    private void SellMenu()
    {
        Console.Clear();

        Console.WriteLine("상점 - 판매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine("");
        Console.WriteLine("[보유골드]");
        Console.WriteLine($"{player.Gold} G");
        Console.WriteLine("");
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < inventory.Count; i++)
        {
            inventory[i].StoreItemSellList(true, i + 1);
        }
        Console.WriteLine("");
        ConsoleUtility.PrintColoredText(Color.Red, "0. 나가기\n");

        int keyInput = ConsoleUtility.ChoiceMenu(0, inventory.Count);

        switch (keyInput)
        {
            case 0:
                StoreMenu();
                break;
            default:
                inventory[keyInput - 1].Sell();
                player.Gold += (int)Math.Round(inventory[keyInput - 1].Price * 0.85);
                Item Sell = inventory[keyInput - 1];
                inventory.Remove(Sell);
                SellMenu();
                break;
        }
    }

    //회복 아이템
    private void PotionMenu()
    {
        Console.Clear();
        Console.Write("포션을 사용하면 체력을 30 회복 할 수 있습니다.");
        Console.WriteLine($" (남은 포션 : {potionInventory.Count} )\n");
        Console.WriteLine("[현재 체력]");
        Console.WriteLine($"{player.Hp}/{player.MaxHp}\n");
        Console.WriteLine("1. 사용하기");
        ConsoleUtility.PrintColoredText(Color.Red, "0. 나가기\n");
        int choice = ConsoleUtility.ChoiceMenu(0, 1);
        switch (choice)
        {
            case 0:
                MainMenu();
                break;
            case 1:
                if (potionInventory.Count == 0)
                {
                    ConsoleUtility.PrintColoredText(Color.Red, "포션이 부족합니다.");
                    Thread.Sleep(500);
                    PotionMenu();
                    break;
                }
                Item Use = potionInventory[choice - 1];
                potionInventory.Remove(Use);
                player.Hp += 30;
                if (player.Hp >= player.MaxHp)
                {
                    player.Hp = player.MaxHp;
                }
                ConsoleUtility.PrintColoredText(Color.Green, "회복을 완료했습니다.");
                Thread.Sleep(500);
                PotionMenu();
                break;
        }
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        GameManager gameManager = new GameManager();
        gameManager.StartGame();
    }
}