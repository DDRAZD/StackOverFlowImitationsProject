using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;
using StackOverflowProject.ViewModels;
using StackOverflowProject.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace StackOverFlowProject.ServiceLayer
{
    public interface ICategoriesService
    {
        void InsertCategory(CategoryViewModel cvm);
        void UpdateCategory(CategoryViewModel cvm);
        void DeleteCategory(int cid);
        List<CategoryViewModel> GetAllCategories();
       
        CategoryViewModel GetCategoryByCategoryId(int id);
    }
    public class CategoriesService: ICategoriesService
    {
        ICategoriesRepository cr;

        //constructor
        public CategoriesService()
        {
            cr = new CategoryRepository();
        }

        public void DeleteCategory(int cid)
        {
          cr.DeleteCategory(cid);
        }

        public List<CategoryViewModel> GetAllCategories()
        {
            
           List<Category> categories= cr.GetCategories();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Category,CategoryViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<CategoryViewModel> cvm = mapper.Map<List<Category>, List<CategoryViewModel>>(categories); //left is the source type, right is the destination type, like massive casting

            return cvm;
        }

        public CategoryViewModel GetCategoryByCategoryId(int id)
        {
            Category category = cr.GetCategoryByID(id).FirstOrDefault();
            CategoryViewModel cvm = null;
            //map only if you found something:
            if(category != null)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Category, CategoryViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                cvm = mapper.Map<Category, CategoryViewModel>(category); //left is the source type, right is the destination type, like massive casting
            }

            return cvm;
        }

        public void InsertCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CategoryViewModel, Category>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Category category = mapper.Map<CategoryViewModel, Category>(cvm); //left is the source type, right is the destination type, like massive casting
            cr.InsertCategory(category);
        }

        public void UpdateCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CategoryViewModel, Category>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Category category = mapper.Map<CategoryViewModel, Category>(cvm); //left is the source type, right is the destination type, like massive casting
            cr.UpdateCategory(category);
        }
    }
}
