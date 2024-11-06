using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalToolClsLib
{
	public static class Win32ApiHelper
	{
		public enum WindowsMessages
		{
			WM_INITDIALOG = 272,
			WM_COMMAND = 273,
			WM_SYSCOMMAND = 274,
			WM_TIMER = 275,
			WM_HSCROLL = 276,
			WM_VSCROLL = 277,
			WM_INITMENU = 278,
			WM_INITMENUPOPUP = 279,
			WM_SYSTIMER = 280,
			WM_MENUSELECT = 287,
			WM_MENUCHAR = 288,
			WM_ENTERIDLE = 289,
			WM_MENURBUTTONUP = 290,
			WM_MENUDRAG = 291,
			WM_MENUGETOBJECT = 292,
			WM_UNINITMENUPOPUP = 293,
			WM_MENUCOMMAND = 294,
			WM_CHANGEUISTATE = 295,
			WM_UPDATEUISTATE = 296,
			WM_QUERYUISTATE = 297,
			WM_USER = 1024,
			WM_APP = 32768
		}

		public enum WindowEdges
		{
			WMSZ_LEFT = 1,
			WMSZ_RIGHT,
			WMSZ_TOP,
			WMSZ_TOPLEFT,
			WMSZ_TOPRIGHT,
			WMSZ_BOTTOM,
			WMSZ_BOTTOMLEFT,
			WMSZ_BOTTOMRIGHT
		}

		public static byte LOBYTE(ushort value)
		{
			return (byte)(value & 0xFFu);
		}

		public static byte LOBYTE(IntPtr ptr)
		{
			return LOBYTE((ushort)ptr.ToInt32());
		}
	}
}
