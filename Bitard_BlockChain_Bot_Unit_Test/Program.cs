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

        //Users List
        static Queue<User> usersList = new Queue<User>();

        //StaffList
        static Staff botsItemList = new Staff();

        //Initilaze bor API
        static ITelegramBotClient botClient;

        //CallbackFactory
        static BotCallbacks callBacksFactory = new BotCallbacks();

        //Buttons factory
        static BotInlineButtonsFactory  buttonsFactory = new BotInlineButtonsFactory();

        //CountForCleanAsking
        public static int positiveCount = 0;

        //Val for saving state of our dormitary
        public static bool isClean = false;

        //"Чистильщик"
        public static User CleanUser = null;

        //DialogState
        //state 0: dialog state
        //state 1: waiting new item to add
        public static int state = 0;

        //Инициализация всех полей бота
        public static void Main(string[] args)
        {
            botClient = new TelegramBotClient("666764887:AAENxCN5kKWXtFjCfv6dVTCH6S8oXxJOJgg");
            var me = botClient.GetMeAsync().Result;
            botClient.OnMessage += Bot_OnMessage;

            callBacksFactory.addCallbacks(botClient, botsItemList, buttonsFactory.getComandLine, usersList);

            botClient.StartReceiving();
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
                if (e.Message.Text == "/help" && state == 0)
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat, ":)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, buttonsFactory.getComandLine);
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
            CleanBlock CleanerReminder = new CleanBlock(usersList);
            while (true)
            {
                CleanerReminder.setNewDate();
                Console.WriteLine(CleanerReminder.getData);

                //Ждем пока не придет время
                while (CleanerReminder.getData > DateTime.Now)
                {
                    Thread.Sleep(3600000);
                }
                
                //Choose user, who will clean our dormitory
                CleanUser = usersList.Dequeue();

                //Reminder
                //Может и не нужна?
                await botClient.SendTextMessageAsync(CleanUser.getId, "Завтра твоя очередь убираться");

                //Время до 12 утра субботы
                //Может увеличить до 18?
                Thread.Sleep(3600000 * 16);

                //Время на приборку в течении 3 часов
                await botClient.SendTextMessageAsync(CleanUser.getId, "Пора прибираться");
                Thread.Sleep(3600000 * 3);

                //remind our user about cleaning every 3 hours
                for (int i = 0; i < 11; i++)
                {

                    //Asking every 3 hours about is job done or not
                    Thread.Sleep(3600000 * 3);
                    await botClient.SendTextMessageAsync(CleanUser.getId, "Прибрался?", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, buttonsFactory.getAskingCleanerLine);
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
        public static bool askingAnotherUsers(Queue<User> usersList)
        {
            foreach(User temple in usersList)
            {
                temple.hasNotVoted();
            }
            foreach(User templeUser in usersList)
            {                
                botClient.SendTextMessageAsync(templeUser.getId, "Он прибрался?)", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, buttonsFactory.getAskingUsersLine);
            }

            //Время на опрос других пользователей 60 минут
            Thread.Sleep(1000 * 60 * 60);
            return 2 <= positiveCount;
        }

        //Inline опрос по поводу приоритета
        async static void addNewItemToList(Staff itemsList, MessageEventArgs e, string item)
        {
            callBacksFactory.setItem(item);
            await botClient.SendTextMessageAsync(e.Message.Chat, "Какой приоритет у этой покупки?", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, buttonsFactory.getPriorityLine);
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
