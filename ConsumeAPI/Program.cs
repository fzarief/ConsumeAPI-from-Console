using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;

namespace ConsumeAPI
{
    public class Program
    {
        static void Main(string[] args)
        {
            //GET ALL MENU

            //foreach (var item in GetAllMenu().Result)
            //{
            //    Console.WriteLine($"ID : {item.Id}");
            //    Console.WriteLine($"Nama : {item.MenuName}");
            //    Console.WriteLine($"Stock : {item.Stock}");
            //    Console.WriteLine($"Price : {item.Price}");
            //    Console.WriteLine($"Kode : {item.Kode}");
            //    Console.WriteLine();
            //    Console.WriteLine();
            //}
            Show();
            //GET MENU FROM ID

            //var menuId = 13;
            //var menu = GetMenuById(menuId).Result;

            //Console.WriteLine("ID: " + menu.Id);
            //Console.WriteLine("Name: " + menu.MenuName);
            //Console.WriteLine("Stock: " + menu.Stock);
            //Console.WriteLine("Price: " + menu.Price);
            //Console.WriteLine("Kode: " + menu.Kode);

            //CREATE MENU

            //var newMenu = new KopiMenu
            //{
            //    MenuName = "Kopi torabica alami",
            //    Stock = 80,
            //    Price = 9000,
            //    Kode = ""
            //};
            //var result = CreateMenu(newMenu).Result;
            //Console.WriteLine("New menu created: " + result.MenuName);
            //Console.WriteLine("show all data below");
            //Console.WriteLine();
            //Show();

            //DELETE MENU

            //var menuId = 2012;
            //var menu = DeleteMenu(menuId).Result;
            //Console.WriteLine("Tampilkan data semuanya");
            //Console.WriteLine();
            //Show();

            //UPDATE MENU

            //var menuId = 1008;
            //var updatedMenu = new KopiMenu
            //{
            //    MenuName = "Nasi lemak kacang ubi",
            //    Stock = 50,
            //    Price = 5000,
            //    Kode = ""
            //};
            //var result = UpdateMenu(menuId, updatedMenu).Result;
            //Show();

            void Show()
            {
                //GET ALL MENU

                foreach (var item in GetAllMenu().Result)
                {
                    Console.WriteLine($"ID : {item.Id}");
                    Console.WriteLine($"Nama : {item.MenuName}");
                    Console.WriteLine($"Stock : {item.Stock}");
                    Console.WriteLine($"Price : {item.Price}");
                    Console.WriteLine($"Kode : {item.Kode}");
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }

            //var pokemons = GetPokemons().Result;
            //foreach (var pokemon in pokemons)
            //{
            //    Console.WriteLine($"Poke Name : {pokemon.Name}");
            //    Console.WriteLine($"url : {pokemon.url}");
            //}



        }
        public static async Task<List<KopiMenu>> GetAllMenu()
        {
            var url = "https://localhost:7282/api/KopiShop";
            //var url = "https://pokeapi.co/";

            var client = new HttpClient();

            var result = new List<KopiMenu>();

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var message = await client.SendAsync(request);

                var httpResult = await message.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<KopiMenu>>(httpResult);
                return result;


            }
        }

        public static async Task<KopiMenu> GetMenuById(int menuId)
        {
            var url = $"https://localhost:7282/api/KopiShop/{menuId}";

            var client = new HttpClient();

            var result = new List<KopiMenu>();

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var message = await client.SendAsync(request);

                var httpResult = await message.Content.ReadAsStringAsync();

                if (message.IsSuccessStatusCode)
                {
                    var n = await message.Content.ReadAsStringAsync();
                    var m = JsonConvert.DeserializeObject<KopiMenu>(n);
                    return m;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<KopiMenu> CreateMenu(KopiMenu newMenu)
        {
            var url = "https://localhost:7282/api/KopiShop";

            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(newMenu);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                request.Content = content;
                var message = await client.SendAsync(request);

                var httpResult = await message.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<KopiMenu>(httpResult);
                return result;
            }
        }



        public static async Task<KopiMenu> DeleteMenu(int menuId)
        {
            var url = $"https://localhost:7282/api/KopiShop/{menuId}";

            var client = new HttpClient();

            using (var request = new HttpRequestMessage(HttpMethod.Delete, url))
            {
                var message = await client.SendAsync(request);

                if (!message.IsSuccessStatusCode)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<KopiMenu>(await message.Content.ReadAsStringAsync());
            }
        }

        public static async Task<KopiMenu> UpdateMenu(int menuId, KopiMenu updatedMenu)
        {
            var url = $"https://localhost:7282/api/KopiShop/{menuId}";

            var client = new HttpClient();

            using (var request = new HttpRequestMessage(HttpMethod.Put, url))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(updatedMenu), Encoding.UTF8, "application/json");
                var message = await client.SendAsync(request);

                if (message.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<KopiMenu>(await message.Content.ReadAsStringAsync());
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
        public static async Task<List<Pokemon>> GetPokemons()
        {
            var url = "https://pokeapi.co/api/v2/pokemon?limit=5";

            var client = new HttpClient();

            var result = new List<Pokemon>();

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var message = await client.SendAsync(request);

                var httpResult = await message.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<PokemonList>(httpResult).Results;
                return result;
            }
        }

    }
}
