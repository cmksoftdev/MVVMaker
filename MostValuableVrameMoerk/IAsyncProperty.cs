using System;
using System.Threading.Tasks;

namespace MostValuableVrameMoerk
{
    public interface IAsyncProperty
    {
        Action propertyChanges { get; set; }
    }
}