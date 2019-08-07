// pages/choose/foodDetails/foodDetails.js
const common = require('../../../utils/common.js')
Page({

  /**
   * 页面的初始数据
   */
  data: {
    imgUrl: common.imgUrl
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    wx.setNavigationBarTitle({
      title: options.name || '详情'
    })
    if (!options.id) {
      return;
    }
    var _this = this;
    common.getData({
      url: "/RfFood/GetRfFood",
      params: {
        id: options.id,
      },
      success: function (res, s, m) {
        if (s) {
          _this.setData({
            food: res,
          })
          if (options.name != res.name){
            wx.setNavigationBarTitle({
              title: res.name || '详情'
            })
          }
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