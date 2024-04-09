namespace IMDB_Api.Exemples
{
    public class ServiceLocator
    {
        private Dictionary<Type, object> Services { get; set; }

        public ServiceLocator()
        {
            Services = new Dictionary<Type, object>();
        }

        public void Add(Type T, object value)
        {
            Services.Add(T, value);
        }

        public object Get(Type T)
        {
            return Services[T];
        }
    }

}
