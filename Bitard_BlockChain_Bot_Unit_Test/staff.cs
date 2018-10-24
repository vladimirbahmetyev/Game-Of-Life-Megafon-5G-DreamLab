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
    class PriorityItem
    {
        //название предмета
        private string itemName;

        //Приоритет предмета
        private int priority;

        //Конструктор предмета
        public PriorityItem(string _item, int _priority)
        {
            itemName = _item;
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
                case 3:
                    return " вопрос жизни и смерти";
                case 4:
                    return " мы умрем без этого";
                default:
                    return "";
            }
        }
    }

    //Класс список предметов
    class Staff
    {
        //Голова списка
        private List<PriorityItem> listOfItems;

        //Конструктор списка
        public Staff() => listOfItems = new List<PriorityItem>();


        /// <summary>
        /// Adding new Item
        /// </summary>
        /// <param name="newItem"></param>
        public void addItem(string newItem, int priority)
        {
            listOfItems.Add(new PriorityItem(newItem, priority));

            listOfItems.Sort((PriorityItem first,PriorityItem second) => (first.getPriority.CompareTo(second.getPriority)));
        }

        //Проверяет есть ли предмет в списке
        public bool isInStaff(string item)
        {
            foreach (PriorityItem templeItem in listOfItems)
            {
                if (templeItem.getItem == item)
                {
                    return true;
                }
            }
            return false;
        }

        //Удаляет элемент из списка
        public void deleteItem(string item)
        {
            PriorityItem deleteItem = null;
            foreach (PriorityItem templeItem in listOfItems)
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
        public void deleteItemAt(int number)
        {
            if ((number <= listOfItems.Capacity - 1) && (number >= 0) )
            {
                listOfItems.RemoveAt(number);
            }
            else
            {
                return;
            }
        }

        //Возвращает значение с головы списка
        public PriorityItem getTop => listOfItems[0];

        //Возвращает список в виде string
        public string getListOfItems()
        {
            string temple = "";
            foreach(PriorityItem item in listOfItems)
            {
                temple = temple + item.getItem + " c приоритетом " + item.getStringPriority() +  "\n";
            }
            return temple;
        }

        //GetSize of list
        public int getSize => listOfItems.Count;

        //построить вертикальные query кнопки
        private InlineKeyboardButton[][] createInlineKeyboard()
        {
            var keyBoardIncolumn = new InlineKeyboardButton[listOfItems.Count][];
            for (int i = 0; i < listOfItems.Count; i++)
            {
                var keyBoardButton = new InlineKeyboardButton[1];
                keyBoardButton[0] = new InlineKeyboardButton
                {
                    Text = listOfItems[i].getItem.ToString() + " c приоритетом " + listOfItems[i].getStringPriority(),
                    CallbackData = i.ToString(),
                };
                keyBoardIncolumn[i] = keyBoardButton;
                
            }
            return keyBoardIncolumn;
        }

        //Сконструировать композицию кнопок
        public InlineKeyboardMarkup getInlineKeyboard => new InlineKeyboardMarkup(createInlineKeyboard());
    }
}
