using System;

namespace Base.Api.Helper
{
	public static class InitializeHelperWithObjectFactory
	{
		/// <summary>
		/// Initialize instance.
		/// </summary>
		public static T Initialize<T>(this T instance, Action<T> initializer)
		{
			initializer(instance);
			return instance;
		}
	}
}