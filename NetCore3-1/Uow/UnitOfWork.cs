using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore3_1.DataContext;
using NetCore3_1.Entities;
using NetCore3_1.Repository;

namespace NetCore3_1.Uow
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SampleDbContext _context;
        private IGenericRepository<Product> _productRepository;
        private IGenericRepository<OrderItems> _orderItemsRepository;
        private IGenericRepository<Order> _orderRepository;

        public UnitOfWork(SampleDbContext context)
        {
            _context = context;
        }



        public IGenericRepository<Product> ProductRepository
        {
            get { return _productRepository ?? (_productRepository = new GenericRepository<Product>(_context)); }
        }

        public IGenericRepository<OrderItems> OrderItemsRepository
        {
            get
            {
                return _orderItemsRepository ?? (_orderItemsRepository = new GenericRepository<OrderItems>(_context));
            }
        }

        public IGenericRepository<Order> OrderRepository
        {
            get { return _orderRepository ?? (_orderRepository = new GenericRepository<Order>(_context)); }
        }

        public async Task Commit()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    _context.Dispose();
                    transaction.Rollback();
                }
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}
