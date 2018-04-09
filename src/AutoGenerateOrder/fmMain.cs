using ManagerCenter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ManagerCenter.Model;
using System.Linq;

namespace AutoGenerateOrder
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            lsRigth.Items.Add("自动生单开始");

            #region 用户信息

            string cutomerInfoText = File.ReadAllText(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration/CustomerInfo.json"));

            if (string.IsNullOrWhiteSpace(cutomerInfoText))
            {
                lsDetails.Items.Add("用户信息没有配制");
                return;
            }
            var customerList = new List<CustomerInfo>() ;
            try
            {
                customerList = ServiceStack.Text.JsonSerializer.DeserializeFromString<List<CustomerInfo>>(cutomerInfoText);
            }
            catch(Exception )
            {
                lsDetails.Items.Add("用户信息有问题，请正常配制，必须满足json格式");
                return;
            }

            #endregion

            #region 商品信息

            string productInfoText = File.ReadAllText(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration/ProductInfo.json"));

            if (string.IsNullOrWhiteSpace(productInfoText))
            {
                lsDetails.Items.Add("商品信息没有配制");
                return;
            }
            var productList = new List<ProductInfo>();
            try
            {
                productList = ServiceStack.Text.JsonSerializer.DeserializeFromString<List<ProductInfo>>(productInfoText);
            }
            catch (Exception)
            {
                lsDetails.Items.Add("商品信息配制错误，请检查, 必须满足json格式");
                return;
            }

            #endregion

            #region 基础配制信息

            string appSettingText = File.ReadAllText(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration/AppSetting.json"));

            if (string.IsNullOrWhiteSpace(appSettingText))
            {
                lsDetails.Items.Add("基础信息没有配制");
                return;
            }
            AppSetting setting = null;
            try
            {
                setting = ServiceStack.Text.JsonSerializer.DeserializeFromString<AppSetting>(appSettingText);
            }
            catch (Exception)
            {
                lsDetails.Items.Add("基础信息错误，请联系开发者");
                return;
            }
            #endregion

            lsRigth.Items.Add("配制文件加载完成");

            foreach (var customer in customerList)
            {
                if (customer == null) continue;

                var cookie = string.Empty;
                var logincookie = string.Empty;

                //登录
                var loginResult =
                Generator.LoginIn<NormalResponse>(
                    string.Format("{0}{1}", setting.HostNormal, setting.Login),
                    new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("CustomerID",customer.CustomerName),
                    new KeyValuePair<string, string>("Password",customer.Password)
                    },
                    ref cookie);
                logincookie = cookie;

                if (!loginResult.Success)
                {
                    lsDetails.Items.Add(string.Format("【{0}】登录出现问题:【{1}】", customer.CustomerName, loginResult.Message));
                    continue;
                }
                lsRigth.Items.Add(string.Format("【{0}】登录成功", customer.CustomerName));


                string tempCookie = cookie;
                //获取用户的发货地址
                var shippingAddress =
                    Generator.ShippingAddress<ShippingAddressResponse>(string.Format("{0}{1}", setting.HostNormal, setting.ShippingAddress),ref tempCookie);
                if (!shippingAddress.Success)
                {
                    lsDetails.Items.Add(string.Format("【{0}】获取用户发货地址失败: 【{1}】", customer.CustomerName, shippingAddress.Message));
                    continue;
                }

                if (shippingAddress.Data == null || shippingAddress.Data.Count == 0)
                {
                    lsDetails.Items.Add(string.Format("【{0}】当前用户没有发货地址，请自行配制", customer.CustomerName));
                    continue;
                }

                int shippingAddressSysNo = 0;
                var defaultAddress = shippingAddress.Data.FirstOrDefault(item => item.IsDefault);
                if (defaultAddress != null)
                    shippingAddressSysNo = defaultAddress.SysNo;
                else
                    shippingAddressSysNo = shippingAddress.Data.First().SysNo;
                

                foreach (var product in productList)
                {
                    if (product == null) continue;

                    //加载购物车
                    cookie = string.Format("{0};{1}", cookie, logincookie);
                    var getCartInfo = Generator.GetCart<GetCartResponse>(string.Format("{0}{1}", setting.HostNormal, setting.GetCart), ref cookie);
                    if (getCartInfo.Success && getCartInfo.Data != null && getCartInfo.Data.HasSucceed && getCartInfo.Data.ReturnData != null &&
                        getCartInfo.Data.ReturnData.OrderItemGroupList != null && getCartInfo.Data.ReturnData.OrderItemGroupList.Count > 0)
                    {
                        var productDeleteList = new List<CartDeleteInfo>();
                        foreach (var itemGroup in getCartInfo.Data.ReturnData.OrderItemGroupList)
                        {
                            if (!itemGroup.IsSelected) continue;
                            var tempProductList = itemGroup.ProductItemList.Select(item => item.ProductSysNo).ToList();
                            foreach (var item in tempProductList)
                            {
                                productDeleteList.Add(new CartDeleteInfo {
                                     ProductSysNo = item,
                                     PackageType = itemGroup.PackageType
                                });
                            }
                        }
                        if (productDeleteList.Count > 0)
                        {
                            string tempDeleteItems = string.Empty;
                            foreach (var delete in productDeleteList)
                                tempDeleteItems = string.Format("{0}:{1};{2}", delete.PackageType, delete.ProductSysNo, tempDeleteItems);

                            tempDeleteItems = tempDeleteItems.Substring(0, tempDeleteItems.Length - 1);

                            //清空购物车
                            var tempDeleteCookie = cookie;
                            var deleteCartInfo = Generator.DeleteCart<NormalResponse>(
                                string.Format("{0}{1}", setting.HostNormal, setting.ClearCart),  
                                new DeleteCartRequest
                                {
                                    Items = tempDeleteItems
                                },
                                ref tempDeleteCookie);

                            if (!deleteCartInfo.Success)
                            {
                                lsDetails.Items.Add(string.Format("【{0}】清空购物车失败", customer.CustomerName));
                                break;
                            }
                        }
                    }

                    //加购车
                    var addCartInfo =
                    Generator.AddCart<NormalResponse>(
                        string.Format("{0}{1}", setting.HostNormal, setting.AddCart),
                        new AddCartRequest
                        {
                            SysNo = product.ProductSysNo,
                            Qty = product.Qty,
                            IsPackage = false
                        },
                    ref cookie);

                    if (!addCartInfo.Success)
                    {
                        lsDetails.Items.Add(string.Format("【{0}】【{1}】的订单生成失败: 【{2}】", customer.CustomerName, product.ProductSysNo, addCartInfo.Message));
                        continue;
                    }

                    lsRigth.Items.Add(string.Format("【{0}】【{1}】加购物车: {2}", customer.CustomerName, product.ProductSysNo, addCartInfo.Message ?? ""));

                    //加载checkout数据，看是否有错误信息
                    cookie = string.Format("{0};{1}", cookie, logincookie);
                    var checkOutInfo = Generator.CheckOut<CheckoutResponse>(string.Format("{0}{1}", setting.HostNormal, setting.CheckOut), null, ref cookie);
                    if (!checkOutInfo.Success || !checkOutInfo.Data.HasSucceed)
                    {
                        string message = !checkOutInfo.Success ? checkOutInfo.Message : checkOutInfo.Data.ErrorMessages;
                        lsDetails.Items.Add(string.Format("【{0}】【{1}】的订单生成失败: 【{2}】", customer.CustomerName, product.ProductSysNo, message));
                        continue;
                    }

                    //提交订单
                    cookie = string.Format("{0};{1}", cookie, logincookie);
                    var generateInfo = Generator.Generate<GenerateResponse>(
                            string.Format("{0}{1}", setting.HostNormal, setting.GenerateOrder),
                            new GenerateRequest
                            {
                                IsUsedPrePay = 0,
                                PayTypeID = setting.PayTypeID,
                                PointPay = 0,
                                ShippingAddressID = shippingAddressSysNo
                            }, ref cookie);

                    if (generateInfo.Success)
                        lsRigth.Items.Add(string.Format("【{0}】的订单生成, 订单号:【{1}】", customer.CustomerName, generateInfo.Data));
                    else
                        lsDetails.Items.Add(string.Format("【{0}】【{1}】的订单生成失败: 【{2}】", customer.CustomerName, product.ProductSysNo, generateInfo.Message));

                }
            }

            lsRigth.Items.Add("处理完成");
            lsDetails.Items.Add("处理完成");
        }

        private void btnCustomerInfo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration/CustomerInfo.json"));
        }

        private void btnProductInfo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration/ProductInfo.json"));
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration/note.txt"));
        }
    }
}
