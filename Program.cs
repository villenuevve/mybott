using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Exceptions;
using botChef;
using rechefsty_bot;
using DBFirstApp;

Cliento cliento = new Cliento();

var dishlist = cliento.GetRecipes("potato").Result;

Random random = new Random();
int i = random.Next(0, 10);
Console.WriteLine($"{dishlist[i].title}\nIngredients:\n{dishlist[i].ingredients}\nRecipe:\n{dishlist[i].instructions}\n");
string randrecipe = dishlist[i].title;

//Console.WriteLine(Cliento.GetCalories("apple").Result.calories.value + " calories in your item");
//Console.WriteLine();

Chefsty chefsty = new Chefsty();
chefsty.Start();



using (helloappContext db = new helloappContext())
{
    var users = db.Everything.OrderBy(p => p.Id);
    foreach (var d in users)
        Console.WriteLine($"{d.Id} - {d.Recipe}");
    
}

Console.ReadLine();
