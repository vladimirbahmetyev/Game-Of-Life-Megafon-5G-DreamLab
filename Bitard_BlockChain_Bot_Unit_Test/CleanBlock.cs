using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitard_BlockChain_Bot_Unit_Test
{
    class CleanBlock
    {
        //Time for seeing
        private DateTime time;

        //Users List
        private Queue<User> UsersList;
        
        /// <summary>
        /// Конструктор cleanBlocka
        /// </summary>
        /// <param name="inputUsers">список юзеров для учета</param>
        public CleanBlock(Queue<User> inputUsers)
        {
            setNewDate();
            UsersList = inputUsers;
        }

        /// <summary>
        /// Устанавливает дату
        /// </summary>
        public void setNewDate()
        {
            time = DateTime.Now;
            while (time.Hour != 19)
            {
                time = time.AddHours(1);
            }
            while ((int)time.DayOfWeek != 5)
            {
                time = time.AddDays(1);
            }
            while (time.Minute != 59)
            {
                time = time.AddMinutes(1);
            }
            while (time.Second != 59)
            {
                time = time.AddSeconds(1);
            }
        }

        /// <summary>
        /// Checked time
        /// </summary>
        /// <returns>True/false</returns>
        public bool isTimeCome => DateTime.Now > time;

        /// <summary>
        /// Checked concreate date
        /// </summary>
        public DateTime getData => time;
    }
}
