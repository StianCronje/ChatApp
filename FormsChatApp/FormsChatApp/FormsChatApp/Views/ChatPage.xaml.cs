using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsChatApp.Models;
using FormsChatApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FormsChatApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatPage : ContentPage
	{
		public ChatPage ()
		{
			InitializeComponent ();

		    ((ChatViewModel) BindingContext).OnMessageInsertAction += (messages) =>
		    {
		        var index = messages.Count-1;
		        ListView.ScrollTo(messages[index], ScrollToPosition.End, true);
		    };
		}

	    private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        ListView.SelectedItem = null;
        }

	    private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        ListView.SelectedItem = null;
        }
    }
}