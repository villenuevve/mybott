using System;
using DBFirstApp;

namespace botChef
{
	public class Help
	{
		public static string Pov(string? lastmess)
		{
			Cliento cliento = new Cliento();

			var dishlist = cliento.GetRecipes(lastmess).Result;
			Console.WriteLine(dishlist.Count);
			Random random = new Random();
			int i = random.Next(0, dishlist.Count);

			string randrecipe = $"{dishlist[i].title}\nIngredients:\n{dishlist[i].ingredients}\nRecipe:\n{dishlist[i].instructions}\n";
			return randrecipe;
			//dishlist - desersjontoapi, vid modeli
		}
		public static async Task<List<string>> ShowRecipes(List<string> Recipes, long chaid)//для myfav з бд
		{
			//List<string> Typs = new List<string>();
			using (helloappContext db = new helloappContext())
			{
				// получаем объекты из бд и выводим на консоль
				var data = db.Everything.ToList();
				Console.WriteLine("ShowRecipes");

				for (int i = 0; i < data.Count(); i++)
				{
					if (data[i].ChatId == chaid)
					{

						Recipes.Add(data[i].Recipe);

						//Console.WriteLine($"{data[i].Recipe} отчет {data[i].Ty}");

					}

				}

			}

			return Recipes;

		}

		public static async Task<List<string>> ShowTypes(List<string> Tps, long chaid)//
		{
			//List<string> Typs = new List<string>();
			using (helloappContext db = new helloappContext())
			{
				// получаем объекты из бд и выводим на консоль
				var data = db.Everything.ToList();
				Console.WriteLine("ShowTypes");

				for (int i = 0; i < data.Count(); i++)
				{
					if (data[i].ChatId == chaid)
					{
						Tps.Add(data[i].Types);


						//Console.WriteLine($"{data[i].Recipe} отчет {data[i].Ty}");

					}

				}
				return Tps;

			}
		}
		/*
		public static async Task<List<string>> ShowIngredients(List<string> Ingredients, long chaid)//для myfav
		{
			//List<string> Typs = new List<string>();
			using (helloappContext db = new helloappContext())
			{
				// получаем объекты из бд и выводим на консоль
				var data = db.Everything.ToList();
				Console.WriteLine("ShowRecipes");

				for (int i = 0; i < data.Count(); i++)
				{
					if (data[i].ChatId == chaid)
					{

						Ingredients.Add(data[i].Ingredients);

						//Console.WriteLine($"{data[i].Recipe} отчет {data[i].Ty}");

					}

				}

			}

			return Ingredients;

		}
		
		public static string Cutter(string ingredients, string wehave)
		{
			string phrase = ingredients;
			string phraswehave = wehave;
			string[] words = phrase.Split('|');
			string[] wordie = phraswehave.Split(',');
			int u = 0;//скільки має бути
			int w = 0;//скільки є елементів

			foreach (var word in words)
			{
				u++;
				foreach(var wo in wordie)
                {

					if (wo == word)
					{
						Console.WriteLine(w++);
					}
				}
				
			}
            if (u/2 == w)
            {

            }
			return wordie;
		}
		
		public static string Ingridients(string? dishname)
        {
			
				Cliento cliento = new Cliento();
				var ing = cliento.GetRecipes(dishname).Result;
			string ingred = "";

		    for(int eda=0; eda < ing.Count(); eda++)
            {
				ingred = $"{ing[eda].ingredients}";
			}

            Console.WriteLine(ingred);
			return ingred;
		} 
		*/


	}
}