using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bitard_BlockChain_Bot_Unit_Test
{
    public class Program
    {
        //myOwnNumberOfChat 347955632
        //EgorId 429664470
        //OlyaId 243390057
        //Id  с разных устройств разные?

        /// <summary>
        /// код для callbacks:
        ///     для добавления(код приоритета)
        ///     для удаления(номер элемента)
        /// </summary>

        //DialogState
        //State 0 = zeroState
        //State 1 = Adding new User
        //State 2 addingNewItem
        static int state = 0;

        //Users List
        static Queue<User> usersList = new Queue<User>();

        //StaffList
        static staff botsItemList = new staff();


        //Initilaze bor API
        static ITelegramBotClient botClient;

        //Кнопки команд
        static InlineKeyboardMarkup commandkeyBoard = new InlineKeyboardMarkup(
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

        //Обработка кнопок команд

        //Запуск бота
        public static void Main(string[] args)
        {
            botClient = new TelegramBotClient("666764887:AAENxCN5kKWXtFjCfv6dVTCH6S8oXxJOJgg");
            var me = botClient.GetMeAsync().Result;
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            //CallBackFromAskingCommandLine
            botClient.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>

            {
                switch(ev.CallbackQuery.Data)
                {
                    case "showEgor":
                        await botClient.SendPhotoAsync(ev.CallbackQuery.Message.Chat, "https://pp.userapi.com/c846122/v846122367/8b1da/oM0jrFvVu8Y.jpg");
                        await botClient.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                        break;
                    case "showSeva":
                        await botClient.SendPhotoAsync(ev.CallbackQuery.Message.Chat, "https://pp.userapi.com/c824202/v824202167/183d3e/S2DcqEvIIKg.jpg");
                        await botClient.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                        break;
                    case "showBogdan":
                        await botClient.SendPhotoAsync(ev.CallbackQuery.Message.Chat, "https://pp.userapi.com/c830708/v830708039/1890f7/ehrKDFthYI4.jpg");
                        await botClient.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                        break;
                    case "getId":
                        await botClient.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ev.CallbackQuery.Message.Chat.Id.ToString());
                        await botClient.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                        break;
                    case "getItemList":
                        if (botsItemList.getSize == 0)
                        {
                            await botClient.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, "Список пуст");
                            await botClient.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, botsItemList.getListOfItems());
                        }
                        break;
                    case "addNewItem":
                        await botClient.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, "Что нужно купить?)");
                        state = 1;
                        break;
                        //Разобраться с вылетом аля добавить три элемента, удалить второй, третий(программа вылетает)
                    case "deleteItem" :
                        if (!(botsItemList.getSize == 0))
                        {
                            var temple = botsItemList.getInlineKeyboard;

                            await botClient.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, "Какой предмет вы хотите удалить?", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, temple);

                            botClient.OnCallbackQuery += async (object sc_2, Telegram.Bot.Args.CallbackQueryEventArgs ev_2) =>

                            {
                                //Пробное преобразование даты для удаления
                                int templeInt = 0;
                                if (Int32.TryParse(ev_2.CallbackQuery.Data, out templeInt))
                                {
                                    botsItemList.deleteItemAt(templeInt);
                                    await botClient.AnswerCallbackQueryAsync(ev_2.CallbackQuery.Id, "Предмет удален");
                                    await botClient.SendTextMessageAsync(ev_2.CallbackQuery.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                                }
                            };
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, "Список пуст");
                            await botClient.SendTextMessageAsync(ev.CallbackQuery.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                        }
                        break;
                    default:
                        break;
                }
            };
            Task CleanTask = new Task(CleanRem);
            CleanTask.Start();
            Thread.Sleep(int.MaxValue);
        }

        //Checking Message and Dialog
        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                //Help comand
                //Может записать inline кнопками?
                if (e.Message.Text == "/help" && state == 0)
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, commandkeyBoard);
                    Console.WriteLine(e.Message.Chat.Id);
                }
                else
                {
                    if (state == 0)
                    {
                        await botClient.SendTextMessageAsync(e.Message.Chat, "Введите /help чтобы увидеть все возможности бота");
                    }
                }
                if (state == 1 && e.Message.Text.Length != 0)
                {
                    addNewItemToList(botsItemList, e, e.Message.Text);
                    state = 0;
                }
            }
        }
    

        //CleanBlock
        static async void CleanRem()
        {
            while (true)
            {
                CleanBlock CleanerReminder = new CleanBlock(usersList);
                CleanerReminder.setNewDate();
                Console.WriteLine(CleanerReminder.getData);

                //Ждем пока не придет время
                while (CleanerReminder.getData > DateTime.Now)
                {
                    Thread.Sleep(3600000);
                }
                
                //Choose user, who will clean our dormitory
                User CleanUser = usersList.Dequeue();

                //Reminder
                //Может и не нужна?
                await botClient.SendTextMessageAsync(CleanUser.getId, "Завтра твоя очередь убираться");

                //Время до 12 утра субботы
                //Может увеличить до 18?
                Thread.Sleep(3600000 * 16);

                //Время на приборку в течении 3 часов
                await botClient.SendTextMessageAsync(CleanUser.getId, "Пора прибираться");
                //Thread.Sleep(3600000 * 3);

                //Val for saving state of our dormitary
                bool isClean = false;

                //remind our user about cleaning every 3 hours
                for (int i = 0; i < 11; i++)
                {

                       //Asking every 3 hours about is job done or not
                        Thread.Sleep(3600000 * 3);
                        var askingCleanerKeyboard = new InlineKeyboardMarkup(
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

                        await botClient.SendTextMessageAsync(CleanUser.getId, "Прибрался?", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, askingCleanerKeyboard);

                        //CallBackFromAskingCleaner
                        botClient.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
                        {
                            if (ev.CallbackQuery.Data == "callback1")
                            {
                                await botClient.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Ща проверим");
                                isClean = askingAnotherUsers(usersList);                                
                                if (!isClean)
                                {
                                    await botClient.SendTextMessageAsync(CleanUser.getId, "Ты точно офигел, иди заново прибираться:)");
                                }
                            }
                            else
                            if (ev.CallbackQuery.Data == "callback2")
                            {
                                await botClient.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Ты че, офигел? Иди прибираться");
                            };
                        };
                    if (isClean)
                    {
                        i = 12;
                    }
                }
                await botClient.SendTextMessageAsync(CleanUser.getId, "Красава");
                isClean = false;
                usersList.Enqueue(CleanUser);
            }
        }

        //Сделать проверку, голосовал ли пользователь или нет
        static bool askingAnotherUsers(Queue<User> usersList)
        {
            foreach(User temple in usersList)
            {
                temple.hasNotVoted();
            }
            int positiveCount = 0;
            foreach(User templeUser in usersList)
            {                
                var keyBoard = new InlineKeyboardMarkup(
                new InlineKeyboardButton[][]
                {
                    new[]
                    {
                        //Positive Reaction
                        InlineKeyboardButton.WithCallbackData("Да","PositiveCallback"),

                        //Negative Reaction
                        InlineKeyboardButton.WithCallbackData("Нет", "NegativeCallback"),
                    },
                } );

                botClient.SendTextMessageAsync(templeUser.getId, "Он прибрался?)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, keyBoard);
            }

            //CallBackFromAsking
            botClient.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>

            {
                if (ev.CallbackQuery.Data == "PositiveCallback")
                {
                    //проверка голосовал ли пользователь
                    foreach (User checkingUser in usersList)
                    {
                        if (!checkingUser.isUserVoited)
                        {
                            await botClient.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Ваша реакция учтена");
                            checkingUser.hasVoted();
                            positiveCount++;
                        }
                        else
                        {
                            await botClient.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Вы уже голосовали");
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
                            await botClient.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Ваша реакция учтена");
                            checkingUser.hasVoted();
                        }
                        else
                        {
                            await botClient.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Вы уже голосовали");
                        }
                    }
                }
            };

            //Время на опрос других пользователей 60 минут
            Thread.Sleep(1000 * 60 * 60);
            return 2 <= positiveCount;
        }

        //Inline опрос по поводу приоритета
        async static void addNewItemToList(staff itemsList, MessageEventArgs e, string item)
        {
            var priorityKeyboard = new InlineKeyboardMarkup(
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

            await botClient.SendTextMessageAsync(e.Message.Chat, "Какой приоритет у этой покупки?", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, priorityKeyboard);

            botClient.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
            {
                int temple = 0;
                if (!botsItemList.isInStaff(item) && Int32.TryParse(ev.CallbackQuery.Data, out temple))
                {
                    if(temple >= -5 && temple <= -1)
                    {
                        //Приводим к виду где приоритет 0-4
                        botsItemList.addItem(item, temple + 5);
                        await botClient.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Предмет успешно добавлен");
                    }
                }
            };
        }
    }
}

/*
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
            }*/
            /*сделать фабрику для производства модулей для прл обработки пользователей
              сделать с=отдельный файл для callback ибо возможны коллизии
              */
