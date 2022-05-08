using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWelcomeController : UIPanel
{
	public TextMeshProUGUI textError;
	public InputField inputUsername;
	public InputField inputPassword;
	public Button buttonRegister;
	public Button buttonConfirm;

	public void ButtonRegister_onClicked()
	{
		UIManager.Instance.ShowPanel<UIRegisterController>();
	}

	public void ButtonConfirm_onClicked()
	{
		string username = inputUsername.text;
		string password = inputPassword.text;

		if (string.IsNullOrEmpty(username))
		{
			textError.text = "Username is empty!";
			textError.gameObject.SetActive(true);
		}
		else if (string.IsNullOrEmpty(password))
		{
			textError.text = "Password is empty!";
			textError.gameObject.SetActive(true);
		}
		else if (!LibraryManager.Instance.CheckUserExist(username))
		{
			textError.text = "Username does not exist!";
			textError.gameObject.SetActive(true);
		}
		else
		{
			var successful = LibraryManager.Instance.Login(username, password);
			if (successful && !LibraryManager.Instance.IsAdmin)
			{
				UIManager.Instance.ShowPanel<UIBookListController>();
			}
			else if (successful && LibraryManager.Instance.IsAdmin)
			{
				UIManager.Instance.ShowPanel<UIBorrowerList>();
			}
			else
			{
				textError.text = "Password wrong!";
				textError.gameObject.SetActive(true);
			}
		}
	}

	public override void Hide()
	{
		Destroy(gameObject);
	}

	public override void Show()
	{
		textError.gameObject.SetActive(false);
		Debug.Log("Panel Welcome Shown!");
	}
}
