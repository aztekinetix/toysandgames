using DataRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public class MyRepository : IRepository<Toy>
    {

        private Object Loki = new Object();
        public IEnumerable<Toy> List()
        {

                return DataUtils.LoadJsonFile();
            
        }

        public void Add(Toy entity)
        {
            lock (Loki)
            {
                var collectionOfToys = DataUtils.LoadJsonFile();
                var listOfToys = new List<Toy>();
                int highestId = 0;
                if (collectionOfToys != null && collectionOfToys.Count() > 0)
                {
                    highestId = collectionOfToys.Select(p => p.Id).Max();
                    listOfToys = collectionOfToys.Cast<Toy>().ToList();
                }

                entity.Id = highestId + 1; //changing or adding the Id to the entity
                listOfToys.Add(entity); // adding to the List
                DataUtils.SaveJsonFile(listOfToys); //save to file
            }
        }

        public void Delete(Toy entity)
        {
            lock (Loki)
            {
                var collectionOfToys = DataUtils.LoadJsonFile().Cast<Toy>().ToList();
                collectionOfToys.RemoveAll(x => x.Id == entity.Id);
                DataUtils.SaveJsonFile(collectionOfToys); //save to file
            }
        }

        public Toy FindById(int Id)
        {
            var collectionOfToys = DataUtils.LoadJsonFile();
            try
            {
                Toy entityToReturn = collectionOfToys.First(x => x.Id == Id);
                return entityToReturn;
            }
            catch (InvalidOperationException){
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(Toy entity)
        {

            var ToyEntity = entity as Toy;
            var collectionOfToys = DataUtils.LoadJsonFile().Cast<Toy>().ToList();
            var objectToModify = collectionOfToys.First(x => x.Id == entity.Id) as Toy;
            lock (Loki)
            {
                if (objectToModify != null)
                {
                    objectToModify.Name = ToyEntity.Name;
                    objectToModify.Price = ToyEntity.Price;
                    objectToModify.Company = ToyEntity.Company;
                    objectToModify.AgeRestriction = ToyEntity.AgeRestriction;
                    objectToModify.Description = ToyEntity.Description;

                }

                DataUtils.SaveJsonFile(collectionOfToys); //save to file
            }
        }
    }
}
