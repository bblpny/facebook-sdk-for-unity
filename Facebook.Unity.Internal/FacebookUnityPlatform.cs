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
	using System.Runtime.InteropServices;
	using UnityEngine;
	using System;

	[StructLayout(LayoutKind.Explicit,Size =4), Serializable]
    public struct FacebookUnityPlatform : System.IEquatable<FacebookUnityPlatform>, System.IComparable<FacebookUnityPlatform>
	{
		[FieldOffset(0), NonSerialized]
		private byte m_A;
		[FieldOffset(1), NonSerialized]
		private byte m_B;
		[FieldOffset(2), NonSerialized]
		private byte m_C;
		[FieldOffset(3), NonSerialized]
		private byte m_Options;
		[FieldOffset(0), SerializeField]
		private int m_Value;

		public char a { get { return (char)m_A; } }
		public char b { get { return m_A == 0 ? '\0' : (char)m_B; } }
		public char c { get { return m_A == 0 || m_B == 0 ? '\0' : (char)m_C; } }
		public sbyte length { get { return m_A == 0 ? (sbyte)0 : m_B == 0 ? (sbyte)1 : m_C == 0 ? (sbyte)2 : (sbyte)3; } }
		public char this[int index] { get { if (index < 0) throw new ArgumentException(); if (m_A == 0) return (char)m_A; else if (m_A == 0) return '\0'; else if (index == 1) return (char)m_B; else if (m_B == 0) return '\0'; else if (index == 2) return (char)m_C; else return '\0'; } }
		public Options options { get { return (Options)m_Options; } }

		public FacebookUnityPlatform(Options options, char a = '\0', char b = '\0', char c = '\0')
		{
			this.m_Value = 0;
			this.m_Options = (byte)options;
			this.m_C = (byte)((ushort)((a == '\0' || b == '\0') ? '\0' : c) & 255);
			this.m_B = (byte)((ushort)((a == '\0') ? '\0' : b) & 255);
			this.m_A = (byte)((ushort)a & 255);
		}

		public override int GetHashCode()
		{
			return 5*(new FacebookUnityPlatform { m_A = m_A, m_B = m_A == 0 ? (byte)0 : m_B, m_C = m_A == 0 || m_B == 0 ? (byte)0 : m_C }).m_Value;
		}
		public override bool Equals(object obj)
		{
			if(obj is FacebookUnityPlatform)
			{
				FacebookUnityPlatform other = (FacebookUnityPlatform)obj;
				if(m_A == other.m_A && (m_A == 0 || (m_B == other.m_B && (m_B == 0 || m_C == other.m_C))))
					return true;
			}
			return false;
		}
		public bool Equals(FacebookUnityPlatform other)
		{
			return m_A == other.m_A && (m_A == 0 || (m_B == other.m_B && (m_B == 0 || m_C == other.m_C)));
		}
		public int CompareTo(FacebookUnityPlatform other)
		{
			if (m_A != other.m_A)
				return m_A.CompareTo(other.m_A);
			if (m_A != 0)
			{
				if (m_B != other.m_B)
					return m_B.CompareTo(other.m_B);
				if (m_B != 0)
				{
					if (m_C != other.m_C)
						return m_C.CompareTo(other.m_C);
				}
			}
			return 0;
		}
		public static bool operator ==(FacebookUnityPlatform a, FacebookUnityPlatform b)
		{
			return a.m_A == b.m_A && (a.m_A == 0 || (a.m_B == b.m_B && (a.m_B == 0 || a.m_C == b.m_C)));
		}
		public static bool operator !=(FacebookUnityPlatform a, FacebookUnityPlatform b)
		{
			return a.m_A != b.m_A || (a.m_A != 0 && (a.m_B != b.m_B || (a.m_B != 0 && a.m_C != b.m_C)));
		}
		public static bool operator <(FacebookUnityPlatform a, FacebookUnityPlatform b)
		{
			return a.m_A < b.m_A || (a.m_A == b.m_A && a.m_A != 0 && (a.m_B < b.m_B || (a.m_B == b.m_B && a.m_C < b.m_C && a.m_B != 0)));
		}
		public static bool operator >(FacebookUnityPlatform a, FacebookUnityPlatform b)
		{
			return a.m_A > b.m_A || (a.m_A == b.m_A && a.m_A != 0 && (a.m_B > b.m_B || (a.m_B == b.m_B && a.m_C > b.m_C && a.m_B != 0)));
		}
		public static bool operator <=(FacebookUnityPlatform a, FacebookUnityPlatform b)
		{
			return a.m_A < b.m_A || (a.m_A == b.m_A && (a.m_A == 0 || (a.m_B < b.m_B || (a.m_B == b.m_B && (a.m_B == 0 || a.m_C <= b.m_C)))));
		}
		public static bool operator >=(FacebookUnityPlatform a, FacebookUnityPlatform b)
		{
			return a.m_A > b.m_A || (a.m_A == b.m_A && (a.m_A == 0 || (a.m_B > b.m_B || (a.m_B == b.m_B && (a.m_B == 0 || a.m_C >= b.m_C)))));
		}
		public override string ToString()
		{
			if (m_A == 0)
				return string.Empty;
			if (m_B == 0)
				return ((char)m_A).ToString();
			if (m_C == 0)
				return new string(new char[] { (char)m_A, (char)m_B, },0,2);
			return new string(new char[] { (char)m_A, (char)m_B, (char)m_C, },0,3);
		}
		[System.Flags]
		public enum Options : byte
		{
			Known = 1,
			IsWeb = 2,
			IsMobile = 4,
			IsArcade = 8,
			IsEditor = 16,
		}

		public static readonly FacebookUnityPlatform
			Unknown = default(FacebookUnityPlatform),
			Android = new FacebookUnityPlatform(Options.Known | Options.IsMobile, 'a', 'p', 'k'),
			IOS = new FacebookUnityPlatform(Options.Known | Options.IsMobile, 'i', 'p', 'a'),
			WebGL = new FacebookUnityPlatform(Options.Known | Options.IsWeb, 'h', 't', 'm'),
			WebPlayer = new FacebookUnityPlatform(Options.Known | Options.IsWeb, 'o', 'l', 'd'),
			Arcade = new FacebookUnityPlatform(Options.Known | Options.IsArcade, 'e', 'x', 'e');
    }
}
