//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Курсач.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        public int id_order { get; set; }
        public int id_client { get; set; }
        public int id_details { get; set; }
        public Nullable<System.DateTime> registration_date { get; set; }
        public Nullable<int> id_review { get; set; }
    
        public virtual CarDetails CarDetails { get; set; }
        public virtual Klients Klients { get; set; }
        public virtual Reviews Reviews { get; set; }
    }
}
