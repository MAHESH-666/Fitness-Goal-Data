namespace Fitness_Goal.Models
{
     public class FoodItem
    {
        public string Name { get; set; }
        public string PortionSize { get; set; }
        public string Calories { get; set; }
        public string Protein { get; set; }
        public string Carbs { get; set; }
        public string Fats { get; set; }
    }

    public class TotalNutrition
    {
        public string TotalCalories { get; set; }
        public string TotalProtein { get; set; }
        public string TotalCarbs { get; set; }
        public string TotalFats { get; set; }
        public string Fiber { get; set; }
        public List<string> VitaminsMinerals { get; set; } = new List<string>();
    }

    public class MealAnalysis
    {
        public string MealType { get; set; }
        public string BalanceScore { get; set; }
        public string ProteinRatio { get; set; }
        public string CarbRatio { get; set; }
        public string FatRatio { get; set; }
    }

    public class HealthInsights
    {
        public string MealBalance { get; set; }
        public List<string> PositiveAspects { get; set; } = new List<string>();
        public List<string> ImprovementAreas { get; set; } = new List<string>();
        public List<string> Suggestions { get; set; } = new List<string>();
    }

    public class DietaryFlags
    {
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsDairyFree { get; set; }
        public List<string> Allergens { get; set; } = new List<string>();
    }

    public class FoodAnalysisResult
    {
        public List<FoodItem> FoodsIdentified { get; set; } = new List<FoodItem>();
        public TotalNutrition TotalNutrition { get; set; } = new TotalNutrition();
        public MealAnalysis MealAnalysis { get; set; } = new MealAnalysis();
        public HealthInsights HealthInsights { get; set; } = new HealthInsights();
        public DietaryFlags DietaryFlags { get; set; } = new DietaryFlags();
    }

    public class FoodAnalysisModel
    {
        public FoodAnalysisResult Result { get; set; } = new FoodAnalysisResult();
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
