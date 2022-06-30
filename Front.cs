using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Exceptions;
using botChef;

using DBFirstApp;

namespace rechefsty_bot
{
    public class Chefsty
    {
        TelegramBotClient botClient = new TelegramBotClient("5451043727:AAGX6YiT6jZiHf6oZ8rFs_8aRMIPJpvxM0I");
        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
        public string lastmes;
        public string recipename { get; set; }

        public async Task Start()
        {
            botClient.StartReceiving(HandlerUpdateAsync, HandlerError, receiverOptions, cancellationToken);
            var botMe = await botClient.GetMeAsync();
            Console.WriteLine($"{botMe.Username} Bot has just started working");
            Console.ReadKey();
        }

        private Task HandlerError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellation)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException ApiRequestException => $"Error in Telegram bot API:\n{ApiRequestException.ErrorCode}" +
                $"\n{ApiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellation)
        {

            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandlerMessage(botClient, update.Message);
            }

            if (update?.Type == UpdateType.CallbackQuery)
            {
                await HandlerCallbackQuery(botClient, update.CallbackQuery);
            }

        }
        private async Task HandlerCallbackQuery(ITelegramBotClient botClient, CallbackQuery? callbackQuery)
        {
            long c = 0;

            if (callbackQuery.Data.StartsWith("Lunch"))
            {
                await Cliento.AddUser(c, callbackQuery.Message.Chat.Id, "Lunch", recipename);

                Console.WriteLine($"{callbackQuery.Message.Chat.Id} -  Lunch - {recipename}");
                return;
            }
            if (callbackQuery.Data.StartsWith("Breakfast"))
            {
                await Cliento.AddUser(c, callbackQuery.Message.Chat.Id, "Breakfast", recipename);

                Console.WriteLine($"{callbackQuery.Message.Chat.Id} -  Breakfast - {recipename}");
                return;
            }
            if (callbackQuery.Data.StartsWith("Dinner"))
            {
                await Cliento.AddUser(c, callbackQuery.Message.Chat.Id, "Dinner", recipename);

                Console.WriteLine($"{callbackQuery.Message.Chat.Id} -  Dinner - {recipename}");
                return;
            }
        }
        private async Task HandlerMessage(ITelegramBotClient botClient, Message message)
        {
            if (message.Text == "/start")
            {

                ReplyKeyboardMarkup replyKeyboardMarkup = new
                    (
                    new[]
                        {
                        new KeyboardButton [] { "Generate new recipe"},
                        new KeyboardButton [] { "My favourites" },
                        new KeyboardButton [] {"Calories count" },
                        }
                    )
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "What's cookin`, good lookin`?)\nMy name is Chefsty and now I`m your personal pocket chef, who can be your kitchen assistant :D\n" +
                    "\n" +
                    "What can I do for you to make cooking much easier?\n" +
                    "- generate recipes,  which are based on a written keyword, and save them\n" +
                    "- show up a suitable recipe referring to the availability of the ingredients\n" +
                    "- count calories in a wishful item\n\t" +
                    "\n" +
                    "There are several buttons under the keyboard that will help you to implement a specific function\n", replyMarkup: replyKeyboardMarkup);
                return;
            }
            else
            if (message.Text == "My favourites")
            {
                List<string> Recip = new List<string>();
                List<string> Typss = new List<string>();
                Help.ShowRecipes(Recip, message.Chat.Id);
                Help.ShowTypes(Typss, message.Chat.Id);


                long b = message.Chat.Id;
                //for(int i=0;i < Recip.Count(); i++)
                //foreach (string e in Recip)
                //foreach (string e in Help.ShowRecipes(message.Chat.Id, Recip))


                for (int i = 0; i < Recip.Count(); i++)//e - назви рецепти бази даних
                {
                    if (message.Chat.Id == b)
                    {
                        //Help.ShowRecipes(message.Chat.Id, Recip);

                        await botClient.SendTextMessageAsync(message.Chat.Id, text: $"{Recip[i]}\n!This recipe was saved to \"{Typss[i]}\" category");
                    }

                }

                return;
            }
            else
            if (message.Text == "Generate new recipe")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "You have clicked on \"Generate new recipe\"\n" +
                    "Write some keyword for me to find some suitable recipe for you\n");
            }
            else
            if (message.Text != null && lastmes == "Generate new recipe")
            {

                
                recipename = Help.Pov(message.Text);
                InlineKeyboardMarkup keyboardMarkup = new
                            (
                                new[]
                                {
                            new[]
                            {
                            InlineKeyboardButton.WithCallbackData("Breakfast", $"Breakfast"),
                            InlineKeyboardButton.WithCallbackData("Lunch", $"Lunch"),
                            InlineKeyboardButton.WithCallbackData("Dinner", $"Dinner"),
                            }
                                }
                            );

                await botClient.SendTextMessageAsync(message.Chat.Id, $"The recipe is called {recipename}\n" +
                    "You can save shown recipe as:\n", replyMarkup: keyboardMarkup);

                return;
            }
            /*
            else
                    if (message.Text == "What to cook?")
            {
                
                await botClient.SendTextMessageAsync(message.Chat.Id, "You have clicked on \"What to cook?\"\n");
            }
            else
            if (message.Text != null && lastmes == "What to cook?")
            {
                await Cliento. (message.Text);
                Console.WriteLine($"{Cliento.GetCalories(message.Text)}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"{Cliento.GetCalories(message.Text).Result.calories.value + " calories in your item"}");

                return;
            }
            */
             else
                if (message.Text == "hello" || message.Text == "hi" || message.Text == ":)" || message.Text == "HI" || message.Text == "h" || message.Text == "Hello" || message.Text == "HELLO")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Hello, I hope you`re doing well and ready to start!");

            }
            else
            if (message.Text == "Bye" || message.Text == "bb" || message.Text == "thx" || message.Text == "thanks" || message.Text == "thank you" || message.Text == "bye" || message.Text == "Thank" || message.Text == "b" || message.Text == "BYE")
            {

                await botClient.SendTextMessageAsync(message.Chat.Id, "Thank you for working with me! Have a nice day and good bye");
            }
            else
                    if (message.Text == "Calories count")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "You have clicked on \"Calories count\"\n" +
                    "Now you can write some food on the keynoard to find out its calories");
            }
            else
                    if (message.Text != null && lastmes == "Calories count")
            {
                await Cliento.GetCalories(message.Text);
                Console.WriteLine($"{Cliento.GetCalories(message.Text)}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"{Cliento.GetCalories(message.Text).Result.calories.value + " calories in your item"}");

                return;
            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Sorry, I don`t understand you :(\nWrite something else instead");

            }
            lastmes = message.Text;

        }

    }
}

