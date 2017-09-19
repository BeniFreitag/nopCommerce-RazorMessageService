using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using RazorEngine;
using RazorEngine.Templating;

namespace ToSic.Nop.Plugins.RazorMessageService.Services
{
	internal class RazorTemplateParser
	{
		/// <summary>
		/// work arounf MD5 has for razorengine caching.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		private static string GetMd5Hash(string input)
		{
			var md5 = MD5.Create();
			var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			var hash = md5.ComputeHash(inputBytes);
			var sb = new StringBuilder();
			foreach (byte t in hash)
			{
				sb.Append(t.ToString("X2"));
			}

			return sb.ToString();
		}

		/// <summary>
		/// Parse text with Razor and handle Template Exception
		/// </summary>
		public static string ParseSafe(int templateId, string text, object model, out bool success)
		{
			string result;
			try
			{
				var key = "MailTemplate" + templateId + GetMd5Hash(text);

				result = Engine.Razor.RunCompile(text, key, model: model);

				success = true;
			}
			catch (TemplateCompilationException ex)
			{
				result = "TemplateCompilationException: ";
				ex.Errors.ToList().ForEach(p => result += p.ErrorText);
				success = false;
			}
			catch (Exception ex)
			{
				result = "RazorParseException: " + ex.Message + ex.StackTrace;
				success = false;
			}

			return result;
		}
	}
}
