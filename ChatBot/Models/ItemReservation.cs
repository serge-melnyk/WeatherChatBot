using System;
using System.Collections.Generic;
using Microsoft.Bot.Builder.FormFlow;

namespace ChatBot.Models
{
    public enum ItemOptions
    {
        KitchenFurniture,
        Sofa,
        Shelfs,
        Accessories
    }

    public enum Services
    {
        Delivery,
        FurnitureAssembly,
        MaterialsSelectionSupport,
        Design
    }

    [Serializable]
    public class ItemReservation
    {
        public ItemOptions? Item;
        public int? NumberOfItems;
        public DateTime? DueDate;
        public List<Services> Services;

        public static IForm<ItemReservation> BuildForm()
        {
            return new FormBuilder<ItemReservation>()
                   .Message("Welcome to the item reservation")
                   .Build();
        }
    }
}