
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Dto
{
    public class ResponseStudent
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ResponseStudent MapTo(Student student)
        {
            Id = student.Id;
            Name = student.Name;

            return this;
        }
    }
}
