using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace stockassistant
{
    public partial class Form1 : Form
    {
        private List<Stock> Stocks;
        private bool isstarted;
        private bool initsuccess;
        private bool downloaded;
        private bool uploaded;
        private Utility.TodayStatus status = Utility.TodayStatus.Normal;
        private bool ispause;
        private WebBrowerControl browser = new WebBrowerControl(1);
        private Dictionary<string, string> ordersforbuy = new Dictionary<string, string>();
        private Dictionary<string, string> ordersforsell = new Dictionary<string, string>();
        private bool disablesynced = ConfigurationSettings.AppSettings["disablesync"] !=null? bool.Parse(System.Configuration.ConfigurationSettings.AppSettings["disablesync"]):false;
        private bool panenable = ConfigurationSettings.AppSettings["panenable"] != null ? bool.Parse(System.Configuration.ConfigurationSettings.AppSettings["panenable"]) : false;

        #region Method

        private void SendSMS(string msg)
        {
            if (browser.CheckBrowser(""))
            {
                browser.SendMsg(msg);
            }
        }

        private void CloseStock()
        {
            string maincaption = System.Configuration.ConfigurationSettings.AppSettings["mainname"].ToString();
            IntPtr hwnd = Utility.FindWindow(null, maincaption);
            if (hwnd == IntPtr.Zero)
            {
                return;
            }
            Utility.CloseWindow(hwnd);

            Application.DoEvents();

            System.Threading.Thread.Sleep(2000);

            SimpleTimer timer = new SimpleTimer(10);
            do
            {
                hwnd = Utility.FindWindow("#32770", "退出确认");
                if (hwnd != IntPtr.Zero)
                    break;
                Application.DoEvents();
            } while (!timer.Elapsed);

            if (hwnd != IntPtr.Zero)
            {
                IntPtr btnOK = Utility.FindWindowEx(hwnd, IntPtr.Zero, "Button", "退出系统");
                Utility.PostMessage(btnOK, 0x00F5, 0, 0);
            }
            else
            {
                Utility.Log("failed to close the tool.");
            }
        }

        private void UpdateStockStatus()
        {
            IntPtr hwnd = Utility.GetWindow();
            if (hwnd == IntPtr.Zero)
            {
                Utility.Log("UpdateStockStatus: failed to find the program");
            }
            List<string> orders = Utility.UpdateOrderStatus(hwnd);
            ispause = false;
            if (orders != null)
            {
                foreach (string order in orders)
                {
                    string[] items = order.Split(',');
                    if (items[0] == "601857")
                    {
                        ispause = true;
                        SendSMS("系统暂停操作");
                        break;
                    }
                }
                foreach (string order in orders)
                {
                    string[] items = order.Split(',');
                    foreach (Stock stock in Stocks)
                    {
                        if (stock.NO == items[0])
                        {
                            if (items[1] == "买入")
                            {
                                stock.MakeBuy = true;
                            }
                            else if (items[1] == "卖出")
                            {
                                stock.MakeSell = true;
                            }
                        }
                    }
                }
            }
        }

        private void UpdateTodayData()
        {
            try
            {
                IntPtr hwnd = Utility.GetWindow();
                if (hwnd == IntPtr.Zero)
                {
                    Utility.Log("UpdateStockStatus: failed to find the program");
                }
                List<string> deals = Utility.UpdateTodayData(hwnd);
                if (deals != null)
                {
                    foreach (string deal in deals)
                    {
                        string[] items = deal.Split(',');
                        int no = 0;
                        int.TryParse(items[3], out no);
                        if (no == 0)
                        {
                            break;
                        }
                        foreach (Stock stock in Stocks)
                        {
                            if (stock.NO == items[0])
                            {
                                if (items[1] == "买入")
                                {
                                    decimal buyprice = 0;
                                    decimal.TryParse(items[2], out buyprice);
                                    if (buyprice <= 0)
                                    {
                                        break;
                                    }
                                    if (stock.LastBuyPrice != buyprice)
                                    {
                                        if (ordersforbuy.ContainsKey(stock.NO))
                                        {
                                            int order = 0;
                                            int.TryParse(ordersforbuy[stock.NO], out order);
                                            if (no <= order)
                                            {
                                                break;
                                            }
                                            ordersforbuy[stock.NO] = items[3];
                                            stock.CanBuy = false;
                                            stock.CanSell = true;
                                        }
                                        else
                                        {
                                            ordersforbuy.Add(stock.NO, items[3]);
                                            stock.CanBuy = false;
                                            stock.CanSell = true;
                                        }
                                        //changed = true;
                                        stock.LastBuyPrice = buyprice;
                                        stock.LastSellPrice = buyprice;
                                        stock.Name = items[5];
                                        if (!stock.IsWatch)
                                        {
                                            MakeOrder(stock.NO, buyprice.ToString("f2"), items[1]);
                                            stock.MakeSell = true;
                                            SendSMS("成功买入:" + stock.NO + ",名称:" + items[5] + ",价格:" + buyprice.ToString("f2") + ",数量:" + items[4] + ",编号" + ordersforbuy[stock.NO] + ",时间:" + items[6] + "。");
                                        }
                                    }
                                    else
                                    {
                                        if (!ordersforbuy.ContainsKey(stock.NO))
                                        {
                                            ordersforbuy.Add(stock.NO, items[3]);
                                            stock.CanBuy = false;
                                            stock.CanSell = true;
                                        }
                                        else
                                        {
                                            int order = 0;
                                            int.TryParse(ordersforbuy[stock.NO], out order);
                                            // same price, different no.
                                            if (no > order)
                                            {
                                                //changed = true;
                                                ordersforbuy[stock.NO] = items[3];
                                                stock.LastBuyPrice = buyprice;
                                                stock.LastSellPrice = buyprice;
                                                stock.Name = items[5];
                                                stock.CanBuy = false;
                                                stock.CanSell = true;
                                                if (!stock.IsWatch)
                                                {
                                                    MakeOrder(stock.NO, buyprice.ToString("f2"), items[1]);
                                                    stock.MakeSell = true;
                                                    SendSMS("成功买入:" + stock.NO + ",名称:" + items[5] + ",价格:" + buyprice.ToString("f2") + ",数量:" + items[4] + ",编号" + ordersforbuy[stock.NO] + ",时间:" + items[6] + "。");
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (items[1] == "卖出")
                                {
                                    decimal sellprice = 0;
                                    decimal.TryParse(items[2], out sellprice);
                                    if (sellprice <= 0)
                                    {
                                        break;
                                    }
                                    if (stock.LastSellPrice != sellprice)
                                    {
                                        if (ordersforsell.ContainsKey(stock.NO))
                                        {
                                            int order = 0;
                                            int.TryParse(ordersforsell[stock.NO], out order);
                                            if (no <= order)
                                            {
                                                break;
                                            }
                                            ordersforsell[stock.NO] = items[3];
                                            stock.CanSell = false;
                                            stock.CanBuy = true;
                                        }
                                        else
                                        {
                                            ordersforsell.Add(stock.NO, items[3]);
                                            stock.CanSell = false;
                                            stock.CanBuy = true;
                                        }
                                        stock.LastSellPrice = sellprice;
                                        stock.LastBuyPrice = sellprice;
                                        stock.Name = items[5];
                                        if (!stock.IsWatch)
                                        {
                                            MakeOrder(stock.NO, sellprice.ToString("f2"), items[1]);
                                            stock.MakeBuy = true;
                                            SendSMS("成功卖出:" + stock.NO + ",名称:" + items[5] + ",价格:" + sellprice.ToString("f2") + ",数量:" + items[4] + ",编号" + ordersforsell[stock.NO] + ",时间:" + items[6] + "。");
                                        }
                                    }
                                    else
                                    {
                                        if (!ordersforsell.ContainsKey(stock.NO))
                                        {
                                            ordersforsell.Add(stock.NO, items[3]);
                                            stock.CanSell = false;
                                            stock.CanBuy = true;
                                        }
                                        else
                                        {
                                            int order = 0;
                                            int.TryParse(ordersforsell[stock.NO], out order);
                                            // same price, different no.
                                            if (no > order)
                                            {
                                                ordersforsell[stock.NO] = items[3];
                                                stock.LastSellPrice = sellprice;
                                                stock.LastBuyPrice = sellprice;
                                                stock.Name = items[5];
                                                stock.CanSell = false;
                                                stock.CanBuy = true;
                                                if (!stock.IsWatch)
                                                {                                                    
                                                    MakeOrder(stock.NO, sellprice.ToString("f2"), items[1]);
                                                    stock.MakeBuy = true;
                                                    SendSMS("成功卖出:" + stock.NO + ",名称:" + items[5] + ",价格:" + sellprice.ToString("f2") + ",数量:" + items[4] + ",编号" + ordersforsell[stock.NO] + ",时间:" + items[6] + "。");
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.Log("UpdateTodayDate failed: " + ex.Message + "//" + ex.StackTrace);
            }
        }

        private void InitPrice()
        {
            if (initsuccess)
            {
                return;
            }
            try
            {
                IntPtr hwnd = Utility.GetWindow();
                if (hwnd == IntPtr.Zero)
                {
                    Utility.Log("InitPrice:failed to find the program");
                    return;
                }
                bool changed = false;
                if (Utility.ClickSell(hwnd))
                {
                    System.Threading.Thread.Sleep(2000);
                    IntPtr dlghwnd = Utility.GetSellDialog(hwnd);
                    List<string> results = Utility.GetWare(dlghwnd);
                    if (results != null)
                    {
                        for (int i = 0; i < results.Count; i++)
                        {
                            string[] res = results[i].Split(',');
                            foreach (Stock stock in Stocks)
                            {
                                if (stock.NO == res[0])
                                {
                                    if (stock.Name != res[1])
                                    {
                                        stock.Name = res[1];
                                        changed = true;
                                    }
                                    int number =0;
                                    int.TryParse(res[2],out number);
                                    if (stock.HandNumber != number)
                                    {
                                        stock.HandNumber = number;
                                        changed = true;
                                    }
                                }
                            }
                        }
                    }
                    foreach (Stock stock in Stocks)
                    {
                        System.Threading.Thread.Sleep(1000);
                        stock.PrePrice = Utility.GetInitSellPrice(dlghwnd, stock.NO);
                        if (stock.PrePrice != 0)
                        {
                            stock.CurPrice = stock.PrePrice;
                            changed = true;
                        }
                        stock.Buy1Price = 0;
                        stock.Sell1Price = 0;
                        stock.MakeBuy = false;
                        stock.MakeSell = false;
                        stock.LastUpdatedTime = Utility.GetLastUpdatedTime();
                    }
                }
                if (changed)
                {
                    ordersforbuy = new Dictionary<string, string>();
                    ordersforsell = new Dictionary<string, string>();
                    SaveData();
                    initsuccess = true;
                }
            }
            catch (Exception ex)
            {
                Utility.Log("InitPrice failed: " + ex.StackTrace);
            }
        }

        private void UpdatePrice()
        {
            try
            {
                IntPtr hwnd = Utility.GetWindow();
                if (hwnd == IntPtr.Zero)
                {
                    Utility.Log("UpdatePrice:failed to find the program");
                    return;
                }
                if (status == Utility.TodayStatus.Updated)
                {
                    Utility.Log("Check the status:" + status.ToString());
                    return;
                }
                if (Utility.ClickSell(hwnd))
                {
                    System.Threading.Thread.Sleep(1000);
                    IntPtr dlghwnd = Utility.GetSellDialog(hwnd);
                    foreach (Stock stock in Stocks)
                    {
                        decimal price = Utility.GetInitSellPrice(dlghwnd, stock.NO);
                        if (price == 0)
                        {
                            System.Threading.Thread.Sleep(2000);
                            continue;
                        }
                        if (stock.HighPrice < price)
                        {
                            stock.HighPrice = price;
                            stock.RaiseStage = true;
                        }
                        if (stock.LowPrice > price)
                        {
                            stock.LowPrice = price;
                            stock.RaiseStage = false;
                        }
                        if (stock.Buy1Price == 0)
                        {
                            stock.Buy1Price = price;
                        }
                        if (stock.Sell1Price == 0)
                        {
                            stock.Sell1Price = price;
                        }
                        if (!stock.IsWatch)
                        {
                            if (!stock.MakeBuy && stock.CanBuy)
                            {
                                if (status != Utility.TodayStatus.BuyOverFlow)
                                {
                                    decimal buyprice = GetBuyPrice(stock, price);
                                    if (buyprice != 0)
                                    {
                                        Buy(stock.NO, buyprice.ToString("f2"), stock.Number.ToString());
                                        stock.MakeBuy = true;
                                    }
                                }
                            }
                            else if (!stock.MakeSell && stock.CanSell)
                            {
                                if (status != Utility.TodayStatus.SellOverFlow)
                                {
                                    decimal sellprice = GetSellPrice(stock, price);
                                    if (sellprice != 0)
                                    {
                                        Sell(stock.NO, sellprice.ToString("f2"), stock.Number.ToString());
                                        stock.MakeSell = true;
                                    }
                                }
                            }
                        }
                        stock.LastUpdatedTime = Utility.GetLastUpdatedTime();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.Log("UpdatePrice failed: " + ex.StackTrace);
            }
        }

        private void SaveData()
        {
            if (!(Stocks != null && Stocks.Count > 0))
            {
                return;
            }
            try
            {
                string[] strstocks = new string[Stocks.Count];
                Stock stock;
                for (int i = 0; i < Stocks.Count; i++)
                {
                    stock = Stocks[i] as Stock;
                    string resstock = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", stock.NO, stock.Number,
                        stock.LowPrice, stock.HighPrice, stock.RaiseStage, stock.PrePrice, stock.Buy1Price, stock.Sell1Price,
                        stock.LastBuyPrice, stock.LastSellPrice, Utility.GetLastUpdatedTime(),stock.Name,stock.HandNumber,stock.IsWatch,stock.CurPrice);
                    strstocks[i] = resstock;
                }
                File.WriteAllLines("stock.data", strstocks);
                LoadData();
            }
            catch (Exception ex)
            {
                Utility.Log("SaveData: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                string[] strstocks = File.ReadAllLines("stock.data");
                Stocks.Clear();
                listBox1.Items.Clear();
                foreach (string strstock in strstocks)
                {
                    string[] strs = strstock.Split(',');
                    if (strs.Length != 15)
                    {
                        throw new Exception("wrong data structure");
                    }
                    Stock stock = new Stock
                    {
                        NO = strs[0],
                        Number = int.Parse(strs[1]),
                        LowPrice = decimal.Parse(strs[2]),
                        HighPrice = decimal.Parse(strs[3]),
                        RaiseStage = bool.Parse(strs[4]),
                        PrePrice = decimal.Parse(strs[5]),
                        Buy1Price = decimal.Parse(strs[6]),
                        Sell1Price = decimal.Parse(strs[7]),
                        LastBuyPrice = decimal.Parse(strs[8]),
                        LastSellPrice = decimal.Parse(strs[9]),
                        LastUpdatedTime = strs[10],
                        Name = strs[11],
                        HandNumber = int.Parse(strs[12]),
                        IsWatch = bool.Parse(strs[13]),
                        CurPrice = decimal.Parse(strs[14]),
                        MakeBuy = false,
                        MakeSell = false,
                        CanSell = true,
                        CanBuy = true
                    };
                    Stocks.Add(stock);                    
                    listBox1.Items.Add("编号:"+stock.NO+",名称:" + stock.Name + ",数量:"+ stock.HandNumber.ToString()+",价格:"+ stock.CurPrice.ToString("f2")+",最后更新时间:"+stock.LastUpdatedTime);
                }
            }
            catch (Exception ex)
            {
                Utility.Log("LoadData: " + ex.Message);
            }
        }

        private void GetInitPrice()
        {
            IntPtr hwnd = Utility.GetWindow();
            if (hwnd == IntPtr.Zero)
            {
                Utility.Log("GetInitPrice:failed to find the program");
                return;
            }
            try
            {
                bool ischanged = false;
                if (Utility.ClickBuy(hwnd))
                {
                    System.Threading.Thread.Sleep(1000);
                    IntPtr dlghwnd = Utility.GetBuyDialog(hwnd);
                    foreach (Stock stock in Stocks)
                    {
                        System.Threading.Thread.Sleep(1000);
                        if (stock.Sell1Price == 0)
                        {
                            decimal price = Utility.GetInitBuyPrice(dlghwnd, stock.NO);
                            if (price != 0)
                            {
                                stock.IsWatch = false;
                                stock.Sell1Price = price;
                                stock.LastUpdatedTime = Utility.GetLastUpdatedTime();
                                ischanged = true;
                            }
                            else
                                stock.IsWatch = true;
                        }
                    }
                }
                if (Utility.ClickSell(hwnd))
                {
                    System.Threading.Thread.Sleep(1000);
                    IntPtr dlghwnd = Utility.GetSellDialog(hwnd);
                    foreach (Stock stock in Stocks)
                    {
                        System.Threading.Thread.Sleep(1000);
                        if (stock.Buy1Price == 0)
                        {
                            decimal price = Utility.GetInitSellPrice(dlghwnd, stock.NO);
                            if (price != 0)
                            {
                                stock.IsWatch = false;
                                stock.Buy1Price = price;
                                if (stock.PrePrice == 0)
                                {
                                    stock.PrePrice = stock.Buy1Price;
                                }
                                stock.LastUpdatedTime = Utility.GetLastUpdatedTime();
                                ischanged = true;
                            }
                            else
                                stock.IsWatch = true;
                        }
                    }
                }
                if (ischanged)
                {
                    MakeOrder("", "", "");
                }
            }
            catch (Exception ex)
            {
                Utility.Log("GetInitPrice: failed get init price because by " + ex.StackTrace);
            }
        }

        private void RemoveOrders(Utility.OrderStatus status, string stockno)
        {
            IntPtr hwnd = Utility.GetWindow();
            if (hwnd == IntPtr.Zero)
            {
                Utility.Log("RemoveOrders: failed to find the program");
            }
            if (!Utility.RemoveOrders(hwnd, status, stockno))
            {
                Utility.Log("RemoveOrders: failed to remove orders: status:" + status.ToString() + " stockno: "+ stockno);
            }
            CheckRemove();
        }

        private void GetToday()
        {
            IntPtr hwnd = Utility.GetWindow();
            if (hwnd == IntPtr.Zero)
            {
                Utility.Log("GetToday failed to find the program");
            }

            status = Utility.GetTodayData(hwnd);

            if (status == Utility.TodayStatus.BuyOverFlow)
            {
                //remove buy order.
                RemoveOrders(Utility.OrderStatus.Buy, "");
            }
            else if (status == Utility.TodayStatus.SellOverFlow)
            {
                //remove sell order
                RemoveOrders(Utility.OrderStatus.Sell, "");
            }
        }

        private void MakeOrder(string stockno, string orderprice, string tag)
        {
            try
            {
                foreach (Stock stock in Stocks)
                {
                    if (stock.IsWatch)
                    {
                        continue;
                    }
                    System.Threading.Thread.Sleep(1000);
                    Application.DoEvents();
                    if (!string.IsNullOrEmpty(stockno) && stockno != stock.NO)
                    {
                        continue;
                    }
                    else if (stockno == stock.NO)
                    {
                        if (tag == "卖出" && stock.CanBuy)
                        {
                            RemoveOrders(Utility.OrderStatus.Buy, stock.NO);
                            decimal price = decimal.Parse(orderprice);
                            if (price * stock.HandNumber > 3000)
                                price = price - (price * (decimal)0.025);
                            else
                                price = price - (price * (decimal)0.03);
                            if (stock.LastBuyPrice != 0)
                            {
                                if (price <= stock.LastBuyPrice)
                                {
                                    Buy(stock.NO, price.ToString("f2"), stock.Number.ToString());
                                }
                                else
                                {
                                    Buy(stock.NO, stock.LastBuyPrice.ToString("f2"), stock.Number.ToString());
                                }
                            }
                            else
                            {
                                Buy(stock.NO, price.ToString("f2"), stock.Number.ToString());
                            }
                        }
                        else if (tag == "买入" && stock.CanSell)
                        {
                            RemoveOrders(Utility.OrderStatus.Sell, stock.NO);
                            decimal price = decimal.Parse(orderprice);
                            if (price * stock.Number > 3000)
                                price = price + (price * (decimal)0.025);
                            else
                                price = price + (price * (decimal)0.03);
                            if (stock.LastSellPrice != 0)
                            {
                                if (price >= stock.LastSellPrice)
                                {
                                    Sell(stock.NO, price.ToString("f2"), stock.Number.ToString());
                                }
                                else
                                {
                                    Sell(stock.NO, stock.LastSellPrice.ToString("f2"), stock.Number.ToString());
                                }
                            }
                            else
                            {
                                Sell(stock.NO, price.ToString("f2"), stock.Number.ToString());
                            }
                        }
                        return;
                    }

                    if (!stock.MakeBuy && stock.CanBuy)
                    {
                        decimal price = GetBuyPrice(stock,0);
                        if (price != 0)
                        {
                            Buy(stock.NO, price.ToString("f2"), stock.Number.ToString());
                            stock.MakeBuy = true;
                        }
                    }

                    if (!stock.MakeSell && stock.CanSell)
                    {
                        decimal price = GetSellPrice(stock,0);
                        if (price != 0)
                        {
                            Sell(stock.NO, price.ToString("f2"), stock.Number.ToString());
                            stock.MakeSell = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.Log("MakeOrder failed: " + ex.Message);
            }
        }

        private decimal GetBuyPrice(Stock stock,decimal CurPrice)
        {
            if (stock.HighPrice == 0 || stock.PrePrice == 0)
            {
                return 0;
            }
            decimal oldprice = 0;
            decimal nextprice = stock.HighPrice;
            if (CurPrice == 0)
            {
                oldprice = stock.PrePrice;
            }
            else
            {
                oldprice = CurPrice;
            }
            if (oldprice == 0)
            {
                return 0;
            }
            if (stock.HighPrice < oldprice)
            {
                return 0;
            }
            if (stock.RaiseStage)
            {
                for (int i = 0; i < 10; i++)
                {
                    nextprice = nextprice - (nextprice * decimal.Parse("0.03"));
                    if (oldprice >= nextprice)
                    {
                        break;
                    }
                    nextprice = nextprice - (nextprice * decimal.Parse("0.04"));
                    if (oldprice >= nextprice)
                    {
                        break;
                    }
                    nextprice = nextprice - (nextprice * decimal.Parse("0.05"));
                    if (oldprice >= nextprice)
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    nextprice = nextprice - (nextprice * decimal.Parse("0.05"));
                    if (oldprice >= nextprice)
                    {
                        break;
                    }
                    nextprice = nextprice - (nextprice * decimal.Parse("0.04"));
                    if (oldprice >= nextprice)
                    {
                        break;
                    }
                    nextprice = nextprice - (nextprice * decimal.Parse("0.03"));
                    if (oldprice >= nextprice)
                    {
                        break;
                    }
                }
            }
            if (CurPrice == 0)
            {
                if (stock.Buy1Price < stock.PrePrice)
                {
                    nextprice = nextprice - (stock.PrePrice - stock.Buy1Price);
                }
            }
            decimal borderprice = nextprice + (nextprice * decimal.Parse("0.029"));
            // too far
            //if (borderprice < oldprice)
            //{
            //    return 0;
            //}
            if (stock.LastBuyPrice != 0)
            {
                if (borderprice > stock.LastBuyPrice)
                {
                    return 0;
                }
            }
            decimal lowestPrice = stock.PrePrice * decimal.Parse("0.901");
            if (nextprice < lowestPrice)
            {
                nextprice = lowestPrice;
            }
            return nextprice;
        }

        private decimal GetSellPrice(Stock stock, decimal CurPrice)
        {
            if (stock.LowPrice == 0 || stock.PrePrice == 0)
            {
                return 0;
            }
            decimal oldprice = 0;
            if (CurPrice == 0)
            {
                oldprice = stock.PrePrice;
            }
            else
            {
                oldprice = CurPrice;
            }
            if (oldprice == 0)
            {
                return 0;
            }
            if (stock.LowPrice > oldprice)
            {
                return 0;
            }
            decimal nextprice = stock.LowPrice;
            if (stock.RaiseStage)
            {
                for (int i = 0; i < 10; i++)
                {
                    nextprice = nextprice + (nextprice * decimal.Parse("0.05"));
                    if (oldprice <= nextprice)
                    {
                        break;
                    }
                    nextprice = nextprice + (nextprice * decimal.Parse("0.04"));
                    if (oldprice <= nextprice)
                    {
                        break;
                    }
                    nextprice = nextprice + (nextprice * decimal.Parse("0.03"));
                    if (oldprice <= nextprice)
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    nextprice = nextprice + (nextprice * decimal.Parse("0.03"));
                    if (oldprice <= nextprice)
                    {
                        break;
                    }
                    nextprice = nextprice + (nextprice * decimal.Parse("0.04"));
                    if (oldprice <= nextprice)
                    {
                        break;
                    }
                    nextprice = nextprice + (nextprice * decimal.Parse("0.05"));
                    if (oldprice <= nextprice)
                    {
                        break;
                    }
                }
            }
            if (CurPrice == 0)
            {
                if (stock.Sell1Price > stock.PrePrice)
                {
                    nextprice = nextprice + (stock.Sell1Price - stock.PrePrice);
                }
            }
            decimal borderprice = nextprice - (nextprice * decimal.Parse("0.029"));
            //too far
            //if (borderprice > oldprice)
            //{
            //    return 0;
            //}
            // T+0
            //if (!stock.RaiseStage)
            //{
            if (stock.LastSellPrice != 0)
            {
                if (borderprice < stock.LastSellPrice)
                {
                    return 0;
                }
            }
            //}
            decimal higestPrice = stock.PrePrice * decimal.Parse("1.099");
            if (nextprice > higestPrice)
            {
                nextprice = higestPrice;
            }
            return nextprice;
        }

        private void Buy(string no, string price, string number)
        {
            IntPtr hwnd = Utility.GetWindow();
            if (hwnd == IntPtr.Zero)
            {
                Utility.Log("Buy failed to find the program");
            }

            if (Utility.ClickBuy(hwnd))
            {
                if (!Utility.BuyStock(hwnd, no, price, number))
                {
                    Utility.Log(String.Format("Buy failed: no:{0} price:{1} number:{2}", no, price, number));
                }
                CheckBuy();
                Utility.Log("Buy order made: no-" + no + ",price-"+price+",number-"+number);
            }
            else
            {
                Utility.Log("Buy failed to open buy dialog");
            }

        }

        private void Sell(string no, string price, string number)
        {
            IntPtr hwnd = Utility.GetWindow();
            if (hwnd == IntPtr.Zero)
            {
                Utility.Log("Sell failed to find the program");
            }

            if (Utility.ClickSell(hwnd))
            {
                if (!Utility.SellStock(hwnd, no, price, number))
                {
                    Utility.Log(String.Format("Sell failed: no:{0} price:{1} number:{2}", no, price, number));
                }
                CheckSell();
                Utility.Log("Sell order made: no-" + no + ",price-" + price + ",number-" + number);
            }
            else
            {
                Utility.Log("failed to open sell dialog");
            }
        }

        private void CheckRemove()
        {
            IntPtr hwnd = IntPtr.Zero;
            SimpleTimer timer = new SimpleTimer(5);
            do
            {
                hwnd = Utility.FindWindow("#32770", "提示");
                if (hwnd != IntPtr.Zero)
                    break;
                Application.DoEvents();
            } while (!timer.Elapsed);

            if (hwnd != IntPtr.Zero)
            {
                IntPtr btnOK = Utility.FindWindowEx(hwnd, IntPtr.Zero, "Button", "确认");
                Utility.PostMessage(btnOK, 0x00F5, 0, 0);
            }
            System.Threading.Thread.Sleep(2000);
            timer = new SimpleTimer(10);
            do
            {
                hwnd = Utility.FindWindow("#32770", "提示");
                if (hwnd != IntPtr.Zero)
                    break;
                Application.DoEvents();
            } while (!timer.Elapsed);

            if (hwnd != IntPtr.Zero)
            {
                IntPtr btnOK = Utility.FindWindowEx(hwnd, IntPtr.Zero, "Button", "确认");
                Utility.PostMessage(btnOK, 0x00F5, 0, 0);
            }
            System.Threading.Thread.Sleep(2000);

            timer = new SimpleTimer(10);
            do
            {
                hwnd = Utility.FindWindow("#32770", "提示");
                if (hwnd != IntPtr.Zero)
                    break;
                Application.DoEvents();
            } while (!timer.Elapsed);

            if (hwnd != IntPtr.Zero)
            {
                IntPtr btnOK = Utility.FindWindowEx(hwnd, IntPtr.Zero, "Button", "确认");
                Utility.PostMessage(btnOK, 0x00F5, 0, 0);
            }
            System.Threading.Thread.Sleep(3000);

            timer = new SimpleTimer(10);
            do
            {
                hwnd = Utility.FindWindow("#32770", "撤单提示");
                if (hwnd != IntPtr.Zero)
                    break;
                Application.DoEvents();
            } while (!timer.Elapsed);

            if (hwnd != IntPtr.Zero)
            {
                IntPtr btnOK = Utility.FindWindowEx(hwnd, IntPtr.Zero, "Button", "确认");
                Utility.PostMessage(btnOK, 0x00F5, 0, 0);
            }

            System.Threading.Thread.Sleep(2000);
            timer = new SimpleTimer(10);
            do
            {
                hwnd = Utility.FindWindow("#32770", "撤单提示");
                if (hwnd != IntPtr.Zero)
                    break;
                Application.DoEvents();
            } while (!timer.Elapsed);

            if (hwnd != IntPtr.Zero)
            {
                IntPtr btnOK = Utility.FindWindowEx(hwnd, IntPtr.Zero, "Button", "确认");
                Utility.PostMessage(btnOK, 0x00F5, 0, 0);
            }

        }

        private void CheckBuy()
        {
            IntPtr hwnd = IntPtr.Zero;
            SimpleTimer timer = new SimpleTimer(10);
            do
            {
                hwnd = Utility.FindWindow("#32770", "买入交易确认");
                if (hwnd != IntPtr.Zero)
                    break;
                Application.DoEvents();
            } while (!timer.Elapsed);

            if (hwnd != IntPtr.Zero)
            {
                IntPtr btnOK = Utility.FindWindowEx(hwnd, IntPtr.Zero, "Button", "买入确认");
                Utility.PostMessage(btnOK, 0x00F5, 0, 0);
            }

            System.Threading.Thread.Sleep(1000);

            timer = new SimpleTimer(10);
            do
            {
                hwnd = Utility.FindWindow("#32770", "提示");
                if (hwnd != IntPtr.Zero)
                    break;
                Application.DoEvents();
            } while (!timer.Elapsed);

            if (hwnd != IntPtr.Zero)
            {
                IntPtr btnOK = Utility.FindWindowEx(hwnd, IntPtr.Zero, "Button", "确认");
                Utility.PostMessage(btnOK, 0x00F5, 0, 0);
            }
        }

        private void CheckSell()
        {
            IntPtr hwnd = IntPtr.Zero;
            SimpleTimer timer = new SimpleTimer(10);
            do
            {
                hwnd = Utility.FindWindow("#32770", "卖出交易确认");
                if (hwnd != IntPtr.Zero)
                    break;
                Application.DoEvents();
            } while (!timer.Elapsed);

            if (hwnd != IntPtr.Zero)
            {
                IntPtr btnOK = Utility.FindWindowEx(hwnd, IntPtr.Zero, "Button", "卖出确认");
                Utility.PostMessage(btnOK, 0x00F5, 0, 0);
            }

            System.Threading.Thread.Sleep(1000);

            timer = new SimpleTimer(10);
            do
            {
                hwnd = Utility.FindWindow("#32770", "提示");
                if (hwnd != IntPtr.Zero)
                    break;
                Application.DoEvents();
            } while (!timer.Elapsed);

            if (hwnd != IntPtr.Zero)
            {
                IntPtr btnOK = Utility.FindWindowEx(hwnd, IntPtr.Zero, "Button", "确认");
                Utility.PostMessage(btnOK, 0x00F5, 0, 0);
            }
        }

        #endregion

        #region Event

        public Form1()
        {
            Stocks = new List<Stock>();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isstarted)
            {
                timer1.Stop();
                timer1.Enabled = false;
                (sender as Button).Text = "Start";
            }
            else
            {
                timer1.Enabled = true;
                (sender as Button).Text = "Stop";
            }
            isstarted = !isstarted;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //RemoveOrders(Utility.OrderStatus.Buy, "002394");
            //Buy("002627", "10.2", "200");
            //Sell("002627", "10.8", "200");
            //UpdatePrice();
            //MakeOrder(true, "", "");
            //CloseStock();            
            if (browser.CheckBrowser(""))
            {
                browser.SendMsg("飞信测试一把");
            }
            //UpdateStockStatus();
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (MessageBox.Show("Are you sure del this stock?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Stocks.RemoveAt(listBox1.SelectedIndex);
                        SaveData();
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNo.Text))
                return;
            Stock stock = new Stock() { NO = txtNo.Text };
            stock.Number = int.Parse(numupnumber.Value.ToString());
            string masktext = "   .";
            if (!string.IsNullOrEmpty(mtxtlowestprice.Text) && mtxtlowestprice.Text != masktext)
            {
                stock.LowPrice = decimal.Parse(mtxtlowestprice.Text);
            }
            if (!string.IsNullOrEmpty(mtxthighestprice.Text) && mtxthighestprice.Text != masktext)
            {
                stock.HighPrice = decimal.Parse(mtxthighestprice.Text);
            }
            stock.RaiseStage = chkraisedstage.Checked;
            if (!string.IsNullOrEmpty(mtxtpreprice.Text) && mtxtpreprice.Text != masktext)
            {
                stock.PrePrice = decimal.Parse(mtxtpreprice.Text);
            }
            if (!string.IsNullOrEmpty(mtxtbuy1price.Text) && mtxtbuy1price.Text != masktext)
            {
                stock.Buy1Price = decimal.Parse(mtxtbuy1price.Text);
            }
            if (!string.IsNullOrEmpty(mtxtsell1price.Text) && mtxtsell1price.Text != masktext)
            {
                stock.Sell1Price = decimal.Parse(mtxtsell1price.Text);
            }
            if (!string.IsNullOrEmpty(mtxtlastbuyprice.Text) && mtxtlastbuyprice.Text != masktext)
            {
                stock.LastBuyPrice = decimal.Parse(mtxtlastbuyprice.Text);
            }
            if (!string.IsNullOrEmpty(mtxtlastsellprice.Text) && mtxtlastsellprice.Text != masktext)
            {
                stock.LastSellPrice = decimal.Parse(mtxtlastsellprice.Text);
            }
            stock.IsWatch = chkiswatch.Checked;
            //update
            if (Stocks.Contains(stock))
            {
                Stocks.Remove(stock);
            }
            if ((stock.PrePrice == 0) && (stock.LowPrice != 0))
            {
                stock.PrePrice = stock.LowPrice;
            }
            else if ((stock.PrePrice == 0) && (stock.HighPrice != 0))
            {
                stock.PrePrice = stock.HighPrice;
            }
            Stocks.Add(stock);
            SaveData();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                Stock stock = Stocks[listBox1.SelectedIndex];
                txtNo.Text = stock.NO;
                numupnumber.Value = stock.Number;
                mtxtlowestprice.Text = stock.LowPrice.ToString("f2");
                mtxthighestprice.Text = stock.HighPrice.ToString("f2");
                chkraisedstage.Checked = stock.RaiseStage;
                mtxtpreprice.Text = stock.PrePrice.ToString("f2");
                mtxtbuy1price.Text = stock.Buy1Price.ToString("f2");
                mtxtsell1price.Text = stock.Sell1Price.ToString("f2");
                mtxtlastbuyprice.Text = stock.LastBuyPrice.ToString("f2");
                mtxtlastsellprice.Text = stock.LastSellPrice.ToString("f2");
                chkiswatch.Checked = stock.IsWatch;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Show();
                Application.DoEvents();
                timer1.Enabled = false;
                DateTime dt = System.DateTime.Now;
                if ((dt.DayOfWeek == DayOfWeek.Saturday) || (dt.DayOfWeek == DayOfWeek.Sunday))
                {
                    timer1.Interval = 3600000;
                }
                else if ((dt.Hour >= 9 && dt.Hour < 11) || ((dt.Hour == 11) && (dt.Minute <= 30)) || (dt.Hour >= 13 && dt.Hour < 15))
                {
                    timer1.Interval = 15000;
                    if ((dt.Hour == 9) && ((dt.Minute >= 25) && (dt.Minute <= 30)))
                    {
                        UpdateStockStatus();
                        //get buy1 and sell1 price.
                        GetInitPrice();
                        Application.DoEvents();                        
                    }
                    else if(!(dt.Hour==9 && dt.Minute<30))
                    {
                        UpdateStockStatus();
                        Application.DoEvents();
                        if (!ispause)
                        {
                            GetToday();
                            Application.DoEvents();                            
                            UpdateTodayData();
                            Application.DoEvents();
                            UpdatePrice();
                            Application.DoEvents();
                            RemoveOrders(Utility.OrderStatus.Repeated, "");
                            Application.DoEvents();
                        }
                    }
                    else if (dt.Hour == 9 && dt.Minute < 10)
                    {
                        timer1.Interval = 60000;
                        if (!disablesynced)
                        {
                            if (!downloaded && panenable)
                                if (!DownloadStockData())
                                {
                                    downloaded = false;
                                    Utility.Log("failed to download data.");
                                }
                                else
                                {
                                    LoadData();
                                    initsuccess = false;
                                    uploaded = false;
                                }
                        }
                        InitPrice();
                        Notifyme();
                    }
                }
                else if (dt.Hour == 15 && dt.Minute <= 10)
                {
                    initsuccess = false;
                    downloaded = false;
                    if (!disablesynced)
                    {
                        if (!uploaded && panenable)
                            if (!UploadStockData())
                            {
                                Utility.Log("failed to upload data.");
                                uploaded = false;
                            }
                    }
                }
                else if (dt.Hour == 8)
                {
                    timer1.Interval = 60000;
                    InitPrice();
                }
                else
                {
                    CloseStock();
                }
            }
            finally
            {
                if (isstarted)
                {
                    timer1.Enabled = true;
                }
            }
        }

        private void Notifyme()
        {
            bool isok = true;
            StringBuilder sbnos = new StringBuilder();
            foreach (Stock stock in Stocks)
            {
                if ((stock.PrePrice < stock.LowPrice) || (stock.PrePrice > stock.HighPrice))
                {
                    sbnos.Append(stock.NO+" , preprie-"+stock.PrePrice.ToString("f2")+",lowprice"+stock.LowPrice.ToString("f2")+",highprice"+stock.HighPrice.ToString("f2"));
                    isok = false;
                    continue;
                }                
            }
            if (!isok)
            {
                SendSMS("No Ready:"+ sbnos.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ordersforbuy = new Dictionary<string, string>();
            ordersforsell = new Dictionary<string, string>();

            LoadData();
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            Form frm = new FrmSetting();
            frm.ShowDialog();            
        }

        private void mtxtlowestprice_DoubleClick(object sender, EventArgs e)
        {
            (sender as MaskedTextBox).Text = "0";
        }

        private void mtxthighestprice_DoubleClick(object sender, EventArgs e)
        {
            (sender as MaskedTextBox).Text = "0";
        }

        private void mtxtpreprice_DoubleClick(object sender, EventArgs e)
        {
            (sender as MaskedTextBox).Text = "0";
        }

        private void mtxtbuy1price_DoubleClick(object sender, EventArgs e)
        {
            (sender as MaskedTextBox).Text = "0";
        }

        private void mtxtsell1price_DoubleClick(object sender, EventArgs e)
        {
            (sender as MaskedTextBox).Text = "0";
        }

        private void mtxtlastbuyprice_DoubleClick(object sender, EventArgs e)
        {
            (sender as MaskedTextBox).Text = "0";
        }

        private void mtxtlastsellprice_DoubleClick(object sender, EventArgs e)
        {
            (sender as MaskedTextBox).Text = "0";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Utility.GetWindow();
            //UpdateTodayData();
            //Utility.GetWindow();
            //RemoveOrders(Utility.OrderStatus.All, "");
            //RemoveOrders(Utility.OrderStatus.All, "600785");
            //RemoveOrders(Utility.OrderStatus.All, "300314");
            //InitPrice();
            //RemoveOrders(Utility.OrderStatus.Repeated, "");
            GetToday();
            //UpdateTodayData();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                timer1.Enabled = false;
                timer1.Dispose();
                timer1 = null;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (DownloadStockData())
            {
                LoadData();
                MessageBox.Show("Download success!");
            }
        }

        private bool DownloadStockData()
        {
            try
            {
                downloaded = true;
                BaiduPan pan = new BaiduPan();
                return pan.DownloadFile("/stock/stock.data", Path.Combine(System.Environment.CurrentDirectory, "stock.data"));
            }
            catch
            {
                return false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(UploadStockData())
            {
                MessageBox.Show("Upload success!");
            }
        }

        private bool UploadStockData()
        {
            try
            {
                uploaded = true;
                SaveData();
                BaiduPan pan = new BaiduPan();
                return pan.UploadFile("/stock/stock.data", Path.Combine(System.Environment.CurrentDirectory, "stock.data"));
            }
            catch
            {
                return false;
            }
        }
    }
}
