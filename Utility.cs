using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace stockassistant
{
    public class Utility
    {
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendTxtMessage(int hWnd, int Msg, int wParam, char[] lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = System.Runtime.InteropServices.CharSet.Auto)] //        
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam); 

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(int hWnd, int msg, int wParam, IntPtr lParam);

        [DllImport("User32.Dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam); 
        
        private const uint WM_GETTEXTLENGTH = 0x000E;        
        private const uint WM_SETTEXT = 0x000C; 
        private const uint WM_GETTEXT = 0x000D; 
        private const uint BM_CLICK = 0x00F5; 
        private const uint WM_CLOSE = 0x0010;

        enum GetWindow_Cmd : uint { GW_HWNDFIRST = 0, GW_HWNDLAST = 1, GW_HWNDNEXT = 2, GW_HWNDPREV = 3, GW_OWNER = 4, GW_CHILD = 5, GW_ENABLEDPOPUP = 6 }

        private const int TV_FIRST = 0x1100;
        private const int TVGN_ROOT = 0x0;
        private const int TVGN_NEXT = 0x1;
        private const int TVGN_CHILD = 0x4;
        private const int TVGN_FIRSTVISIBLE = 0x5;
        private const int TVGN_NEXTVISIBLE = 0x6;
        private const int TVGN_CARET = 0x9;
        private const int TVM_SELECTITEM = (TV_FIRST + 11);
        private const int TVM_GETNEXTITEM = (TV_FIRST + 10);
        private const int TVM_GETITEM = (TV_FIRST + 12);
        private const int TV_SELECTITEM = 0x110B;
        private const int TVM_GETITEMRECT = (TV_FIRST + 4);

        private const int LVM_GETITEMRECT = (0x1000 + 14);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("kernel32 ", CharSet = CharSet.Unicode)]
        public static extern int CopyMemory(RECT Destination, IntPtr Source, int Length);

        [DllImport("kernel32.dll")]
        public static extern int OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_READ = 0x0010;
        const int PROCESS_VM_WRITE = 0x0020;

        [DllImport("kernel32.dll")]
        public static extern int VirtualAllocEx(IntPtr hwnd, Int32 lpaddress, int size, int type, Int32 tect);
        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead); 
        [DllImport("kernel32.dll")]
        public static extern int GetProcAddress(int hwnd, string lpname);
        [DllImport("kernel32.dll")]
        public static extern int GetModuleHandleA(string name);
        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hwnd, int attrib, int size, int address, int par, int flags, int threadid);
        [DllImport("kernel32.dll")]
        public static extern Int32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        [DllImport("kernel32.dll")]
        public static extern Boolean VirtualFree(IntPtr lpAddress, Int32 dwSize, Int32 dwFreeType);

        const int MEM_COMMIT = 0x1000;
        const int MEM_RESERVE = 0x2000;
        const int MEM_RELEASE = 0x8000;
        const int PAGE_READWRITE = 0x04;

        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(uint hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, uint nSize, ref uint lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead); 

        [DllImport("kernel32.dll")]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);

        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int MK_LBUTTON = 0x1;

        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, out  RECT lpRect); 

        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x,int y);

        [DllImport("user32.dll")]
        static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32")]
        public extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);

        [DllImport("User32")]
        public extern static bool GetCursorPos(out POINT p);

        [DllImport("User32")]
        public extern static int ShowCursor(bool bShow);

        [Flags]
        public enum MouseEventFlags
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            Wheel = 0x0800,
            Absolute = 0x8000
        }

        public enum TodayStatus
        {
            Normal,
            BuyOverFlow,
            SellOverFlow,
            Updated
        }

        public enum OrderStatus
        {
            Buy,
            Sell,
            All,
            Repeated
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        private struct LVITEM
        {      
            public int mask;
            public int iItem;
            public int iSubItem;
            public int state;
            public int stateMask;
            public IntPtr pszText; // string
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
            public int iIndent;
            public int iGroupId;
            public int cColumns;
            public IntPtr puColumns;  
        }

        private const int LVIF_TEXT = 0x0001;

        private const int LVM_FIRST = 0x1000;
        private const int LVM_GETITEMCOUNT = LVM_FIRST + 4;
        private const int LVM_GETITEMW = LVM_FIRST + 75;

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        static extern IntPtr GetWindowLong64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        static extern IntPtr SetWindowLong32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        static extern IntPtr SetWindowLong64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        public enum WindowLongFlags : int
        {
            GWL_EXSTYLE = -20,
            GWLP_HINSTANCE = -6,
            GWLP_HWNDPARENT = -8,
            GWL_ID = -12,
            GWL_STYLE = -16,
            GWL_USERDATA = -21,
            GWL_WNDPROC = -4,
            DWLP_USER = 0x8,
            DWLP_MSGRESULT = 0x0,
            DWLP_DLGPROC = 0x4
        }

        public static IntPtr MyGetWindowLongPtr(IntPtr hWnd, WindowLongFlags nIndex)
        {
            if (IntPtr.Size == 8)
                return GetWindowLong64(hWnd, (int)nIndex);
            else
                return GetWindowLong32(hWnd, (int)nIndex);
        }

        public static IntPtr MySetWindowLongPtr(IntPtr hWnd, WindowLongFlags nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLong64(hWnd, (int)nIndex, dwNewLong);
            else
                return SetWindowLong32(hWnd, (int)nIndex, dwNewLong);
        }

        public static void CloseWindow(IntPtr hwnd)
        {
            SetForegroundWindow(hwnd);
            PostMessage(hwnd, WM_CLOSE, 0, 0);
        }

        public static IntPtr GetWindow()
        {
            try
            {
                string maincaption = ConfigurationSettings.AppSettings["mainname"].ToString();
                string logincaption = ConfigurationSettings.AppSettings["loginname"].ToString();
                //find window
                IntPtr curhandle = FindWindow(null, maincaption);
                if (curhandle == IntPtr.Zero)
                {
                    //open window
                    curhandle = FindWindow(null, logincaption);
                    if (curhandle == IntPtr.Zero)
                    {
                        OpenWindow();
                        curhandle = FindWindow(null, logincaption);
                    }
                    LogWindow(curhandle);
                    System.Threading.Thread.Sleep(3000);
                    curhandle = FindWindow(null, maincaption);
                }
                return curhandle;
            }
            catch
            {
                return IntPtr.Zero;
            }
        }

        private static IntPtr GetHwnd(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName)
        {
            IntPtr handle = FindWindowEx(hwndParent, hwndChildAfter, lpClassName, lpWindowName);
            if (handle != IntPtr.Zero)
            {
                return handle;
            }
            else
            {
                throw new Exception("failed to get handle");
            }
        }

        private static void LogWindow(IntPtr curhandle)
        {
            //account
            IntPtr tabhandle = GetHwnd(curhandle, IntPtr.Zero, "ComboBox", string.Empty);
            tabhandle = GetHwnd(curhandle, tabhandle, "ComboBox", string.Empty);
            tabhandle = GetHwnd(curhandle, tabhandle, "ComboBox", string.Empty);
            tabhandle = GetHwnd(tabhandle, IntPtr.Zero, "Edit", string.Empty);
            string strTxt = ConfigurationManager.AppSettings["account"].ToString();
            SendTxtMessage((int)tabhandle, 12, 0, strTxt.ToCharArray());

            //pwd
            tabhandle = GetHwnd(curhandle, IntPtr.Zero, "AfxWnd42", string.Empty);
            strTxt = Decrypt(ConfigurationManager.AppSettings["password"].ToString());
            SendTxtMessage((int)tabhandle, 12, 0, strTxt.ToCharArray());

            //pwd 1
            tabhandle = GetHwnd(curhandle, IntPtr.Zero, "Static", "保护帐号");
            tabhandle = GetHwnd(curhandle, tabhandle, "AfxWnd42", string.Empty);
            SendTxtMessage((int)tabhandle, 12, 0, strTxt.ToCharArray());


            tabhandle = GetHwnd(curhandle, IntPtr.Zero, "Button", "确定");
            PostMessage(tabhandle, BM_CLICK, 0, 0); //Send left click(0x00f5) to OK button 
        }

        private static void OpenWindow()
        {
            string filename = ConfigurationSettings.AppSettings["path"].ToString();
            if (!File.Exists(filename))
            {
                Log("OpenWindow failed:" + filename + " is not exists!");
            }
            Process proc = Process.Start(filename);
            System.Threading.Thread.Sleep(2000);
        }

        private static void Unlockscreen(IntPtr handle)
        {
            try
            {
                IntPtr lockdialog = GetHwnd(handle, IntPtr.Zero, "#32770", string.Empty);
                IntPtr handlefowpwd = GetHwnd(lockdialog, IntPtr.Zero, "AfxWnd42", string.Empty);
                string strTxt = "759408";
                SendTxtMessage((int)handlefowpwd, 12, 0, strTxt.ToCharArray());
                IntPtr btnOK = GetHwnd(lockdialog, IntPtr.Zero, "Button", "确定");
                PostMessage(btnOK, BM_CLICK, 0, 0); //Send left click(0x00f5) to OK button 
                System.Threading.Thread.Sleep(2000);
            }
            catch { }
        }

        private static IntPtr GetTreeObject(IntPtr hwnd)
        {
            IntPtr handle = GetHwnd(hwnd, IntPtr.Zero, "AfxFrameOrView42", string.Empty);
            handle = GetHwnd(handle, IntPtr.Zero, "#32770", "通达信网上交易V6");
            Unlockscreen(handle);
            handle = GetHwnd(handle, IntPtr.Zero, "Afx:10000000:3:10011:1900010:10003", string.Empty);
            handle = GetHwnd(handle, IntPtr.Zero, "AfxMDIFrame42", string.Empty);
            handle = GetHwnd(handle, IntPtr.Zero, "AfxWnd42", string.Empty);
            handle = GetHwnd(handle, IntPtr.Zero, "Afx:10000000:0:10011:0:0", string.Empty);
            handle = GetHwnd(handle, IntPtr.Zero, "SysTreeView32", string.Empty);
            return handle;
        }

        public static IntPtr GetSellDialog(IntPtr hwnd)
        {
            IntPtr handle = GetHwnd(hwnd, IntPtr.Zero, "AfxFrameOrView42", string.Empty);
            handle = GetHwnd(handle, IntPtr.Zero, "#32770", "通达信网上交易V6");
            Unlockscreen(handle);
            handle = GetHwnd(handle, IntPtr.Zero, "Afx:10000000:3:10011:1900010:10003", string.Empty);
            handle = GetHwnd(handle, IntPtr.Zero, "AfxMDIFrame42", string.Empty);
            IntPtr childhand = GetHwnd(handle, IntPtr.Zero, "AfxWnd42", string.Empty);
            handle = GetHwnd(handle, childhand, "AfxWnd42", string.Empty);
            childhand = GetHwnd(handle, IntPtr.Zero, "#32770", string.Empty);
            IntPtr StyleWnd = MyGetWindowLongPtr(childhand, WindowLongFlags.GWL_STYLE);
            while (childhand != IntPtr.Zero)
            {
                if ((int)StyleWnd == 1342177348)
                {
                    if (FindWindowEx(childhand, IntPtr.Zero, "Static", "卖出价格:") != IntPtr.Zero)
                    {
                        if (FindWindowEx(childhand, IntPtr.Zero, "Button", "卖出下单") != IntPtr.Zero)
                        {
                            return childhand;
                        }
                    }
                }
                childhand = FindWindowEx(handle, childhand, "#32770", string.Empty);
                if (childhand != IntPtr.Zero)
                {
                    StyleWnd = MyGetWindowLongPtr(childhand, WindowLongFlags.GWL_STYLE);
                }
            }
            return childhand;
        }

        private static IntPtr GetOrder(IntPtr hwnd, out IntPtr dialog)
        {
            dialog = IntPtr.Zero;
            IntPtr handle = GetHwnd(hwnd, IntPtr.Zero, "AfxFrameOrView42", string.Empty);
            handle = GetHwnd(handle, IntPtr.Zero, "#32770", "通达信网上交易V6");
            Unlockscreen(handle);
            handle = GetHwnd(handle, IntPtr.Zero, "Afx:10000000:3:10011:1900010:10003", string.Empty);
            handle = GetHwnd(handle, IntPtr.Zero, "AfxMDIFrame42", string.Empty);
            IntPtr childhand = GetHwnd(handle, IntPtr.Zero, "AfxWnd42", string.Empty);
            handle = GetHwnd(handle, childhand, "AfxWnd42", string.Empty);
            childhand = GetHwnd(handle, IntPtr.Zero, "#32770", string.Empty);
            IntPtr StyleWnd = MyGetWindowLongPtr(childhand,WindowLongFlags.GWL_STYLE);
            IntPtr dlghwnd = IntPtr.Zero;
            while (childhand != IntPtr.Zero)
            {
                //0x40000044 for hide, 0x50000044 for show
                if ((int)StyleWnd == 1342177348)
                {
                    dlghwnd = FindWindowEx(childhand, IntPtr.Zero, "SysListView32", "List1");
                    if (dlghwnd != IntPtr.Zero)
                    {
                        if (FindWindowEx(childhand, IntPtr.Zero, "Button", "撤 单") != IntPtr.Zero)
                        {
                            //return listview handle.
                            IntPtr btnrefresh = FindWindowEx(childhand, IntPtr.Zero, "Button", "刷 新");
                            if (btnrefresh != IntPtr.Zero)
                            {
                                PostMessage(btnrefresh, BM_CLICK, 0, 0); //Send left click(0x00f5) to OK button 
                                System.Threading.Thread.Sleep(5000);
                            }
                            dialog = childhand;
                            return dlghwnd;
                        }
                    }
                }
                childhand = FindWindowEx(handle, childhand, "#32770", string.Empty);
                if (childhand != IntPtr.Zero)
                {
                    StyleWnd = MyGetWindowLongPtr(childhand, WindowLongFlags.GWL_STYLE);
                }
            }
            return childhand;
        }

        private static IntPtr GetToday(IntPtr hwnd)
        {
            IntPtr handle = GetHwnd(hwnd, IntPtr.Zero, "AfxFrameOrView42", string.Empty);
            handle = GetHwnd(handle, IntPtr.Zero, "#32770", "通达信网上交易V6");
            Unlockscreen(handle);
            handle = GetHwnd(handle, IntPtr.Zero, "Afx:10000000:3:10011:1900010:10003", string.Empty);
            handle = GetHwnd(handle, IntPtr.Zero, "AfxMDIFrame42", string.Empty);
            IntPtr childhand = GetHwnd(handle, IntPtr.Zero, "AfxWnd42", string.Empty);
            handle = GetHwnd(handle, childhand, "AfxWnd42", string.Empty);
            childhand = GetHwnd(handle, IntPtr.Zero, "#32770", string.Empty);
            IntPtr StyleWnd = MyGetWindowLongPtr(childhand, WindowLongFlags.GWL_STYLE);
            IntPtr dlghwnd = IntPtr.Zero;
            while (childhand != IntPtr.Zero)
            {
                if ((int)StyleWnd == 1342177348)
                {
                    dlghwnd =FindWindowEx(childhand, IntPtr.Zero, "SysListView32", "List1");
                    if (dlghwnd!= IntPtr.Zero)
                    {
                        if (FindWindowEx(childhand, IntPtr.Zero, "Button", "输 出") != IntPtr.Zero)
                        {
                            //return listview handle.
                            IntPtr btnrefresh = FindWindowEx(childhand, IntPtr.Zero, "Button", "刷 新");
                            if (btnrefresh != IntPtr.Zero)
                            {
                                PostMessage(btnrefresh, BM_CLICK, 0, 0); //Send left click(0x00f5) to OK button 
                                System.Threading.Thread.Sleep(5000);
                            }
                            return dlghwnd;
                        }
                    }
                }
                childhand = FindWindowEx(handle, childhand, "#32770", string.Empty);
                if (childhand != IntPtr.Zero)
                {
                    StyleWnd = MyGetWindowLongPtr(childhand, WindowLongFlags.GWL_STYLE);
                }
            }
            return childhand;
        }

        public static decimal GetInitBuyPrice(IntPtr dlghwnd, string stockno)
        {
            IntPtr edithwnd = GetHwnd(dlghwnd, IntPtr.Zero, "Edit", string.Empty);
            SendTxtMessage((int)edithwnd, 12, 0, stockno.ToCharArray());
            System.Threading.Thread.Sleep(2000);
            edithwnd = GetHwnd(dlghwnd, edithwnd, "Edit", string.Empty);
            string buy1price = GetControlText(edithwnd);
            if (String.IsNullOrEmpty(buy1price)) return 0;
            decimal result =0;
            decimal.TryParse(buy1price, out result);
            return result;
        }

        public static decimal GetInitSellPrice(IntPtr dlghwnd, string stockno, out int maxnum)
        {
            IntPtr edithwnd = GetHwnd(dlghwnd, IntPtr.Zero, "Edit", string.Empty);
            SendTxtMessage((int)edithwnd, 12, 0, stockno.ToCharArray());
            System.Threading.Thread.Sleep(2000);
            edithwnd = GetHwnd(dlghwnd, edithwnd, "Edit", string.Empty);
            string sell1price = GetControlText(edithwnd);
            maxnum = 0;
            if (String.IsNullOrEmpty(sell1price)) return 0;
            decimal result = 0;
            decimal.TryParse(sell1price, out result);
            IntPtr numberptr = GetHwnd(dlghwnd, IntPtr.Zero, "Static", "卖出数量:");
            numberptr = GetHwnd(dlghwnd, numberptr, "Static", null);
            numberptr = GetHwnd(dlghwnd, numberptr, "Static", null);
            string number = GetControlText(numberptr);
            int.TryParse(number, out maxnum);
            return result;
        }

        public static List<string> GetWare(IntPtr dlghwnd)
        {
            try
            {
                IntPtr lsthwnd = FindWindowEx(dlghwnd, IntPtr.Zero, "SysListView32", "List1");
                if (lsthwnd != IntPtr.Zero)
                {
                    List<string> listviewdataforname = GetListViewText(lsthwnd, 1);
                    List<string> listviewdatafornumber = GetListViewText(lsthwnd, 2);
                    List<string> listviewdataforno = GetListViewText(lsthwnd, 0);
                    List<string> results = new List<string>();
                    for (int i = 0; i < listviewdataforno.Count; i++)
                    {
                        results.Add(listviewdataforno[i] + "," + listviewdataforname[i] + "," + listviewdatafornumber[i]);
                    }
                    return results;
                }
            }
            catch (Exception ex)
            {
                Log("GetWare failed: " + ex.Message);
            }
            return null;
         }

        public static IntPtr GetBuyDialog(IntPtr hwnd)
        {
            IntPtr handle = GetHwnd(hwnd, IntPtr.Zero, "AfxFrameOrView42", string.Empty);
            handle = GetHwnd(handle, IntPtr.Zero, "#32770", "通达信网上交易V6");
            Unlockscreen(handle);
            handle = GetHwnd(handle, IntPtr.Zero, "Afx:10000000:3:10011:1900010:10003", string.Empty);
            handle = GetHwnd(handle, IntPtr.Zero, "AfxMDIFrame42", string.Empty);
            IntPtr childhand = GetHwnd(handle, IntPtr.Zero, "AfxWnd42", string.Empty);
            handle = GetHwnd(handle, childhand, "AfxWnd42", string.Empty);
            childhand = GetHwnd(handle, IntPtr.Zero, "#32770", string.Empty);
            IntPtr StyleWnd = MyGetWindowLongPtr(childhand, WindowLongFlags.GWL_STYLE);
            while (childhand != IntPtr.Zero)
            {
                if ((int)StyleWnd == 1342177348)
                {
                    if (FindWindowEx(childhand, IntPtr.Zero, "Static", "买入价格:") != IntPtr.Zero)
                    {
                        if (FindWindowEx(childhand, IntPtr.Zero, "Button", "买入下单") != IntPtr.Zero)
                        {
                            return childhand;
                        }
                    }
                }
                childhand = FindWindowEx(handle, childhand, "#32770", string.Empty);
                if (childhand != IntPtr.Zero)
                {
                    StyleWnd = MyGetWindowLongPtr(childhand, WindowLongFlags.GWL_STYLE);
                }
            }
            return childhand;
        }

        public static TodayStatus GetTodayData(IntPtr hwnd)
        {
            TodayStatus status = TodayStatus.Updated;
            try
            {
                SetForegroundWindow(hwnd);
                IntPtr handle = GetTreeObject(hwnd);
                int treeItem = 0;
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_ROOT, IntPtr.Zero);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_CHILD, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                SendMessage((int)handle, TV_SELECTITEM, TVGN_CARET, (IntPtr)treeItem);
                RECT[] rec = new RECT[1];
                if (GetTreeViewItemRECT(handle, (IntPtr)treeItem, ref rec))
                {
                    RECT hwdrect;
                    int res = GetWindowRect(hwnd, out hwdrect);
                    if (res == 1)
                    {
                        int fixedx = 10;
                        int fixedy = 54;
                        RECT itemrect = rec[0];
                        if ((itemrect.left == 0) && (itemrect.top == 0) && (itemrect.right == 0) && (itemrect.bottom == 0))
                            return status;
                        clicknode(hwdrect.left + fixedx + itemrect.left + (itemrect.right - itemrect.left) / 2, hwdrect.top + fixedy + itemrect.top + (itemrect.bottom - itemrect.top) / 2);
                        IntPtr listviewhwnd = GetToday(hwnd);
                        if (listviewhwnd != IntPtr.Zero)
                        {
                            List<string> listviewdata = GetListViewText(listviewhwnd,0);
                            if (listviewdata.Count > 0)
                            {
                                if (listviewdata[0] == "没有相应的查询信息!")
                                {
                                    return TodayStatus.Normal;
                                }
                                //List<string> listviewdatafortest = GetListViewText(listviewhwnd, 11);
                                //if (listviewdatafortest.Count > 0)
                                //{
                                //    throw new Exception("open wrong window!!!!");
                                //}
                                List<string> listviewdatafortag = GetListViewText(listviewhwnd, 3);
                                //change it from 7 ->6
                                List<string> listviewdataformoney = GetListViewText(listviewhwnd, 6);
                                List<string> listviewdataforno = GetListViewText(listviewhwnd, 1);
                                List<string> listviewdatafortime = GetListViewText(listviewhwnd, 0);                                
                                if (listviewdatafortag.Count == listviewdataformoney.Count)
                                {
                                    decimal buymoney = 0;
                                    decimal sellmoney = 0;
                                    for (int i = 0; i < listviewdatafortag.Count; i++)
                                    {
                                        if (listviewdatafortag[i] == "买入")
                                        {
                                            buymoney += decimal.Parse(listviewdataformoney[i]);
                                        }
                                        else if (listviewdatafortag[i] == "卖出")
                                        {
                                            if (!listviewdataforno[i].StartsWith("1318"))
                                            {
                                                sellmoney += decimal.Parse(listviewdataformoney[i]);
                                            }
                                        }
                                    }
                                    decimal OverFlowMoney = 5000;
                                    decimal.TryParse(ConfigurationSettings.AppSettings["overflow"].ToString(), out OverFlowMoney);

                                    if ((buymoney-sellmoney) > OverFlowMoney)
                                    {
                                        status = TodayStatus.BuyOverFlow;
                                    }
                                    else if ((sellmoney - buymoney) > OverFlowMoney)
                                    {
                                        status = TodayStatus.SellOverFlow;
                                    }
                                    else
                                    {
                                        status = TodayStatus.Normal;
                                    }
                                }
                            }
                            return status;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log("GetTodayData failed: " +ex.Message + "\\"+ ex.StackTrace);
            }
            return status;
        }

        public static List<string> UpdateTodayData(IntPtr hwnd)
        {
            try
            {
                SetForegroundWindow(hwnd);
                IntPtr handle = GetTreeObject(hwnd);
                int treeItem = 0;
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_ROOT, IntPtr.Zero);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_CHILD, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                SendMessage((int)handle, TV_SELECTITEM, TVGN_CARET, (IntPtr)treeItem);
                RECT[] rec = new RECT[1];
                if (GetTreeViewItemRECT(handle, (IntPtr)treeItem, ref rec))
                {
                    RECT hwdrect;
                    int res = GetWindowRect(hwnd, out hwdrect);
                    if (res == 1)
                    {
                        int fixedx = 10;
                        int fixedy = 54;
                        RECT itemrect = rec[0];
                        if ((itemrect.left == 0) && (itemrect.top == 0) && (itemrect.right == 0) && (itemrect.bottom == 0))
                            return null;
                        clicknode(hwdrect.left + fixedx + itemrect.left + (itemrect.right - itemrect.left) / 2, hwdrect.top + fixedy + itemrect.top + (itemrect.bottom - itemrect.top) / 2);
                        IntPtr listviewhwnd = GetToday(hwnd);
                        if (listviewhwnd != IntPtr.Zero)
                        {
                            List<string> deals = new List<string>();
                            List<string> listviewdata = GetListViewText(listviewhwnd, 0);
                            if (listviewdata.Count > 0)
                            {
                                if (listviewdata[0] == "没有相应的查询信息!")
                                {
                                    return null;
                                }
                                //List<string> listviewdatafortest = GetListViewText(listviewhwnd, 11);
                                //if (listviewdatafortest.Count > 0)
                                //{
                                //    throw new Exception("open fault window!!!");
                                //}
                                List<string> listviewdatafortag = GetListViewText(listviewhwnd, 3);
                                // change 5 -> 6
                                List<string> listviewdataforprice = GetListViewText(listviewhwnd, 4);
                                List<string> listviewdataforno = GetListViewText(listviewhwnd, 1);
                                List<string> listviewdatafororder = GetListViewText(listviewhwnd, 8);
                                // change 6 -> 5
                                List<string> listviewdatafornumber = GetListViewText(listviewhwnd, 5);
                                List<string> listviewdataforname = GetListViewText(listviewhwnd, 2);
                                List<string> listviewdatafortime = GetListViewText(listviewhwnd, 0);
                                string results = string.Empty;
                                for (int i = 0; i < listviewdataforno.Count; i++)
                                {
                                    results = string.Format("{0},{1},{2},{3},{4},{5},{6}", listviewdataforno[i], listviewdatafortag[i], listviewdataforprice[i], listviewdatafororder[i], listviewdatafornumber[i], listviewdataforname[i], listviewdatafortime[i]);
                                    deals.Add(results);
                                }
                            }
                            return deals;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log("UpdateTodayData failed: " +ex.Message+ "\\" + ex.StackTrace);
            }
            return null;
        }

        public static bool RemoveOrders(IntPtr hwnd, OrderStatus status, string stockno)
        {            
            try
            {
                SetForegroundWindow(hwnd);
                IntPtr handle = GetTreeObject(hwnd);
                int treeItem = 0;
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_ROOT, IntPtr.Zero);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                SendMessage((int)handle, TV_SELECTITEM, TVGN_CARET, (IntPtr)treeItem);
                RECT[] rec = new RECT[1];
                if (GetTreeViewItemRECT(handle, (IntPtr)treeItem, ref rec))
                {
                    RECT hwdrect;
                    int res = GetWindowRect(hwnd, out hwdrect);
                    if (res == 1)
                    {
                        int fixedx = 10;
                        int fixedy = 54;
                        RECT itemrect = rec[0];
                        if ((itemrect.left == 0) && (itemrect.top == 0) && (itemrect.right == 0) && (itemrect.bottom == 0))
                            return false;
                        clicknode(hwdrect.left + fixedx + itemrect.left + (itemrect.right - itemrect.left) / 2, hwdrect.top + fixedy + itemrect.top + (itemrect.bottom - itemrect.top) / 2);
                        System.Threading.Thread.Sleep(2000);
                        IntPtr dlghwnd = IntPtr.Zero;
                        IntPtr listviewhwnd = GetOrder(hwnd,out dlghwnd);
                        if (listviewhwnd != IntPtr.Zero)
                        {
                            List<string> listviewdata = GetListViewText(listviewhwnd, 0);
                            if (listviewdata.Count > 0)
                            {
                                if (listviewdata[0] == "没有相应的查询信息!")
                                {
                                    return true;
                                }
                                List<string> listviewdatafortag = GetListViewText(listviewhwnd, 3);
                                List<string> listviewdataforno = GetListViewText(listviewhwnd, 1);
                                // change 5-> 6
                                List<string> listviewdataforprice = GetListViewText(listviewhwnd, 6);
                                List<string> listviewdatafortype = GetListViewText(listviewhwnd, 11);
                                bool selected = false;
                                if (listviewdatafortag.Count == listviewdataforno.Count)
                                {
                                    if (status == OrderStatus.All)
                                    {
                                        if (string.IsNullOrEmpty(stockno))
                                        {
                                            //remove all
                                            for (int i = 0; i < listviewdataforno.Count; i++)
                                            {
                                                if (listviewdatafortype[i] != "申购")
                                                {
                                                    SelectOneRow(listviewhwnd, i);
                                                    selected = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //remove this stock
                                            for (int i = 0; i < listviewdataforno.Count; i++)
                                            {
                                                if (listviewdataforno[i] == stockno)
                                                {
                                                    SelectOneRow(listviewhwnd, i);
                                                    selected = true;
                                                }
                                            }
                                        }
                                    }
                                    else if(status == OrderStatus.Buy)
                                    {
                                        if (string.IsNullOrEmpty(stockno))
                                        {
                                            for (int i = 0; i < listviewdataforno.Count; i++)
                                            {
                                                if (listviewdatafortag[i] == "买入" && listviewdatafortype[i] != "申购")
                                                {
                                                    SelectOneRow(listviewhwnd, i);
                                                    selected = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < listviewdataforno.Count; i++)
                                            {
                                                if (listviewdatafortag[i] == "卖出")
                                                {
                                                    continue;
                                                }
                                                if (listviewdataforno[i] == stockno)
                                                {
                                                    SelectOneRow(listviewhwnd, i);
                                                    selected = true;
                                                }
                                            }
                                        }
                                    }
                                    else if (status == OrderStatus.Sell)
                                    {
                                        if (string.IsNullOrEmpty(stockno))
                                        {
                                            for (int i = 0; i < listviewdataforno.Count; i++)
                                            {
                                                if (listviewdatafortag[i] == "卖出")
                                                {
                                                    SelectOneRow(listviewhwnd, i);
                                                    selected = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < listviewdataforno.Count; i++)
                                            {
                                                if (listviewdatafortag[i] == "买入")
                                                {
                                                    continue;
                                                }
                                                if (listviewdataforno[i] == stockno)
                                                {
                                                    SelectOneRow(listviewhwnd, i);
                                                    selected = true;
                                                }
                                            }
                                        }                                        
                                    }
                                    else if (status == OrderStatus.Repeated)
                                    {
                                        for (int i = 0; i < listviewdataforno.Count; i++)
                                        {
                                            string tag = listviewdatafortag[i];
                                            string price = listviewdataforprice[i];
                                            bool find = false;
                                            for (int j = 0; j < listviewdataforno.Count; j++)
                                            {
                                                if (listviewdataforno[i] == listviewdataforno[j])
                                                {
                                                    if (tag == "买入")
                                                    {
                                                        //get highest buy price
                                                        if ((tag == listviewdatafortag[j]) && (decimal.Parse(listviewdataforprice[j]) > decimal.Parse(price)))
                                                        {
                                                            price = listviewdataforprice[j];
                                                            find = true;
                                                        }
                                                    }
                                                    else if (tag == "卖出")
                                                    {
                                                        //get lowest sell price
                                                        if ((tag == listviewdatafortag[j]) && (decimal.Parse(listviewdataforprice[j]) < decimal.Parse(price)))
                                                        {
                                                            price = listviewdataforprice[j];
                                                            find = true;
                                                        }

                                                    }
                                                }
                                            }
                                            if (price != listviewdataforprice[i] || !find)
                                            {
                                                for (int j = 0; j < listviewdataforno.Count; j++)
                                                {
                                                    if (listviewdataforno[i] == listviewdataforno[j])
                                                    {
                                                        if (tag == "买入")
                                                        {
                                                            if ((tag == listviewdatafortag[j]) && (decimal.Parse(listviewdataforprice[j]) < decimal.Parse(price)))
                                                            {
                                                                SelectOneRow(listviewhwnd, j);
                                                                selected = true;
                                                            }
                                                        }
                                                        else if (tag == "卖出")
                                                        {
                                                            if ((tag == listviewdatafortag[j]) && (decimal.Parse(listviewdataforprice[j]) > decimal.Parse(price)))
                                                            {
                                                                SelectOneRow(listviewhwnd, j);
                                                                selected = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            if (!selected)
                                            {
                                                for (i = 0; i < listviewdataforno.Count; i++)
                                                {
                                                    tag = listviewdatafortag[i];
                                                    price = listviewdataforprice[i];
                                                    for (int j = i+1; j < listviewdataforno.Count; j++)
                                                    {
                                                        if (listviewdataforno[i] == listviewdataforno[j] && listviewdatafortag[i] == listviewdatafortag[j] && listviewdataforprice[i] == listviewdataforprice[j])
                                                        {
                                                            SelectOneRow(listviewhwnd, j);
                                                            selected = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (selected)
                                {
                                    IntPtr btnOK = GetHwnd(dlghwnd, IntPtr.Zero, "Button", "撤 单");
                                    PostMessage(btnOK, BM_CLICK, 0, 0); //Send left click(0x00f5) to OK button                                 
                                }
                            }
                            return true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log("RemoveOrders failed :" + ex.StackTrace);
            }
            return false;
        }

        public static List<string> UpdateOrderStatus(IntPtr hwnd)
        {
            try
            {
                SetForegroundWindow(hwnd);
                IntPtr handle = GetTreeObject(hwnd);
                int treeItem = 0;
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_ROOT, IntPtr.Zero);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                SendMessage((int)handle, TV_SELECTITEM, TVGN_CARET, (IntPtr)treeItem);
                RECT[] rec = new RECT[1];
                if (GetTreeViewItemRECT(handle, (IntPtr)treeItem, ref rec))
                {
                    RECT hwdrect;
                    int res = GetWindowRect(hwnd, out hwdrect);
                    if (res == 1)
                    {
                        int fixedx = 10;
                        int fixedy = 54;
                        RECT itemrect = rec[0];
                        if ((itemrect.left == 0) && (itemrect.top == 0) && (itemrect.right == 0) && (itemrect.bottom == 0))
                            return null;
                        clicknode(hwdrect.left + fixedx + itemrect.left + (itemrect.right - itemrect.left) / 2, hwdrect.top + fixedy + itemrect.top + (itemrect.bottom - itemrect.top) / 2);
                        System.Threading.Thread.Sleep(2000);
                        IntPtr dlghwnd = IntPtr.Zero;
                        IntPtr listviewhwnd = GetOrder(hwnd, out dlghwnd);
                        List<string> orders = new List<string>();
                        if (listviewhwnd != IntPtr.Zero)
                        {
                            List<string> listviewdata = GetListViewText(listviewhwnd, 0);
                            if (listviewdata.Count > 0)
                            {
                                if (listviewdata[0] == "没有相应的查询信息!")
                                {
                                    return null;
                                }
                                List<string> listviewdatafortag = GetListViewText(listviewhwnd, 3);
                                List<string> listviewdataforno = GetListViewText(listviewhwnd, 1);
                                string result = string.Empty;                                
                                for (int i = 0; i < listviewdataforno.Count; i++)
                                {
                                    result = String.Format("{0},{1}", listviewdataforno[i], listviewdatafortag[i]);
                                    orders.Add(result);                                    
                                }
                            }
                            return orders;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log("UpdateOrderStatus failed: " + ex.StackTrace);
            }
            return null;
        }

        private static void SelectOneRow(IntPtr listviewhwnd,int rowIndex)
        {
            SetForegroundWindow(listviewhwnd);
            RECT listviewrect;
            GetWindowRect(listviewhwnd, out listviewrect);
            clicknode(listviewrect.left + 10 + 50, listviewrect.top + 21 + (int)((rowIndex) * 18));
            System.Threading.Thread.Sleep(1000);
        }

        public static bool ClickBuy(IntPtr hwnd)
        {
            try
            {
                SetForegroundWindow(hwnd);
                IntPtr handle = GetTreeObject(hwnd);
                int treeItem = 0;
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_ROOT, IntPtr.Zero);
                SendMessage((int)handle, TV_SELECTITEM, TVGN_CARET, (IntPtr)treeItem);
                RECT[] rec = new RECT[1];
                if (GetTreeViewItemRECT(handle, (IntPtr)treeItem, ref rec))
                {
                    RECT hwdrect;
                    int res = GetWindowRect(hwnd, out hwdrect);
                    if (res == 1)
                    {
                        int fixedx = 10;
                        int fixedy = 54;
                        RECT itemrect = rec[0];
                        if ((itemrect.left == 0) && (itemrect.top == 0) && (itemrect.right == 0) && (itemrect.bottom == 0))
                            return false;
                        clicknode(hwdrect.left + fixedx + itemrect.left + (itemrect.right - itemrect.left) / 2, hwdrect.top + fixedy + itemrect.top + (itemrect.bottom - itemrect.top) / 2);
                        if (GetBuyDialog(hwnd) != IntPtr.Zero)
                        {
                            return true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log("ClickBuy failed: " + ex.StackTrace);
            }
            return false;           
        }

        public static bool ClickSell(IntPtr hwnd)
        {
            try
            {
                SetForegroundWindow(hwnd);
                IntPtr handle = GetTreeObject(hwnd);
                int treeItem = 0;
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_ROOT, IntPtr.Zero);
                treeItem = SendMessage((int)handle, TVM_GETNEXTITEM, TVGN_NEXT, (IntPtr)treeItem);
                SendMessage((int)handle, TV_SELECTITEM, TVGN_CARET, (IntPtr)treeItem);
                RECT[] rec = new RECT[1];
                if (GetTreeViewItemRECT(handle, (IntPtr)treeItem, ref rec))
                {
                    RECT hwdrect;
                    int res = GetWindowRect(hwnd, out hwdrect);
                    if (res == 1)
                    {
                        int fixedx = 10;
                        int fixedy = 54;
                        RECT itemrect = rec[0];
                        if ((itemrect.left == 0) && (itemrect.top == 0) && (itemrect.right == 0) && (itemrect.bottom == 0))
                            return false;
                        clicknode(hwdrect.left + fixedx + itemrect.left + (itemrect.right - itemrect.left) / 2, hwdrect.top + fixedy + itemrect.top + (itemrect.bottom - itemrect.top) / 2);
                        if (GetSellDialog(hwnd) != IntPtr.Zero)
                        {
                            return true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log("ClickSell failed: " + ex.StackTrace);
            }
            return false;
        }

        public static bool BuyStock(IntPtr hwnd, string no, string price, string number)
        {
            try
            {
                //all keys http://blog.csdn.net/hfzsjz/article/details/4350715
                System.Threading.Thread.Sleep(1000);
                SendKeys.SendWait(no);
                SendKeys.SendWait(price);
                SendKeys.SendWait("{TAB}");
                System.Threading.Thread.Sleep(1000);
                SendKeys.SendWait("{TAB}"+number);
                SendKeys.SendWait("{ENTER}");                
                return true;
            }
            catch(Exception ex)
            {
                Log("BuyStock failed: " + ex.StackTrace);
            }
            return false;
        }

        public static bool SellStock(IntPtr hwnd, string no, string price, string number)
        {
            try
            {
                //all keys http://blog.csdn.net/hfzsjz/article/details/4350715
                System.Threading.Thread.Sleep(1000);
                SendKeys.SendWait(no);                                
                //SendKeys.Send(price);
                System.Threading.Thread.Sleep(1000);
                SendKeys.SendWait(price);
                SendKeys.SendWait("{TAB}");
                System.Threading.Thread.Sleep(1000);
                SendKeys.SendWait(number);                
                //SendKeys.SendWait("{TAB}"+ number);                
                SendKeys.SendWait("{ENTER}");
                //SendKeys.Send("{TAB}");
                return true;
            }
            catch (Exception ex)
            {
                Log("SellStock failed: " + ex.StackTrace);
            }
            return false;
        }

        private static string GetControlText(IntPtr hWnd)
        {
            StringBuilder title = new StringBuilder();            // Get the size of the string required to hold the window title.   
            int size = SendMessage((int)hWnd, (int)WM_GETTEXTLENGTH, 0, IntPtr.Zero); // If the return is 0, there is no title. 
            if (size > 0) 
            {         
                title = new StringBuilder(size + 1);
                SendMessage(hWnd,WM_GETTEXT, title.Capacity, title); 
            }  
            return title.ToString();  
        }

        private static void clicknode(int x, int y)
        {
            POINT p = new POINT();
            GetCursorPos(out p);

            try
            {
                ShowCursor(false);
                SetCursorPos(x, y);

                mouse_event((int)(MouseEventFlags.LeftDown | MouseEventFlags.Absolute), 0, 0, 0, IntPtr.Zero);
                mouse_event((int)(MouseEventFlags.LeftUp | MouseEventFlags.Absolute), 0, 0, 0, IntPtr.Zero);

                System.Threading.Thread.Sleep(2000);
            }
            finally
            {
                SetCursorPos(p.X, p.Y);
                ShowCursor(true);
            }
        }

        private static bool GetTreeViewItemRECT(IntPtr TreeViewHandle, IntPtr TreeItemHandle, ref RECT[] Rect)
        {
            bool result = false;
            int processId;
            GetWindowThreadProcessId(TreeViewHandle, out processId);
            IntPtr process = (IntPtr)OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE, false, processId);
            IntPtr buffer = (IntPtr)VirtualAllocEx(process, 0, 4096, MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);
            try
            {
                uint bytes = 0;
                WriteProcessMemory(process, buffer, ref TreeItemHandle, Marshal.SizeOf(TreeItemHandle), ref bytes);
                SendMessage((int)TreeViewHandle, TVM_GETITEMRECT, 1, buffer);
                ReadProcessMemory((uint)process, buffer, Marshal.UnsafeAddrOfPinnedArrayElement(Rect, 0), (uint)Marshal.SizeOf(typeof(RECT)), ref bytes);
                result = true;
            }
            finally
            {
                VirtualFreeEx(process, buffer, 0, MEM_RELEASE);
                CloseHandle(process);
            }
            return result;
        }

        private static bool GetListViewItemRECT(IntPtr ListViewHandle, IntPtr TreeItemHandle, ref RECT[] Rect)
        {
            bool result = false;
            int processId;
            GetWindowThreadProcessId(ListViewHandle, out processId);
            IntPtr process = (IntPtr)OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE, false, processId);
            IntPtr buffer = (IntPtr)VirtualAllocEx(process, 0, 4096, MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);
            try
            {
                uint bytes = 0;
                WriteProcessMemory(process, buffer, ref TreeItemHandle, Marshal.SizeOf(TreeItemHandle), ref bytes);
                SendMessage((int)ListViewHandle, LVM_GETITEMRECT, 1, buffer);
                ReadProcessMemory((uint)process, buffer, Marshal.UnsafeAddrOfPinnedArrayElement(Rect, 0), (uint)Marshal.SizeOf(typeof(RECT)), ref bytes);
                result = true;
            }
            finally
            {
                VirtualFreeEx(process, buffer, 0, MEM_RELEASE);
                CloseHandle(process);
            }
            return result;
        }

        private static List<string> GetListViewText(IntPtr AHandle,int ColumnNo) 
        {
            List<string> AOutput = new List<string>();
            if (AHandle == IntPtr.Zero) 
                return AOutput;
            int vProcessId;
            GetWindowThreadProcessId(AHandle, out vProcessId);
            int vItemCount = SendMessage((int)AHandle, LVM_GETITEMCOUNT, 0, IntPtr.Zero); 
            IntPtr vProcess = (IntPtr)OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE, false, vProcessId);
            IntPtr vPointer = (IntPtr)VirtualAllocEx(vProcess, 0, 4096, MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);
            try
            { 
                for (int i = 0; i < vItemCount; i++)
                { 
                    byte[] vBuffer = new byte[256];
                    LVITEM[] vItem = new LVITEM[1]; 
                    vItem[0].mask = LVIF_TEXT;
                    vItem[0].iItem = i;
                    vItem[0].iSubItem = ColumnNo;
                    vItem[0].cchTextMax = vBuffer.Length; 
                    vItem[0].pszText = (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(LVITEM)));
                    uint vNumberOfBytesRead = 0;
                    WriteProcessMemory(vProcess, vPointer, Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0), Marshal.SizeOf(typeof(LVITEM)), ref vNumberOfBytesRead);
                    SendMessage((int)AHandle, LVM_GETITEMW, i, vPointer); 
                    ReadProcessMemory(vProcess, (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(LVITEM))), Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0), vBuffer.Length, ref vNumberOfBytesRead);
                    string vText = Marshal.PtrToStringUni(Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0));
                    AOutput.Add(vText);
                }
            }
            finally
            { 
                VirtualFreeEx(vProcess, vPointer, 0, MEM_RELEASE);
                CloseHandle(vProcess);
            }
            return AOutput;
        }

        public static string GetLastUpdatedTime()
        {
            return DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToLongTimeString();
        }

        public static void Log(string logMessage)
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                w.WriteLine("{0}: {1}-{2}", logMessage, DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
            }
        }

        public static string Decrypt(string cypher)
        {
            byte[] bytes = System.Convert.FromBase64String(cypher);
            byte[] decrypted = System.Security.Cryptography.ProtectedData.Unprotect(bytes, null, System.Security.Cryptography.DataProtectionScope.LocalMachine);
            return System.Text.Encoding.Unicode.GetString(decrypted);
        }

        public static string Encrypt(string data)
        {
            byte[] plaintext = null;
            plaintext = Encoding.Unicode.GetBytes(data);
            byte[] ciphertext = System.Security.Cryptography.ProtectedData.Protect(plaintext, null, System.Security.Cryptography.DataProtectionScope.LocalMachine);
            return System.Convert.ToBase64String(ciphertext, 0, ciphertext.Length);
        }

        public static long GetCurrentTimeStamp()
        {
            DateTime dt = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(DateTime.Now - dt).TotalSeconds;
        }

        public static long GetTimeStamp(DateTime dt)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(dt - dtStart).TotalSeconds;
        }

        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }

    }
}
