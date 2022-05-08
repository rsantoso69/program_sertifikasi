using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRegisterController : UIPanel
{
	public TextMeshProUGUI textError;
	public InputField inputUsername;
	public InputField inputPassword;
	public InputField inputReEnterPassword;
	public Button buttonRegister;

	public void ButtonRegister_onClicked()
	{
		string username = inputUsername.text;
		string password = inputPassword.text;
		string reEnterPassword = inputReEnterPassword.text;

		if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(reEnterPassword))
		{
			textError.text = "A field is empty!";
			textError.gameObject.SetActive(true);
		}
		else if (LibraryManager.Instance.CheckUserExist(username))
		{
			textError.text = "User exist!";
			textError.gameObject.SetActive(true);
		}
		else if (password != reEnterPassword)
		{
			textError.text = "Password does not match!";
			textError.gameObject.SetActive(true);
		}
		else
		{
			Member member = new Member()
			{
				name = username,
				username = username,
				password = password,
			};
			LibraryManager.Instance.AddUser(member);
			UIManager.Instance.ShowPanel<UIWelcomeController>();
		}
	}

	public void ButtonBack_onClicked()
	{
		UIManager.Instance.ShowPanel<UIWelcomeController>();
	}

	public override void Hide()
	{
		Destroy(gameObject);
	}

	public override void Show()
	{
		textError.gameObject.SetActive(false);
		Debug.Log("Panel Register Shown!");
	}
}
