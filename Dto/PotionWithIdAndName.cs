using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Dto
{
    public class PotionWithIdAndName
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual PotionWithIdAndName MapTo(Potion potion)
        {
            Id = potion.Id;
            Name = potion.Name;

            return this;
        }
    }
}
