using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common;
using RestaurantReservation.Data.Models;
using RestaurantReservation.Service.Repository;
using RestaurantReservation.Data;
using RestaurantReservation.Service.IRepository; // Adjust the namespace as needed

namespace RestaurantReservation
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // Bind the LoopDBContext
            _kernel.Bind<LoopDBContext>().ToSelf().InRequestScope();
            // Bind other services if needed
            _kernel.Bind<IAdmin>().To<Admin>();
            _kernel.Bind<IRestaurant>().To<Restaurant>();
            _kernel.Bind<IUser>().To<User>();
        }
    }

}
