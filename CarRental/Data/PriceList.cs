//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRental.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PriceList
    {
        public int Id { get; set; }
        public int VehiclesId { get; set; }
        public decimal Price { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    }
}
