using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;

namespace ChatBot.Models
{
    public enum ItemOption
    {
        Employee,
        Candidate,
        Project,
        FormerEmployee
    }

    [Serializable]
    public class ItemQuery
    {
        public ItemOption? Item;
        public int? NumberOfItems;
        public DateTime? DueDate;
        public List<Services> Services;

        public static IForm<ItemQuery> BuildForm()
        {
            return new FormBuilder<ItemQuery>()
                   .Message("Welcome to the query options")
                   .Build();
        }
    }
}