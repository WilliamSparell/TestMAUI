namespace TestMAUI;

public partial class MainPage : ContentPage
{
	string translatedNumber;
	public MainPage()
	{
		InitializeComponent();
	}

    private void OnTranslate(object sender, EventArgs e)
    {
        string enteredNumber = PhoneNumberText.Text;
        translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);
        if (!string.IsNullOrWhiteSpace(translatedNumber))
        {
            callButton.IsEnabled = true;
            callButton.Text = "Call " + translatedNumber;
        }
        else
        {
            callButton.IsEnabled = false;
            callButton.Text = "Call";
        }
    }
    
    async void OnCall(object sender, System.EventArgs e)
    {
        if (await this.DisplayAlert(
            "Dial a Number",
            "Would you like to call" + translatedNumber + "?",
            "Yes",
            "No"
            ))
        {
            try
            {
                if (PhoneDialer.Default.IsSupported)
                    PhoneDialer.Default.Open(translatedNumber);
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Unable to dial", "Phone dialing is not supported on this device.", "OK");
            }
            catch (Exception)
            {
                await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
            }
        }
    }

}