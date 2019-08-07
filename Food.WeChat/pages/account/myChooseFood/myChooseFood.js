// pages/account/myChooseFood/myChooseFood.js
const app = getApp()
var common = require('../../../utils/common.js')
Page({

  /**
   * 页面的初始数据
   */
  data: {
    isSearch: true,
    pageIndex: 1,
    chooseShopList: [],
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    searchList(this);
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
    this.setData({
      chooseShopList: [],
      isSearch: true,
      pageIndex: 1
    })
    searchList(this);
  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {
    searchList(this);
  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})

function searchList(_this) {
  if (!_this.data.isSearch)
    return;
  common.getData({
    url: "/RfChooseFood/GetRfChooseFoodPageList",
    params: {
      pageIndex: _this.data.pageIndex,
      pageSize: common.pageSize,
      openId: app.globalData.openId,
    },
    success: function (res, s, m) {
      if (s && res.length != 0) {
        _this.setData({
          chooseShopList: _this.data.chooseShopList.concat(res),
          pageIndex: _this.data.pageIndex + 1,
        })
        if (res.length != common.pageSize) {
          _this.setData({
            isSearch: false,
          })
        }
      } else {
        _this.setData({
          isSearch: false,
        })
      }
    },
    fail: function () { }
  })
}