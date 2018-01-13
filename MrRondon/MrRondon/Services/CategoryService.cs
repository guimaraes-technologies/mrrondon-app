using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services
{
    public class CategoryService
    {
        public async Task<IList<Category>> GetCategories()
        {
            var items = new List<Category>
            {
                new Category
                {
                    CategoryId = 1,
                    Name = "Aventura"
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Hospedagem"
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Serviços Úteis"
                },
                new Category
                {
                    CategoryId = 4,
                    Name = "Gastronomia"
                }
            };

            items.AddRange(new List<Category>
            {
                new Category
                {
                    CategoryId = 5,
                    SubCategoryId = 4,
                    Name = "Aeroclube"
                },
                new Category
                {
                    CategoryId = 6,
                    SubCategoryId = 4,
                    Name = "AirSoft"
                },
                new Category
                {
                    CategoryId = 7,
                    SubCategoryId = 2,
                    Name = "Camping"
                },
                new Category
                {
                    CategoryId = 8,
                    SubCategoryId = 3,
                    Name = "Kart"
                },
                new Category
                {
                    CategoryId = 9,
                    SubCategoryId = 2,
                    Name = "Paintball"
                },
                new Category
                {
                    CategoryId = 10,
                    SubCategoryId = 1,
                    Name = "Rapel"
                },
            });
            await Task.Delay(10);
            return items;
        }
    }
}