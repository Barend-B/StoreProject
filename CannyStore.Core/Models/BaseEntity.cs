using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannyStore.Core.Models
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public DateTimeOffset Created_At { get; set; }
        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Created_At = DateTime.Now;
        }
    }
}