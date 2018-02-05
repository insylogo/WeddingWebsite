using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingWebsite.Models
{
    public class RSVP
    {
        [Key]        
        public virtual int Id { get; set; } = 0;

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Message { get; set; }

        [DisplayName("How many guests?")]
        public virtual int NumberOfPeople { get; set; }
        
        [DisplayName("Food Preference")]
        public virtual FoodPreference FoodPreference { get; set; }
    }

    public enum FoodPreference
    {
        None = 0,
        Vegetarian,
        Vegan,
        Other

    }
}