using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

/// <summary>
/// 0 необходимо в скором времени
/// 3 это нужно срочно 
/// 2 надо сходить в магазин за
/// 4 пизда рулю, нужно сейчас 
/// 5 (вопрос жизни и смерти)
/// </summary>

namespace Bitard_BlockChain_Bot_Unit_Test
{
    //Класс предмет с приоритетом
    class priorityItem
    {
        //название предмета
        private string itemName;

        //Приоритет предмета
        private int priority;

        //Конструктор предмета
        public priorityItem(string _item, int _priority)
        {
            itemName = _item;
            priority = _priority;
        }

        public priorityItem(string _item)
        {
            itemName = _item;
            priority = -1;
        }

        public  void setPiority(int _priority)
        {
            priority = _priority;
        }

        //Получить имя данного предмета
        public string getItem => itemName;

        //Получить приоритет данного предмета в целочисленном эквиваленте
        public int getPriority => priority;

        //Получить приоритет данного предмета в строчном эквиваленте
        public string getStringPriority ()
        {
            switch(priority)
            {
                case 0:
                    return " когда-нибудь можно купить";
                case 1:
                    return " желательно сходить в скором времени";
                case 2:
                    return " нужно срочно";
                case 3:C:\Users\Владимир\Desktop\BitardsBotGItKracken\BitardsBotTgm\Bitard_BlockChain_Bot_Unit_Test\staff.cs
                    return " вопрос жизни и смерти";
                case 4:
                    return " мы умрем без этого";
                default:
                    return "";
            }
        }
    }


    class staff
    {
        //Голова списка
        private List<priorityItem> listOfItems;

        //Конструктор списка
        public staff() => listOfItems = new List<priorityItem>();


        /// <summary>
        /// Adding new Item
        /// </summary>
        /// <param name="newItem"></param>
        public void addItem(string newItem,int priority) => listOfItems.Add(new priorityItem(newItem, priority));

        /// <summary>
        /// Adding new item without priority
        /// </summary>
        /// <param name="newItem"></param>
        public void addItem(string newItem) => listOfItems.Add(new priorityItem(newItem));


        //Установить приоритет элементу с приоритетом -1
        public void setPriotiyFirstItem(int _priority)
        {
            foreach( priorityItem temple in listOfItems)
            {
                if(temple.getPriority == -1)
                {
                    temple.setPiority(_priority);
                }
            }
        }

        //Удаляет элемент из списка
        public void deleteItem(string item)
        {
            priorityItem deleteItem = null;
            foreach (priorityItem templeItem in listOfItems)
            {
                if (templeItem.getItem == item)
                {
                    deleteItem = templeItem;
                }
            }
            if (null != deleteItem)
            {
                listOfItems.Remove(deleteItem);
            }
            else
            {
                return;
            }
        }

        //Удаления элемента на определенной позиции
        public void deleteItemAt(int number) => listOfItems.RemoveAt(number);


        public priorityItem getTop => listOfItems[0];

        //Возвращает список в виде string
        public string getListOfItems()
        {
            string temple = "";
            foreach(priorityItem item in listOfItems)
            {
                temple = temple + item + "c приоритетом " + item.getStringPriority() +  "\n";
            }
            return temple;
        }

        //GetSize of list
        public int getSize => listOfItems.Count;

        //Придумать как динамически добавлять кнопки
        public InlineKeyboardMarkup getListInlineKeyBoard()
        {
            return null;
        }

        private InlineKeyboardButton[][] GetInlineKeyboard()
        {
            var keyBoardInline = new InlineKeyboardButton[1][];
            var keyBoardButtons = new InlineKeyboardButton[listOfItems.Count];
            for(int i = 0; i < listOfItems.Count;i++)
            {
                keyBoardButtons[i] = new InlineKeyboardButton
                {
                    Text = ??
                }
            }
            return null;
        }
    }
}