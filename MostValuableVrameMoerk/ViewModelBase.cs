using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MostValuableVrameMoerk
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void SetPropertyChange(string name, IAsyncProperty property)
        {
            property.propertyChanges = () => { this.OnPropertyChanged(name); };
        }

        public void SetPropertyChangeForAll()
        {
            this.GetType()
                .GetProperties()
                .Where(x => x.PropertyType.IsGenericType &&
                    x.PropertyType.GetGenericTypeDefinition() == typeof(AsyncProperty<>))
                .ToList()
                .ForEach(x => SetPropertyChange(x.Name, (IAsyncProperty)x.GetValue(this)));
        }


        public class AsyncProperty<T> : IAsyncProperty
        {
            T property;
            Object lockObject = new Object();

            public AsyncProperty()
            {
            }

            public AsyncProperty(T t)
            {
                property = t;
            }

            public T Get
            {
                get => property;
                set
                {
                    property = value;
                    propertyChanges?.Invoke();
                }
            }
            
            public Action propertyChanges { get; set; }
            public async Task SetAsync(T value)
            {
                await Task.Run(() =>
                {
                    lock (lockObject)
                        property = value;
                });
                propertyChanges?.Invoke();
            }
        }
    }
}
