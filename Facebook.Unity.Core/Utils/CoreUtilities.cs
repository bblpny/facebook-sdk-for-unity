/**
 * Copyright (c) 2014-present, Facebook, Inc. All rights reserved.
 *
 * You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
 * copy, modify, and distribute this software in source code or binary form for use
 * in connection with the web services and APIs provided by Facebook.
 *
 * As with any software that integrates with the Facebook platform, your use of
 * this software is subject to the Facebook Developer Principles and Policies
 * [http://developers.facebook.com/policy/]. This copyright notice shall be
 * included in all copies or substantial portions of the software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

namespace Facebook.Unity.Internal
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Text;
	public static class CoreUtilities
	{
		private const string WarningMissingParameter = "Did not find expected value '{0}' in dictionary";
		private static Dictionary<string, string> commandLineArguments;
		/// <summary>
		/// exposes the set accessor to AccessToken.CurrentAccessToken to Internal
		/// </summary>
		public static AccessToken CurrentAccessToken { get { return AccessToken.CurrentAccessToken; } set { AccessToken.CurrentAccessToken = value; } }

		public static AccessToken CreateAccessToken(
			string tokenString,
			string userId,
			DateTime expirationTime,
			IEnumerable<string> permissions,
			DateTime? lastRefresh)
		{
			return new AccessToken(tokenString, userId, expirationTime, permissions, lastRefresh);
		}

		public static Dictionary<string, string> CommandLineArguments
		{
			get
			{
				if (commandLineArguments != null)
				{
					return commandLineArguments;
				}

				var localCommandLineArguments = new Dictionary<string, string>();
				var arguments = Environment.GetCommandLineArgs();
				for (int i = 0; i < arguments.Length; i++)
				{
					if (arguments[i].StartsWith("/") || arguments[i].StartsWith("-"))
					{
						var value = i + 1 < arguments.Length ? arguments[i + 1] : null;
						localCommandLineArguments.Add(arguments[i], value);
					}
				}

				commandLineArguments = localCommandLineArguments;
				return commandLineArguments;
			}
		}

		public static bool TryGetValue<T>(
			this IDictionary<string, object> dictionary,
			string key,
			out T value)
		{
			object resultObj;
			if (dictionary.TryGetValue(key, out resultObj) && resultObj is T)
			{
				value = (T)resultObj;
				return true;
			}

			value = default(T);
			return false;
		}

		public static long TotalSeconds(this DateTime dateTime)
		{
			TimeSpan t = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			long secondsSinceEpoch = (long)t.TotalSeconds;
			return secondsSinceEpoch;
		}

		public static string ToCommaSeparateList(this IEnumerable<string> list)
		{
			string[] array;
			if (list == null)
			{
				return string.Empty;
			}
			array = list as string[];
			if (null == array)
			{
				if (list is List<string>)
					array = ((List<string>)list).ToArray();
				else if(list is ICollection<string>)
				{
					array = new string[((ICollection<string>)list).Count];
					((ICollection<string>)list).CopyTo(array, 0);
				}
				else
				{
					array = new List<string>(list).ToArray();
				}
			}
			return string.Join(",", array);
		}

		public static string AbsoluteUrlOrEmptyString(this Uri uri)
		{
			if (uri == null)
			{
				return string.Empty;
			}

			return uri.AbsoluteUri;
		}

		public static string GetUserAgent(string productName, string productVersion)
		{
			return string.Format(
				CultureInfo.InvariantCulture,
				"{0}/{1}",
				productName,
				productVersion);
		}

		public static string ToJson(this IDictionary<string, object> dictionary)
		{
			return MiniJSON.Json.Serialize(dictionary);
		}

		public static void AddAllKVPFrom<T1, T2>(this IDictionary<T1, T2> dest, IDictionary<T1, T2> source)
		{
			foreach (T1 key in source.Keys)
			{
				dest[key] = source[key];
			}
		}
		
		public static string ToStringNullOk(this object obj)
		{
			if (obj == null)
			{
				return "null";
			}

			return obj.ToString();
		}

		// Use this instead of reflection to avoid crashing at
		// runtime due to Unity's stripping
		public static string FormatToString(
			string baseString,
			string className,
			IDictionary<string, string> propertiesAndValues)
		{
			StringBuilder sb = new StringBuilder();
			if (baseString != null)
			{
				sb.Append(baseString);
			}

			sb.AppendFormat("\n{0}:", className);
			foreach (var kvp in propertiesAndValues)
			{
				string value = kvp.Value != null ? kvp.Value : "null";
				sb.AppendFormat("\n\t{0}: {1}", kvp.Key, value);
			}

			return sb.ToString();
		}

		private static DateTime FromTimestamp(int timestamp)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp);
		}
	}
}
