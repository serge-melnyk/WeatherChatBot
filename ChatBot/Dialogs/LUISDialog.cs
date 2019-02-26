using System;
using System.Linq;
using System.Threading.Tasks;
using ChatBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace ChatBot.Dialogs
{
    [LuisModel("c1d86515-401e-41bf-a8c1-7c0b1a302687", "e545a931182a41bdbe1f18dcd9a848a1")]
    [Serializable]
    public class LUISDialog : LuisDialog<ItemQuery>
    {
        private readonly BuildFormDelegate<ItemQuery> ReserveItem;

        public LUISDialog(BuildFormDelegate<ItemQuery> getItem)
        {
            ReserveItem = getItem;
        }

        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry I don't know what you mean.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            context.Call(new GreetingDialog(), Callback);
        }

        [LuisIntent("CurrentWeather")]
        public async Task CurrentWeather(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Current weather here.");
            context.Wait(MessageReceived);
        }

        //[LuisIntent("ItemQuery")]
        //public async Task ItemQuery(IDialogContext context, LuisResult result)
        //{
        //    var queryState = new ItemQuery();
        //    var enrollmentForm = new FormDialog<ItemQuery>(queryState, ReserveItem, FormOptions.PromptInStart)
        //        .ContinueWith(AfterGreetingContinuation);
        //    context.Call(enrollmentForm, Callback);
        //}

        //private static async Task<IDialog<string>> AfterGreetingContinuation(IBotContext context, IAwaitable<object> item)
        //{
        //    var token = await item;
        //    var name = "User";

        //    context.UserData.TryGetValue<string>("Name", out name);
        //    return Chain.Return($"Thank you for using the chat bot: {name}").PostToUser();
        //}

        //[LuisIntent("QueryServices")]
        //public async Task QueryServices(IDialogContext context, LuisResult result)
        //{
        //    foreach (var entity in result.Entities.Where(e => e.Type == "Service"))
        //    {
        //        var value = entity.Entity.ToLower();
        //        if (value == "delivery" || value == "furniture assembly"
        //            || value == "materials selection support" || value == "design")
        //        {
        //            await context.PostAsync("Yes we do that!");
        //            context.Wait(MessageReceived);
        //            return;
        //        }
        //        //await context.PostAsync("I'm sorry we don't do that.");
        //        //context.Wait(MessageReceived);
        //        //return;
        //    }
        //    await context.PostAsync("I'm sorry we don't do that.");
        //    context.Wait(MessageReceived);
        //}

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }
    }
}