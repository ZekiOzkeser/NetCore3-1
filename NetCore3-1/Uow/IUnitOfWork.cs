using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore3_1.Entities;
using NetCore3_1.Repository;

namespace NetCore3_1.Uow
{
   public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<OrderItems> OrderItemsRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        Task Commit();
    }
}
