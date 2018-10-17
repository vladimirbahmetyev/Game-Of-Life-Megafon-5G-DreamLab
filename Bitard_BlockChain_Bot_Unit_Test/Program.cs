using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using System.ComponentModel;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bitard_BlockChain_Bot_Unit_Test
{
    public class Program
    {
        //myOwnNumberOfChat 347955632
        //EgorId 429664470
        //OlyaId 243390057
        //DialogState

        static int state = 0;
        //State 0 = zeroState
        //State 1 = Adding new User
        //State 2 addingNewItem

        //Users List
        static Queue<User> usersList = new Queue<User>();

        //Initilaze bor API
        static ITelegramBotClient botClient;

        //Запуск бота
        public static void Main(string[] args)
        {
            botClient = new TelegramBotClient("666764887:AAENxCN5kKWXtFjCfv6dVTCH6S8oXxJOJgg");
            var me = botClient.GetMeAsync().Result;
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Task CleanTask = new Task(CleanRem);
            CleanTask.Start();
            Thread.Sleep(int.MaxValue);
        }

        //Checking Message and Dialog
        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            staff botsItemList = new staff();
            if (e.Message.Text != null)
            {
                //Help comand
                if (e.Message.Text == "/help" && state == 0)
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat, "/AddUser \n /ShowUser \n /showEgor \n /showBogdan \n /showSeva \n /Test \n /getId");
                    Console.WriteLine(e.Message.Chat.Id);
                }

                //Input name new user
                if (state == 1)
                {
                    User temple = new User(e.Message.Text, e.Message.Chat.Id);
                    usersList.Enqueue(temple);
                    state = 0;
                    await botClient.SendTextMessageAsync(e.Message.Chat, "User has been added");
                }

                //Getid
                if (e.Message.Text == "/getId")
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat, e.Message.Chat.Id.ToString());
                }

                //Show User_1
                if (e.Message.Text == "/showEgor" && state == 0)
                {
                    await botClient.SendPhotoAsync(e.Message.Chat, "https://pp.userapi.com/c846122/v846122367/8b1da/oM0jrFvVu8Y.jpg");
                }
                //Show User_2
                if (e.Message.Text == "/showBogdan" && state == 0)
                {
                    await botClient.SendPhotoAsync(e.Message.Chat, "https://pp.userapi.com/c830708/v830708039/1890f7/ehrKDFthYI4.jpg");
                }

                //Show User_3
                if (e.Message.Text == "/showSeva" && state == 0)
                {
                    await botClient.SendPhotoAsync(e.Message.Chat, "https://pp.userapi.com/c824202/v824202167/183d3e/S2DcqEvIIKg.jpg");
                }

                //Change state to Added User
                if (e.Message.Text == "/AddUser" && state == 0)
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat, "Input User's Name");
                    state = 1;
                }

                //TestFunc
                if (e.Message.Text == "/Test")
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat, "first ");
                    Thread.Sleep(100);
                    await botClient.SendTextMessageAsync(e.Message.Chat, "second");
                }

                //ShowLastUser
                if (e.Message.Text == "/ShowUser" && state == 0)
                {
                    if (usersList.Count == 0)
                    {
                        await botClient.SendTextMessageAsync(e.Message.Chat, "There is no users");
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(e.Message.Chat, usersList.Peek().getName);
                    }
                }

                //getItemsList
                if (e.Message.Text == "/getStaffList" && state == 0)
                {
                    if (botsItemList.getSize == 0)
                    {
                        await botClient.SendTextMessageAsync(e.Message.Chat, "List is empty");
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(e.Message.Chat, botsItemList.getListOfItems());
                    }
                }

                //addNewItemToList
                if (e.Message.Text == "/addItemToList" && state == 0)
                {
                    state = 2;
                    await botClient.SendTextMessageAsync(e.Message.Chat, "Что нужно купить?)");
                }

                //addiingNewItem
                //Придумать как добавить предмет в 2 этапа (название + приоритет) 
                if (state == 2)
                {
                    if (e.Message.Text.Length == 0)
                    {
                        await botClient.SendTextMessageAsync(e.Message.Chat, "Твою душу не купить, сорян:(");
                    }
                    else
                    {
                        botsItemList.addItem(e.Message.Text);                        
                        await botClient.SendTextMessageAsync(e.Message.Chat, e.Message.Text + "успешно добавлена в список покупок");
                        state = 0;
                    }
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
                while (CleanerReminder.getData > DateTime.Now)
                {
                    Thread.Sleep(3600000);
                }
                
                //Choose user, who will clean our dormitory
                User CleanUser = usersList.Dequeue();

                //Reminder
                //Может и не нужна?ы
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
                        var keyboard = new InlineKeyboardMarkup(
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
                        await botClient.SendTextMessageAsync(CleanUser.getId, "Прибрался?", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, keyboard);

                        //CallBackFromAskingCleaner
                        botClient.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>

                        {
                            var message = ev.CallbackQuery.Message;
                            if (ev.CallbackQuery.Data == "callback1")
                            {
                                await botClient.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Ща проверим");
                                isClean = askingAnotherUsers(usersList);                                
                                if (!isClean)
                                {
                                    await botClient.SendTextMessageAsync(CleanUser.getId, "Ты точно ахуел, иди заново прибираться:)");
                                }
                            }
                            else
                            if (ev.CallbackQuery.Data == "callback2")
                            {
                                await botClient.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Ты че, ахуел? Иди прибираться");
                            };
                        };
                    Thread.Sleep(30 * 60 * 1000);
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
                var message = ev.CallbackQuery.Message;
                if (ev.CallbackQuery.Data == "PositiveCallback")
                {
                    //проверка голосовал ли пользователь
                    foreach (User checkingUser in usersList)
                    {
                        if (!checkingUser.isUserVoited)
                        {
                            await botClient.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Ваша реакция учтена");
                            Console.WriteLine("Пользователь согласился");
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
                            Console.WriteLine("Пользователь не согласился");
                            checkingUser.hasVoted();
                        }
                        else
                        {
                            await botClient.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Вы уже голосовали");
                        }
                    }
                };
            };

            //Time for Asking other users
            Thread.Sleep(1000 * 60 * 30);
            return 2 <= positiveCount;
        }
    }
}
