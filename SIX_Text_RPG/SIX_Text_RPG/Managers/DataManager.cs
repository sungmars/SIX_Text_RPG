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

            // 플레이어 정보 저장하기
            stringBuilder.Append($"{JsonConvert.SerializeObject(player.Type)}\n");
            stringBuilder.Append($"{JsonConvert.SerializeObject(player.Stats)}\n");

            // 아이템 정보 저장하기
            foreach (var item in GameManager.Instance.Inventory)
            {
                stringBuilder.Append(item is IGraphicable graphicable ? $"{JsonConvert.SerializeObject(graphicable.Graphic)}\n" : "\n");
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

            // 플레이어 정보 불러오기
            string[] jsonData = Encoding.UTF8.GetString(data).Split('\n');
            PlayerType playerType = JsonConvert.DeserializeObject<PlayerType>(jsonData[0]);
            Stats stats = JsonConvert.DeserializeObject<Stats>(jsonData[1]);
            Player player = GameManager.Instance.Player = new(playerType) { Stats = stats };
            Scene_CreatePlayer.PlayerName = player.Stats.Name;

            // 아이템 정보 불러오기
            for (int i = 2; i < jsonData.Length - 1; i++)
            {
                if ((i + 1) % 3 == 0)
                {
                    continue;
                }

                IGraphicable? graphicable = JsonConvert.DeserializeObject<IGraphicable>(jsonData[i]);
                ItemType itemType = JsonConvert.DeserializeObject<ItemType>(jsonData[i + 1]);
                ItemInfo info = JsonConvert.DeserializeObject<ItemInfo>(jsonData[i + 2]);

                //Item item;
                //switch (itemType)
                //{
                //    case ItemType.Armor:
                //        item = new Armor(info);
                //        break;
                //    case ItemType.Accessory:
                //        item = new Accessory(info);
                //        break;
                //    case ItemType.Potion:
                //        item = new Potion(info);
                //        break;
                //    case ItemType.Weapon:
                //        item = new Weapon(info);
                //        break;
                //}
            }

            return true;
        }
    }
}