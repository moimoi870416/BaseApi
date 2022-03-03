using System.Linq;
using System.Text.RegularExpressions;

namespace Base.Api.Helper
{
	public static class AccountInformationValidationHelper
	{

		private const int MaxUserNameLength = 40;
		private const int MinUserNameLength = 6;
		private const int MaxPasswordLength = 20;
		private const int MinPasswordLength = 6;
		private static readonly Regex UserNameRegex = new Regex(@"^[0-9A-Za-z_]+$");
		private static readonly Regex PasswordRegex = new Regex(@"^[0-9A-Za-z]+$");

		public static bool IsValidApiCustomerUsername(string username)
		{
			return 
				username.Length >= MinUserNameLength 
				&& username.Length <= MaxUserNameLength 
				&& UserNameRegex.IsMatch(username);
		}

		public static bool IsValidApiCustomerPassword(string password)
		{
			return 
				password.Length >= MinPasswordLength 
				&& password.Length <= MaxPasswordLength 
				&& PasswordRegex.IsMatch(password);
		}

		public static bool IsValidPlayerUsername(this string username)
		{
			if (string.IsNullOrEmpty(username)) return false;
			var isInvalidLength = username.Length < 6 || username.Length > 40;
			var hasLetter = username.Any(char.IsLetter);
			return !string.IsNullOrEmpty(username) 
			       && !isInvalidLength 
			       && hasLetter 
			       && UserNameRegex.IsMatch(username);
		}

		public static bool IsValidAdminUsername(this string username)
		{
			if (string.IsNullOrEmpty(username)) return false;
			var isInvalidLength = username.Length < 3 || username.Length > 20;
			var hasLetter = username.Any(char.IsLetter);
			return !isInvalidLength 
			       && hasLetter 
			       && UserNameRegex.IsMatch(username);
		}

		public static bool IsValidPassword(this string password)
		{
			if (string.IsNullOrEmpty(password))
				return false;
			const string specialChars = " '\"<>";
			var hasSpecialChars = password.IndexOfAny(specialChars.ToCharArray()) > -1;
			var isInvalidLength = password.Length < 6 || password.Length > 20;
			var hasLetter = password.Any(char.IsLetter);
			var hasDigit = password.Any(char.IsDigit);
			return !hasSpecialChars && !isInvalidLength && hasLetter && hasDigit;
		}

		public static bool IsValidPaymentPassword(this string password)
		{
			if (string.IsNullOrEmpty(password))
				return false;
			var isInvalidLength = password.Length == 4;
			var isOnlyDigit = password.All(char.IsDigit);
			return isInvalidLength && isOnlyDigit;
		}
	}
}