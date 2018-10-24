using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitard_BlockChain_Bot_Unit_Test
{
    public class User
    {
        //User's Name
        private string name;
        //User's point
        private int bitardsPoint;
        //User's Rank
        private string rank;
        //Relations List
        private List<int> relations;

        //Id пользователя
        private long ChatId;


        //Голосовал ли пользователь?
        private bool isVoted;

        /// <summary>
        /// Constractor
        /// </summary>
        /// <param name="inputName">User's Name</param>
        public User(string inputName, long chatId)
        {
            ChatId = chatId;
            name = inputName;
            bitardsPoint = 0;
            rank = "Middle";
            isVoted = false;
        }

        public User()
        {
        }

        /// <summary>
        /// Get name
        /// </summary>
        public string getName => name;

        /// <summary>
        /// getRank
        /// </summary>
        public string getRank => rank;

        /// <summary>
        /// Up User's rep and conf it
        /// </summary>
        public void increaseRep()
        {
            bitardsPoint++;
            rankConfig();
        }


        public void hasVoted()
        {
            isVoted = true;
        }

        public void hasNotVoted()
        {
            isVoted = false;
        }

        public bool isUserVoited => isVoted;

        /// <summary>
        /// Down User's rep and conf it
        /// </summary>
        public void degreaseRep()
        {
            bitardsPoint--;
            rankConfig();
        }

        //Set Id
        public void setUserId(int id) => ChatId = id;

        //get Id
        public long getId => ChatId;

        /// <summary>
        /// Correct User's rank
        /// </summary>
        private void rankConfig()
        {
            switch (bitardsPoint)
            {
                case 6:
                    rank = "Bitard' God";
                    break;
                case 4:
                    rank = "GodLike";
                    break;
                case 2:
                    rank = "Senior";
                    break;
                case 0:
                    rank = "Middle";
                    break;
                case -2:
                    rank = "Junior";
                    break;
                case -4:
                    rank = "Bitard";
                    break;
                default:
                    break;
            }
        }   
    }
}