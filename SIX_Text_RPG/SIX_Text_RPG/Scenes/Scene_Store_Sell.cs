using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Store_Sell : Scene_Base
    {
        List<Item> inven = GameManager.Instance.Inventory;

        public override void Awake()
        {
            
        }
        protected override void Display()
        {
            throw new NotImplementedException();
        }

        public override int Update()
        {
            return base.Update();
        }
    }
}
