// pages/opinion/opinion.js
const app = getApp()
var common = require('../../utils/common.js')
Page({

  /**
   * 页面的初始数据
   */
  data: {
    btnTxt: "提交",
    btnSubmit: 'btnAddOpinion',
  },

  btnAddOpinion: function(e){
    var _this = this;
    if (e.detail.value.opinion.length == 0) {
      _this.setData({
        tip: '提示：反馈意见不能为空！'
      })
      return;
    }
    if (e.detail.value.contact.length == 0) {
      _this.setData({
        tip: '提示：联系信息不能为空！'
      })
      return;
    }
    _this.setData({
      tip: '',
      btnTxt: "提交中...",
      btnSubmit: ''
    })
    var shop = {
      opinion: e.detail.value.opinion,
      contactInfo: e.detail.value.contact,
      openId: app.globalData.openId
    }
    common.postData({
      url: "/RfOpinion/AddRfOpinion",
      params: shop,
      success: function (res, s, m) {
        if (s && res > 0) {
          common.alert('提交成功！')
          wx.navigateBack({})
        } else {
          common.alertError('提交失败！')
        }
        _this.setData({
          btnTxt: "提交",
          btnSubmit: 'btnAddOpinion'
        })
      },
      fail: function () {
        _this.setData({
          btnTxt: "提交",
          btnSubmit: 'btnAddOpinion'
        })
      }
    })
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {

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