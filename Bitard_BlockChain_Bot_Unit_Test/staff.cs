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

    class priorityItem
    {
        private string itemName;
        private int priority;
        public priorityItem(string item, int _priority)
        {
            itemName = item;
            priority = _priority;
        }
        public string getItem => itemName;
        public int
    }


    class staff
    {
        private List<string> listOfItems;
        public staff() => listOfItems = new List<string>();


        /// <summary>
        /// Adding new Item
        /// </summary>
        /// <param name="newItem"></param>
        public void addItem(string newItem) => listOfItems.Add(newItem);

        public void deleteItem(string item) => listOfItems.Remove(item);

        public string getListOfItems()
        {
            string temple = "";
            foreach(string item in listOfItems)
            {
                temple = temple + item + "\n";
            }
            return temple;
        }
        //GetSize of list
        public int getSize => listOfItems.Count;
    }
}