using System;

namespace Smalldebts.Core.UI.Services
{
	public interface ISecureStorage
	{
		void Store(string key, string value);
		string Retrieve(string key);
		void Delete(string key);
		bool Contains(string key);
	}

	public static class StringByteExtensions
	{
	public static byte[] GetBytes(this string str)
	{
		byte[] bytes = new byte[str.Length * sizeof(char)];
		System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
		return bytes;
	}

	public static string GetString(this byte[] bytes)
	{
		char[] chars = new char[bytes.Length / sizeof(char)];
		System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
		return new string(chars);
	}
}
}

