using Fitness_Goal.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Goal.Services
{
    public class FoodService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey = "add your api key";
        private readonly string _host = "ai-workout-planner-exercise-fitness-nutrition-guide.p.rapidapi.com";

        public FoodService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("x-rapidapi-key", _apiKey);
            _client.DefaultRequestHeaders.Add("x-rapidapi-host", _host);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<FoodAnalysisModel> AnalyzeFoodAsync(string imageUrl)
        {
            var url = $"https://{_host}/analyzeFoodPlate";

            var jsonBody = new JObject
            {
                ["imageUrl"] = imageUrl,
                ["lang"] = "en",
                ["noqueue"] = 1
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(jsonBody.ToString(), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            var model = new FoodAnalysisModel
            {
                Status = json["status"]?.ToString(),
                Message = json["message"]?.ToString()
            };

            var result = json["result"];
            if (result != null)
            {
                // Foods identified
                var foods = result["foods_identified"] as JArray;
                if (foods != null)
                {
                    foreach (var f in foods)
                    {
                        model.Result.FoodsIdentified.Add(new FoodItem
                        {
                            Name = f["name"]?.ToString(),
                            PortionSize = f["portion_size"]?.ToString(),
                            Calories = f["calories"]?.ToString(),
                            Protein = f["protein"]?.ToString(),
                            Carbs = f["carbs"]?.ToString(),
                            Fats = f["fats"]?.ToString()
                        });
                    }
                }

                // Total nutrition
                var total = result["total_nutrition"];
                if (total != null)
                {
                    model.Result.TotalNutrition.TotalCalories = total["total_calories"]?.ToString();
                    model.Result.TotalNutrition.TotalProtein = total["total_protein"]?.ToString();
                    model.Result.TotalNutrition.TotalCarbs = total["total_carbs"]?.ToString();
                    model.Result.TotalNutrition.TotalFats = total["total_fats"]?.ToString();
                    model.Result.TotalNutrition.Fiber = total["fiber"]?.ToString();

                    var vitamins = total["vitamins_minerals"] as JArray;
                    if (vitamins != null)
                    {
                        foreach (var v in vitamins)
                            model.Result.TotalNutrition.VitaminsMinerals.Add(v.ToString());
                    }
                }

                // Meal analysis
                var meal = result["meal_analysis"];
                if (meal != null)
                {
                    model.Result.MealAnalysis.MealType = meal["meal_type"]?.ToString();
                    model.Result.MealAnalysis.BalanceScore = meal["balance_score"]?.ToString();
                    model.Result.MealAnalysis.ProteinRatio = meal["protein_ratio"]?.ToString();
                    model.Result.MealAnalysis.CarbRatio = meal["carb_ratio"]?.ToString();
                    model.Result.MealAnalysis.FatRatio = meal["fat_ratio"]?.ToString();
                }

                // Health insights
                var health = result["health_insights"];
                if (health != null)
                {
                    model.Result.HealthInsights.MealBalance = health["meal_balance"]?.ToString();

                    var positives = health["positive_aspects"] as JArray;
                    if (positives != null)
                        foreach (var p in positives) model.Result.HealthInsights.PositiveAspects.Add(p.ToString());

                    var improvements = health["improvement_areas"] as JArray;
                    if (improvements != null)
                        foreach (var p in improvements) model.Result.HealthInsights.ImprovementAreas.Add(p.ToString());

                    var suggestions = health["suggestions"] as JArray;
                    if (suggestions != null)
                        foreach (var s in suggestions) model.Result.HealthInsights.Suggestions.Add(s.ToString());
                }

                // Dietary flags
                var flags = result["dietary_flags"];
                if (flags != null)
                {
                    model.Result.DietaryFlags.IsVegetarian = flags["is_vegetarian"]?.ToObject<bool>() ?? false;
                    model.Result.DietaryFlags.IsVegan = flags["is_vegan"]?.ToObject<bool>() ?? false;
                    model.Result.DietaryFlags.IsGlutenFree = flags["is_gluten_free"]?.ToObject<bool>() ?? false;
                    model.Result.DietaryFlags.IsDairyFree = flags["is_dairy_free"]?.ToObject<bool>() ?? false;

                    var allergens = flags["allergens"] as JArray;
                    if (allergens != null)
                        foreach (var a in allergens) model.Result.DietaryFlags.Allergens.Add(a.ToString());
                }
            }

            return model;
        }
    }
}
