using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bitard_BlockChain_Bot_Unit_Test
{
    class BotCallbacks
    {
        private string itemForAdding = "";

        public void setItem(string newItem)
        {
            itemForAdding = newItem;
        }
        public void addCallbacks(ITelegramBotClient bot, Staff botsItemList, InlineKeyboardMarkup commandkeyBoard, Queue<User> usersList)
        {

            //CallBackFromAskingCommandLine
            bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>

            {
                switch (ev.CallbackQuery.Data)
                {
                    case "showEgor":
                        await bot.SendPhotoAsync(ev.CallbackQuery.Message.Chat, "https://pp.userapi.com/c846122/v846122367/8b1da/oM0jrFvVu8Y.jpg");
                        await bot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                        break;
                    case "showSeva":
                        await bot.SendPhotoAsync(ev.CallbackQuery.Message.Chat, "https://pp.userapi.com/c824202/v824202167/183d3e/S2DcqEvIIKg.jpg");
                        await bot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                        break;
                    case "showBogdan":
                        await bot.SendPhotoAsync(ev.CallbackQuery.Message.Chat, "https://pp.userapi.com/c830708/v830708039/1890f7/ehrKDFthYI4.jpg");
                        await bot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                        break;
                    case "getId":
                        await bot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ev.CallbackQuery.Message.Chat.Id.ToString());
                        await bot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                        break;
                    case "getItemList":
                        if (botsItemList.getSize == 0)
                        {
                            await bot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, "Список пуст");
                            await bot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, botsItemList.getListOfItems());
                        }
                        break;
                    case "addNewItem":
                        await bot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, "Что нужно купить?)");
                        Program.state = 1;
                        break;
                    case "deleteItem":
                        if (!(botsItemList.getSize == 0))
                        {
                            var templeKeyBoard = botsItemList.getInlineKeyboard;

                            await bot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, "Какой предмет вы хотите удалить?", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, templeKeyBoard);
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, "Список пуст");
                            await bot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                        }
                        break;
                    default:
                        break;
                }
            };

            //CallBackFromDeleteItem
            bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>

            {
                //Пробное преобразование даты для удаления
                int templeInt = 0;
                if (Int32.TryParse(ev.CallbackQuery.Data, out templeInt))
                {
                    if (templeInt >= 0)
                    {
                        botsItemList.deleteItemAt(templeInt);
                        await bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Предмет удален");
                        await bot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                    }
                }
            };


            //CallBackFromAskingCleaner
            bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
            {
                if (ev.CallbackQuery.Data == "callback1")
                {
                    await bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Ща проверим");
                    Program.isClean = Program.askingAnotherUsers(usersList);                    
                    if (!Program.isClean)
                    {
                        await bot.SendTextMessageAsync(Program.CleanUser.getId, "Ты точно офигел, иди заново прибираться:)");
                    }
                }
                else
                if (ev.CallbackQuery.Data == "callback2")
                {
                    await bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Ты че, офигел? Иди прибираться");
                };
            };

            //CallBack на добавление
            bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
            {
                int temple = 0;
                if (!botsItemList.isInStaff(itemForAdding) && Int32.TryParse(ev.CallbackQuery.Data, out temple))
                {
                    if (temple >= -5 && temple <= -1)
                    {
                        //Приводим к виду где приоритет 0-4
                        botsItemList.addItem(itemForAdding, temple + 5);
                        await bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Предмет успешно добавлен");
                    }
                }
            };

            //CallBackFromAsking
            bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>

            {
                if (ev.CallbackQuery.Data == "PositiveCallback")
                {
                    //проверка голосовал ли пользователь
                    foreach (User checkingUser in usersList)
                    {
                        if (!checkingUser.isUserVoited)
                        {
                            await bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Ваша реакция учтена");
                            checkingUser.hasVoted();
                            Program.positiveCount++;
                        }
                        else
                        {
                            await bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Вы уже голосовали");
                        }
                    }
                }
                else
                {
                    //проверка голосовал ли пользователь
                    foreach (User checkingUser in usersList)
                    {
                        if (!checkingUser.isUserVoited)
                        {
                            await bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Ваша реакция учтена");
                            checkingUser.hasVoted();
                        }
                        else
                        {
                            await bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Вы уже голосовали");
                        }
                    }
                }
            };
        }
    }
}
