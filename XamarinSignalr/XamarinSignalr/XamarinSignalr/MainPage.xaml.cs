using System;
using Xamarin.Forms;

namespace XamarinSignalr
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        void JoinChat(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUsername.Text))
                App.Current.MainPage = new ChatPage(txtUsername.Text);
        }
	}
}