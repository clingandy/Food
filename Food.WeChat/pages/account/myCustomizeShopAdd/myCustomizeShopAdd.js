// pages/account/myFoodAdd/myFoodAdd.js
const app = getApp()
var common = require('../../../utils/common.js')

Page({

  /**
   * 页面的初始数据
   */
  data: {
    btnTxt:"保存",
    btnSubmit:'btnAddShop',
    id: 0,
    lable: 8,
    shop:{}
  },

  radioLableChange: function(e){
    var lable = parseInt(e.detail.value);
    this.setData({
      lable: lable
    });
  },

  btnAddShop: function (e) {
    var _this = this;
    if (e.detail.value.title.length == 0) {
      _this.setData({
        tip: '提示：名称不能为空！'
      })
      return;
    }
    if (e.detail.value.category.length == 0) {
      _this.setData({
        tip: '提示：类别不能为空！'
      })
      return;
    }
    _this.setData({
      tip: '',
      btnTxt: "保存中...",
      btnSubmit: ''
    })
    var url = "/RfMemberCustomizeShop/ModifyRfMemberCustomizeShop";
    var shop = _this.data.shop;
    var method = "POST"
    if (_this.data.id == 0){
      shop = {
        title: e.detail.value.title,
        category: e.detail.value.category,
        lable: _this.data.lable,
        openId: app.globalData.openId
      }
      url = "/RfMemberCustomizeShop/AddRfMemberCustomizeShop";
    }else{
      method = "PUT"
      shop.lable = _this.data.lable;
      shop.title = e.detail.value.title;
      shop.category= e.detail.value.category;
    }
    common.postData({
      method: method,
      url: url,
      params: shop,
      success: function (res, s, m) {
        if (s) {
          common.alert('操作成功！')
          // wx.navigateBack({})
        } else {
          common.alertError('操作失败！')
        }
        _this.setData({
          btnTxt: "保存",
          btnSubmit: 'btnAddShop'
        })
      },
      fail: function () {
        _this.setData({
          btnTxt: "保存",
          btnSubmit: 'btnAddShop'
        })
      }
    })
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    if (!options.id){
      return;
    }else{
      wx.setNavigationBarTitle({
        title: '编辑收藏'
      })
    }
    var _this = this;
    common.getData({
      url: "/RfMemberCustomizeShop/GetRfMemberCustomizeShop",
      params: {
        id: options.id,
      },
      success: function (res, s, m) {
        if (s) {
          _this.setData({
            id: options.id,
            shop: res,
            lable: res.lable
          })
        } else {
          common.alertError('获取信息失败！')
        }
      },
      fail: function () { }
    })
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})