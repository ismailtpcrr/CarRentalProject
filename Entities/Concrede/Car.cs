using Core.Entities;

namespace Entities.Concrede
{
    public class Car : IEntity
    {
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public decimal DailyPrice { get; set; }
        public string Plate { get; set; }


    }
}
