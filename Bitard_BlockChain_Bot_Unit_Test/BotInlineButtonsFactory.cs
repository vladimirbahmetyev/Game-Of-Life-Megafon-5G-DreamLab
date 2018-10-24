using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bitard_BlockChain_Bot_Unit_Test
{
    class BotInlineButtonsFactory
    {
        public InlineKeyboardMarkup getComandLine => new InlineKeyboardMarkup(
                new InlineKeyboardButton[][]
                {
                    new[]
                    {
                        //Positive Reaction
                        InlineKeyboardButton.WithCallbackData("Покажи Егора","showEgor"),
                    },
                    new[]
                    {
                        //Positive Reaction
                        InlineKeyboardButton.WithCallbackData("Покажи Севу","showSeva"),
                    },
                    new[]
                    {
                        //Positive Reaction
                        InlineKeyboardButton.WithCallbackData("Покажи Богдана","showBogdan"),
                    },
                    new[]
                    {
                        //Positive Reaction
                        InlineKeyboardButton.WithCallbackData("Получить ID","getId"),
                    },
                    new[]
                    {
                        //Positive Reaction
                        InlineKeyboardButton.WithCallbackData("Получить список товаров","getItemList"),
                    },
                    new[]
                    {
                        //Positive Reaction
                        InlineKeyboardButton.WithCallbackData("Добавить товар в список","addNewItem"),
                    },
                    new[]
                    {
                        //Positive Reaction
                        InlineKeyboardButton.WithCallbackData("Удалить товар из списка","deleteItem"),
                    },
                });

        public InlineKeyboardMarkup getAskingCleanerLine => new InlineKeyboardMarkup(
        new InlineKeyboardButton[][]
        {
             // First row
            new []
            {
                //First column
                InlineKeyboardButton.WithCallbackData("Да","callback1"),
                // Second column
                InlineKeyboardButton.WithCallbackData("Нет","callback2"),
            },
        }
    );

        public InlineKeyboardMarkup getAskingUsersLine => new InlineKeyboardMarkup(
        new InlineKeyboardButton[][]
        {
            new[]
            {
                //Positive Reaction
                InlineKeyboardButton.WithCallbackData("Да","PositiveCallback"),
                //Negative Reaction
                InlineKeyboardButton.WithCallbackData("Нет", "NegativeCallback"),
            },
        });

        public InlineKeyboardMarkup getPriorityLine => new InlineKeyboardMarkup(
            new InlineKeyboardButton[][]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("когда-нибудь можно купить","-5")
                },
                 new[]
                {
                    InlineKeyboardButton.WithCallbackData("желательно сходить в скором времени","-4")
                },
                  new[]
                {
                    InlineKeyboardButton.WithCallbackData("нужно срочно","-3")
                },
                   new[]
                {
                    InlineKeyboardButton.WithCallbackData("вопрос жизни и смерти","-2")
                },
                    new[]
                {
                    InlineKeyboardButton.WithCallbackData("мы умрем без этого","-1")
                },
            });
    }
}
