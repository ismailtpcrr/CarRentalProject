using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrede;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrede
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            IResult result = BusinessRules.Run(CheckIfCustomerEmailExists(customer.Email),CheckIfCustomerPhoneExists(customer.Phone));

            if (result != null)
            {
                return result;

            }

            _customerDal.Add(customer);

            return new SuccessResult(Messages.CustomerAdded);
        }

        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult();
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        public IDataResult<Customer> GetById(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.CustomerId == id));
        }

        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult();
        }


        private IResult CheckIfCustomerEmailExists(string customerEmail)
        {
            var result = _customerDal.GetAll(c => c.Email == customerEmail).Any();

            if (result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IResult CheckIfCustomerPhoneExists(string customerPhone)
        {
            var result = _customerDal.GetAll(c => c.Phone == customerPhone).Any();

            if (result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

    }
}
