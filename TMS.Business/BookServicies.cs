using System;
using TMS.Entities;
using TMS.Interfaces;

namespace TMS.Business
{
    public class BookServicies
    {
        private readonly IUnitOfWork unitOfWork;

        public BookServicies(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Create()
        {
            Random r = new Random();
            Book b = new Book() {Id =r.Next(1,1000000), Author = "Second", Name = "mike2", Price = 2};
            unitOfWork.Books.Create(b);

            unitOfWork.Save();
        }

        public void GetItems()
        {
           var res = unitOfWork.Books.GetItem(1);
            unitOfWork.Save();
        }
    }
}
