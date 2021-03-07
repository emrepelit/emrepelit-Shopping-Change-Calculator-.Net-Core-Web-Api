using System.Collections.Generic;
using System.Linq;

namespace CalculateShoppingChange.Business.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool IsSuccess => Errors.Count == 0;
    }

}
