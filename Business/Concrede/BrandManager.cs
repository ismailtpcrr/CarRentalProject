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
using Business.BusinessAspects.Autofac;

namespace Business.Concrede
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        ICarService _carService;
        public BrandManager(IBrandDal brandDal, ICarService carService)
        {
            _brandDal = brandDal;
            _carService = carService;

        }

        [SecuredOperation("brand.add,admin")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandNameExists(brand.BrandName), CheckIfBrandLimitExceded());

            if (result != null)
            {
                return result;
            }
            
            _brandDal.Add(brand);

            return new SuccessResult(Messages.BrandAdded);
        }

        public IResult Delete(Brand brand)
        { 
            IResult result = BusinessRules.Run(CheckIfBrandHasCars(brand.BrandId));

            if (result != null)
            {
                return result;
            }

            _brandDal.Delete(brand);

            return new SuccessResult();
        }

        public IDataResult<List<Brand>> GetAll()
        {
            
            return new SuccessDataResult<List<Brand>>(Messages.BrandListed,_brandDal.GetAll());
        }

        public IDataResult<Brand> GetById(int id)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.BrandId == id));
        }

        public IResult Update(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandNameExistsForUpdate(brand.BrandId, brand.BrandName));
       

            if (result != null)
            {
                return result;
            }

            _brandDal.Update(brand);

            return new SuccessResult();
        }

        private IResult CheckIfBrandNameExists(string brandName)
        {
            var result = _brandDal.GetAll(b => b.BrandName.ToLower().Trim() == brandName.ToLower().Trim()).Any();
            // kücük büyük ve boşluk duyarsız

            if (result)
            {
                return new ErrorResult(Messages.BrandNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfBrandLimitExceded()
        {
            var result = _brandDal.GetAll().Count;

            if(result > 10)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        private IResult CheckIfBrandHasCars(int brandId)
        {
            var result = _carService.GetByBrandId(brandId).Data.Any();

            if (result)
            {
                return new ErrorResult(); 
            }
            return new SuccessResult();
        }

        private IResult CheckIfBrandNameExistsForUpdate(int brandId ,string brandName)
        {
            var result = _brandDal.GetAll(b => b.BrandId != brandId && 
            b.BrandName.ToLower().Trim() == brandName.ToLower().Trim()).Any();

            if (result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }



    }
}
