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
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [ValidationAspect(typeof(CarValidator))]

        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(CheckIfPlateExists(car.Plate));

            if (result != null)
            {
                return result;
            }

           _carDal.Add(car);

            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult();
        }

        public IDataResult<List<Car>> GetAll()
        {
            if(DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(Messages.CarListed ,_carDal.GetAll());
        }

        public IDataResult<List<Car>> GetByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId)); 
        }

        public IDataResult<List<Car>> GetByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == id));
        }

        public IResult Update(Car car)
        {
            IResult result = BusinessRules.Run(CheckIfPlateExistsForUpdate(car.CarId ,car.Plate));

            if (result != null)
            {
                return result;
            }

            _carDal.Update(car);
            return new SuccessResult();
        }

        private IResult CheckIfPlateExists(string plate)
        {
            var result = _carDal.GetAll(c => c.Plate.ToLower().Trim() == plate.ToLower().Trim()).Any();

            if (result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();

        }

        private IResult CheckIfPlateExistsForUpdate(int carId ,string plate)
        {
            var result = _carDal.GetAll(c => c.CarId != carId && c.Plate.ToLower().Trim() == plate.ToLower().Trim()).Any();

            if (result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();

        }




    }
}
