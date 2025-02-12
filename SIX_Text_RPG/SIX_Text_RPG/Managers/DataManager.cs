using System.Text;
using Newtonsoft.Json;
using SIX_Text_RPG.Scenes;

namespace SIX_Text_RPG
{
    internal class DataManager
    {
        private readonly string FILE_NAME = "dat";

        public static readonly DataManager Instance = new();

        public void SaveData()
        {
            Player? player = GameManager.Instance.Player;
            if (player == null)
            {
                return;
            }

            FileStream fileStream = new($"{FILE_NAME}.json", FileMode.Create);
            StringBuilder stringBuilder = new();

            // 스테이지 정보 저장하기
            stringBuilder.Append($"{JsonConvert.SerializeObject(GameManager.Instance.TargetStage)}\n");

            // 대화 정보 저장하기
            stringBuilder.Append($"{JsonConvert.SerializeObject(Scene_Store.ScriptToggles)}\n");

            // 플레이어 정보 저장하기
            stringBuilder.Append($"{JsonConvert.SerializeObject(player.Type)}\n");
            stringBuilder.Append($"{JsonConvert.SerializeObject(player.Stats - player.EquipStats)}\n");

            // 아이템 정보 저장하기
            foreach (var item in GameManager.Instance.Inventory)
            {
                stringBuilder.Append($"{JsonConvert.SerializeObject(item.Type)}\n");
                stringBuilder.Append($"{JsonConvert.SerializeObject(item.Iteminfo)}\n");
            }

            // data.json 파일에 데이터 쓰기
            string jsonData = stringBuilder.ToString();
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }

        public bool LoadData()
        {
            // 저장된 데이터가 있는지 확인
            FileStream fileStream;
            try
            {
                fileStream = new($"{FILE_NAME}.json", FileMode.Open);
            }
            catch
            {
                Utils.WriteColor("\n\n\n >> ", ConsoleColor.DarkYellow);
                Console.WriteLine(Define.ERROR_MESSAGE_DATA);

                return false;
            }

            // data.json 파일의 데이터 읽어오기
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();

            // 스테이지 정보 불러오기
            string[] jsonData = Encoding.UTF8.GetString(data).Split('\n');
            int currentStage = JsonConvert.DeserializeObject<int>(jsonData[0]);
            GameManager.Instance.CurrentStage = currentStage;
            GameManager.Instance.TargetStage = currentStage;

            // 대화 정보 불러오기
            bool[]? scriptToggles = JsonConvert.DeserializeObject<bool[]>(jsonData[1]);
            if (scriptToggles != null)
            {
                Scene_Store.ScriptToggles = scriptToggles;
            }

            // 플레이어 정보 불러오기
            PlayerType playerType = JsonConvert.DeserializeObject<PlayerType>(jsonData[2]);
            Stats stats = JsonConvert.DeserializeObject<Stats>(jsonData[3]);
            Player player = GameManager.Instance.Player = new(playerType) { Stats = stats };
            Scene_CreatePlayer.PlayerName = player.Stats.Name;

            // 아이템 정보 불러오기
            for (int i = 4; i < jsonData.Length - 1; i++)
            {
                if (i % 2 != 0)
                {
                    continue;
                }

                ItemType itemType = JsonConvert.DeserializeObject<ItemType>(jsonData[i]);
                ItemInfo info = JsonConvert.DeserializeObject<ItemInfo>(jsonData[i + 1]);

                Item? item = null;
                switch (itemType)
                {
                    case ItemType.Armor:
                        item = new Armor(info);
                        break;
                    case ItemType.Accessory:
                        item = new Accessory(info);
                        break;
                    case ItemType.Potion:
                        item = new Potion(info);
                        break;
                    case ItemType.Weapon:
                        item = new Weapon(info);
                        break;
                }

                if (item == null)
                {
                    return false;
                }

                if (item.Iteminfo.IsEquip)
                {
                    player.Equip(item as IEquipable);
                }

                GameManager.Instance.Inventory.Add(item);
                ItemManager.Instance.SetBool_StoreItem(itemType, item);
            }

            return true;
        }
    }
}