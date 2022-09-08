using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Mvc5;
using System.Web.Mvc;
using StackOverflowProject.ServiceLayer;

namespace StackOverFlowImitationsProject
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            //we will create a container and register the types

            container.RegisterType<IQuestionService, QuestionService>(); //every time you see IQuestino, swap it at run time with Question
            container.RegisterType<ICategoriesService, CategoriesService>();            
            container.RegisterType<IUserservice,UserService>();//in any controller, whenever we refeernce IUserserive, it will be swapped with UserService


            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));//because we have both WebAPI and MVC dependancy injection, we need 
            //to align them, hence we are sending the MVC resolver to the other resolver

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);//this allows dependancy injection for WebAPI

            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
           
        }
    }
}