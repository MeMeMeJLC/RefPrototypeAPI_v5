//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RefPrototypeAPI_v5.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class game
    {
        public int game_id { get; set; }
        public Nullable<int> location_id { get; set; }
        public Nullable<int> referee_id { get; set; }
        public Nullable<System.DateTime> game_date { get; set; }
        public Nullable<System.TimeSpan> game_time { get; set; }
        public Nullable<int> team_one_id { get; set; }
        public Nullable<int> team_two_id { get; set; }
    }
}