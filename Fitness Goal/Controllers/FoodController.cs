using Fitness_Goal.Models;
using Fitness_Goal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LustIn.Controllers
{
    public class FoodController : Controller
    {
        private readonly FoodService _foodService;

        public FoodController()
        {
            _foodService = new FoodService();
        }

        public async Task<IActionResult> Index()
        {
            string imageUrl = "https://upload.wikimedia.org/wikipedia/commons/b/bd/Breakfast_foods.jpg";

            FoodAnalysisModel result = await _foodService.AnalyzeFoodAsync(imageUrl);

            return View(result);
        }
    }
}
