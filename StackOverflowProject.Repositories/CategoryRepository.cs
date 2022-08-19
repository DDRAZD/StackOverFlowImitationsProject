using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface ICategoriesRepository
    {
        List<Category> GetCategories();
        List<Category> GetCategoryByID(int id);
        void DeleteCategory(int id);
        void UpdateCategory(Category category);
        void InsertCategory(Category category);

    }
    public class CategoryRepository : ICategoriesRepository
    {
        StackOverflowDatabaseDBContext db;
        //constructor
        public CategoryRepository()
        {
            db = new StackOverflowDatabaseDBContext();
        }

        public void DeleteCategory(int id)
        {
           Category categoryToDelete = db.Categories.Where(y=>y.CategoryID == id).FirstOrDefault();
            if(categoryToDelete != null)
            {
                db.Categories.Remove(categoryToDelete);
                db.SaveChanges();
            }
                
        }

        public List<Category> GetCategories()
        {
            return db.Categories.ToList();
        }

        public List<Category> GetCategoryByID(int id)
        {
            List<Category> category = db.Categories.Where(y=>y.CategoryID == id).ToList();
            return category;
        }

        public void InsertCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
           Category categoryToUpdate = db.Categories.Where(y=>y.CategoryID == category.CategoryID).FirstOrDefault();
            if(categoryToUpdate!=null)
            {
                categoryToUpdate.CategoryName = category.CategoryName;
                db.SaveChanges();

            }
           
        }
    }
}
