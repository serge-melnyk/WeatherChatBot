using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChatBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace ChatBot.Dialogs
{
    public class FurnitureServiceBotDialog
    {
        public static readonly IDialog<string> Dialog = Chain.PostToChain()
            .Select(msg => msg.Text)
            .Switch(
                new RegexCase<IDialog<string>>(new Regex("^hi", RegexOptions.IgnoreCase),
                    (context, text) 
                    => new GreetingDialog().ContinueWith(AfterGreetingContinuation)),
                new DefaultCase<string, IDialog<string>>((context, text) 
                    => FormDialog.FromForm(ItemReservation.BuildForm, FormOptions.PromptInStart).ContinueWith(AfterGreetingContinuation)))
            .Unwrap()
            .PostToUser();


        private static async Task<IDialog<string>> AfterGreetingContinuation(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;
            var name = "User";
            context.UserData.TryGetValue<string>("Name", out name);
            return Chain.Return($"Thank you for using the chat bot: {name}");
        }
    }
}