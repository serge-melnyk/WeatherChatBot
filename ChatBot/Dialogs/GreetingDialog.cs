using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ChatBot.Dialogs
{
    [Serializable]
    public class GreetingDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.UserData.Clear();
            await context.PostAsync("Hi I'm Enterprise Search Chat Bot");
            await Respond(context);

            context.Wait(MessageReceivedAsync);
        }

        private static async Task Respond(IDialogContext context)
        {
            string userName;
            context.UserData.TryGetValue<string>("Name", out userName);
            if (string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("What is your name?");
                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
                await context.PostAsync($"Hi {userName}. How can I help you today?");
            }
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            string userName;
            var getName = false;
            context.UserData.TryGetValue<string>("Name", out userName);
            context.UserData.TryGetValue<bool>("GetName", out getName);

            if (getName)
            {
                userName = message.Text;
                context.UserData.SetValue<string>("Name", userName);
                context.UserData.SetValue<bool>("GetName", false);
            }

            await Respond(context);
            context.Done(message);
        }
    }
}