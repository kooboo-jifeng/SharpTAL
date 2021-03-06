﻿//
// RepeatItem.cs
//
// Author:
//   Roman Lacko (backup.rlacko@gmail.com)
//
// Copyright (c) 2010 - 2014 Roman Lacko
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Collections;
using System.Collections.Generic;

namespace SharpTAL
{
	public class RepeatItem : ITalesIterator
	{
		private int _position;

		public RepeatItem(IEnumerable sequence)
		{
			_position = -1;

			var c = sequence as ICollection;
			if (c != null)
				length = c.Count;
			else
			{
				var s = sequence as string;
				if (s != null)
					length = s.Length;
				else
					length = 0;
			}
		}

		#region ITalesIterator implementation

		public void next(bool isLast)
		{
			_position++;
			end = isLast;
		}

		public int length { get; protected set; }

		public int index { get { return _position; } }

		public int number { get { return _position + 1; } }

		public bool even { get { return index % 2 == 0; } }

		public bool odd { get { return index % 2 == 1; } }

		public bool start { get { return index == 0; } }

		public bool end { get; protected set; }

		public string letter
		{
			get
			{
				string result = "";
				int nextCol = _position;
				if (nextCol == 0)
					return "a";
				while (nextCol > 0)
				{
					int tmp = nextCol;
					nextCol = tmp / 26;
					int thisCol = tmp % 26;
					result = ((char)(('a') + thisCol)) + result;
				}
				return result;
			}
		}

		public string Letter { get { return letter.ToUpper(); } }

		public string roman
		{
			get
			{
				var romanNumeralList = new Dictionary<string, int>
                    {
                        { "m", 1000 }
                        ,{ "cm", 900 }
                        ,{ "d", 500 }
                        ,{ "cd", 400 }
                        ,{ "c", 100 }
                        ,{ "xc", 90 }
                        ,{ "l", 50 }
                        ,{ "xl", 40 }
                        ,{ "x", 10 }
                        ,{ "ix", 9 }
                        ,{ "v", 5 }
                        ,{ "iv", 4 }
                        ,{ "i", 1 }
                    };
				if (_position > 3999)
					return " ";
				int num = _position + 1;
				string result = "";
				foreach (KeyValuePair<string, int> kv in romanNumeralList)
				{
					while (num >= kv.Value)
					{
						result += kv.Key;
						num -= kv.Value;
					}
				}
				return result;
			}
		}

		public string Roman { get { return roman.ToUpper(); } }

		#endregion
	}
}
