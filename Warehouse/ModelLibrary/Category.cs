using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Annotations;

namespace ModelLibrary
{
    public class Category : INotifyPropertyChanged
    {
        [Key]
        [Required]
        public int categoryId { get; set; }
        [Required]
        public string name { get; set; }
        public virtual List<Customer> hasDiscount { get; set; }
        public virtual List<Product> hasProducts { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public string CategoryName => $"{name}";
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
