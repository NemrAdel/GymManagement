using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.SeedDara
{
    public static class GymDbContextSeeding
    {
        public static bool SeedData(GymDbContext dbContext)
        {
            try
            {
                var HasPlans = dbContext.Plans.Any();
                var HasCategories = dbContext.Categories.Any();
                if (HasPlans && HasCategories) return false;

                if (!HasPlans)
                {
                    var plans = LoadDataFromJson<Plan>("Plans.json");
                    if (plans.Any())
                    {
                        dbContext.AddRange(plans);
                    }
                }
                if (!HasCategories)
                {
                    var categories = LoadDataFromJson<Category>("Categories.json");

                    if (categories.Any())
                    {
                        dbContext.AddRange(categories);
                    }
                }
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }

        }
        private static List<T> LoadDataFromJson<T>(string FileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\SeedFiles",FileName);
            if (!File.Exists(filePath)) return [];
            var jsonData = File.ReadAllText(filePath);
            var options =new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<T>>(jsonData) ?? [];
        }// wwwRoot always exist in presentation layer


    }   }
