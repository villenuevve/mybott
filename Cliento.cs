using DBFirstApp;
using mybot;
using Newtonsoft.Json;
using RestSharp;
using Telegram.Bot.Types;

namespace botChef
{
	public class Cliento
	{

		private HttpClient _client;
		private static string _address;
		private static string _key;

		public Cliento()
		{
			_address = Constanto.address;
			_key = Constanto.key;
			_client = new HttpClient();
			_client.BaseAddress = new Uri(_address);
		}

		public static async Task<Modelio> GetCalories(string food)
		{
			var client = new RestClient($"https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/guessNutrition?title={food}&Apikey={_key}");
			var request = new RestRequest();
			request.AddHeader("X-RapidAPI-Key", "ed7d33507fmsh3e29771d6995771p195f9bjsn29baba04f262");
			request.AddHeader("X-RapidAPI-Host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com");
			RestResponse response = client.Execute(request);
			var result = JsonConvert.DeserializeObject<Modelio>(response.Content);
			return result;

		}

		public async Task<List<Data>> GetRecipes(string dish)
		{
			var client = new RestClient($"https://recipe-by-api-ninjas.p.rapidapi.com/v1/recipe?query={dish}");
			var request = new RestRequest();
			request.AddHeader("X-RapidAPI-Key", "3469217803mshe7515dac4541358p1c7fadjsn6809889db67b");
			request.AddHeader("X-RapidAPI-Host", "recipe-by-api-ninjas.p.rapidapi.com");
			RestResponse response = client.Execute(request);
			var result = JsonConvert.DeserializeObject<List<Data>>(response.Content);
			return result;

		}

		public static async Task AddUser(long id, long idChat, string types, string recipes)
		{
			await using (helloappContext db = new helloappContext())
			{
				await db.Everything.AddRangeAsync(new Everythin
				{
					Id = id,
					ChatId = idChat,
					Types = types,
					Recipe = recipes,
					
				}) ;
				await db.SaveChangesAsync();
			}

		}
			
			

		
	}
}