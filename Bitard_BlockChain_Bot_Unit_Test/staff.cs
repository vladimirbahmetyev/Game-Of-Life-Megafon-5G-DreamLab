using System.Collections.Generic;

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
        public void addItem(string newItem,, int priority) => listOfItems.Add(new priorityItem( newItem, priority));


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
    }
}