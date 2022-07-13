using Model;
using Repository.Interface;
using System.Xml.Serialization;

namespace Repository.Abstract
{
    public abstract class Repository<T> : IRepository<T>
        where T : ITerm
    {
        private readonly string _fileName;
        private List<T> _entities = new();

        protected Repository()
        {
            _fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{this.GetType().Name.ToLower()}.xml");
            Load(); 
        }

        private void Load()
        {
            if (File.Exists(_fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                using (TextReader reader = new StreamReader(_fileName))
                {
                    var items = serializer.Deserialize(reader) as List<T>;
                    _entities = items ?? new List<T>();
                }
            }
        }
        private void Save()
        {
            Console.WriteLine("Salvando...");
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (TextWriter writer = new StreamWriter(_fileName))
            {
                serializer.Serialize(writer, _entities.OrderBy(e => e.Word).ToList());
            }
            Console.WriteLine("Salvo.");
        }
        public void Add(T entity)
        {
            _entities.Add(entity);
            Save();
        }

        public T Get(string word)
        {
            return _entities.FirstOrDefault(x => x.Word.ToLower() == word.ToLower());
        }
        public List<T> FindAll(string word)
        {
            return _entities.FindAll(x => x.Word.ToLower().Contains(word.ToLower())).OrderBy(e => e.Word).ToList();
        }

        public List<T> GetAll()
        {
            return _entities;
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public void Update(T entity)
        {
            Save();
        }
    }
}
